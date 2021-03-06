﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AP
{

    //part 38 time 118 is doing rotation on attacks for the enemy will add if the enemy gets fixed
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

        //public int attackCount = 30; // he has this set to 30
        //int _attack;

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

            RaycastToTarget();
			switch (aiState) 
			{
			case AIstate.far:
				HandleFarSight ();
				break;
			case AIstate.close:
				HandleCloseSight ();
				break;
			case AIstate.inSight:
				InSight ();
				break;
			case AIstate.attacking:
				if(estates.canMove)
				{
					aiState = AIstate.inSight;
                    //estates.agent.enabled = true;
				}
				break;

			}
            estates.Tick (delta);
          
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

        void GoToTarget()
        {
            estates.hasDestination = false;
            estates.SetDestination(target.position);
        }
		void InSight()
		{
            LookTowardsTarget();
           // HandelCooldowns();

            //if(_attack > 0)
            //{
            //    _attack--;
            //    return;
            //}
            //_attack = attackCount;

            float d2 = Vector3.Distance(estates.targetDestination, target.position);
            if (d2 > 2 || dis > sight * .5)
            {
                GoToTarget();
            }
            if (dis < 2)
            {
                estates.agent.isStopped = true;
            }

			AIAttacks attack = WillAttack ();
			if (attack != null)
			{
				aiState = AIstate.attacking;

				estates.anim.Play(attack.targetAnim);
				estates.anim.SetBool ("canMove", false); //I NEED TO DO HIS STATICSTRINGS CLASS
				estates.canMove = false;
                attack._cool = attack.coolDown;
                estates.agent.isStopped = true;
               // estates.agent.enabled = false;
				return;
			}

		
		}
	

        //void HandelCooldowns()
        //{
        //    for (int i = 0; i < ai_attacks.Length; i++)
        //    {
        //        AIAttacks a = ai_attacks[i];
        //        if (a._cool > 0)
        //        {
        //            continue;
        //        }
        //    }
        //}

        public void LookTowardsTarget()
        {
            
            Vector3 dir = dirToTarget;
            dir.y = 0;
            if(dir == Vector3.zero)
            {
                dir = transform.forward;
            }
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, delta * 5);
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

                if (dis > a.minDistance)
					continue;
                if (angle < a.minAngle)
					continue;
                if (angle > a.maxAngle)
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
                    estates.anim.SetBool("lockon", true);
					aiState = AIstate.inSight;
                    estates.SetDestination(target.position);
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

