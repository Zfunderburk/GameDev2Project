﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AP
{
	public class AIHandler : MonoBehaviour 
	{
		public AIAttacks[] ai_attacks;

		public StateManager states;
		public EnemyStates estates;
		public Transform target;

		public int closeCount = 10;
		int _close;

		public int frameCount = 30;
		int _frame;
		public float sight;
		public float fov_angle;
		float dis;
		float angle;
		float delta;
		Vector3 dirToTarget;

		float distanceFromTarget()
		{
			if(target == null)
				return 100;
	
			return Vector3.Distance(target.position, transform.position);

		}

		float angleToTarget()
		{
			float a = 180;				
			if(target)
			{
				Vector3 d = dirToTarget;
				a = Vector3.Angle (d, transform.forward);
			}
			return a;

		}

		void Start()
		{
			if(estates == null)
			{
				estates = GetComponent<EnemyStates> ();
			}
			estates.Init ();
		}

		public AIstate aiState;
		public enum AIstate
		{
			far,close,inSight,attacking
		}


		void Update()
		{
			delta = Time.deltaTime;
			dis = distanceFromTarget ();
			angle = angleToTarget ();
			if (target)
				dirToTarget = target.position - transform.position;
			
			switch (aiState) 
			{
			case AIstate.far:
				HandleFarSight ();
				break;
			case AIstate.close:
				HandleCloseSight ();
				break;
			case AIstate.inSight:
				inSight ();
				break;
			case AIstate.attacking:
				if(estates.canMove)
				{
					aiState = AIstate.inSight;
				}
				break;

			}
			estates.Tick ();
	
		}



		void HandleCloseSight()
		{
			_close++;
			if (_close > closeCount) {
				_close = 0;

				if (dis > sight || angle > fov_angle) 
				{
					aiState = AIstate.far;
					return;
				}

			}
			RaycastToTarget ();
		}
		void inSight()
		{
			AIAttacks attack = WillAttack ();
			if (attack != null)
			{
				aiState = AIstate.attacking;

				estates.anim.Play(attack.targetAnim);
				estates.anim.SetBool ("canMove", false); //I NEED TO DO HIS STATICSTRINGS CLASS
				estates.canMove = false;
				return;
			}

			float d2 = Vector3.Distance (estates.targetDestination, target.position);
			if(d2 > 2)
			{
				estates.SetDestination (target.position);
			}
			if (dis< 2)
			{
				estates.agent.isStopped = true;
			}
		}
	

		public AIAttacks WillAttack()
		{
			int w = 0;
			List<AIAttacks> l = new List<AIAttacks> ();
			for (int i = 0; i < ai_attacks.Length; i++)
			{
				AIAttacks a = ai_attacks [i];
				if(a._cool > 0)
				{
					a._cool -= delta;
					if(a._cool < 0)
						a._cool = 0;
					continue;
					
				}

				if (a.minDistance > dis)
					continue;
				if (a.minAngle < fov_angle)
					continue;
				if (a.maxAngle > fov_angle)
					continue;
				if (a.weight == 0)
					continue;

				w += a.weight;
				l.Add (a);
			}

			if (l.Count == 0)
				return null;
			
			int ran = Random.Range (0, w + 1);
			int c_w = 0;
			for (int i = 0; i < l.Count; i++)
			{
				c_w += l [i].weight;
				if(c_w> ran)
				{
					return l[i];
				}
			}
			return null;
		}

		void RaycastToTarget()
		{
			RaycastHit hit;
			Vector3 origin = transform.position;
			origin.y += 0.5f;
			Vector3 dir = dirToTarget;
			dir.y += 0.5f;
			if(Physics.Raycast(origin,dir, out hit, sight, estates.ignoreLayers))
			{
				StateManager st = hit.transform.GetComponentInParent<StateManager> ();
				if (st!= null)
				{
					aiState = AIstate.inSight;
				}
			}
		}

		void HandleFarSight()
		{
			if (target == null)
				return;
			_frame++;
			if(_frame > frameCount)
			{
				_frame = 0;

				if(dis < sight)
				{
					if(angle < fov_angle)
					{
						aiState = AIstate.close;
					}
				}

					
			}
		}
	}



	[System.Serializable]
	public class AIAttacks
	{
		public int weight;
		public float minDistance;
		public float minAngle;
		public float maxAngle;

		public float coolDown = 2;
		public float _cool;
		public string targetAnim;
	}
}

