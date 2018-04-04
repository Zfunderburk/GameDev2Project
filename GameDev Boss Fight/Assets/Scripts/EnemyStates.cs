using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AP
{
	public class EnemyStates : MonoBehaviour
	{
		public float health;
		public bool isInvincible;
		public bool canMove;
		public bool hasDestination;
		public Vector3 targetDestination;

		public NavMeshAgent agent;

		public Animator anim;
		EnemyTarget enTarget;
		AnimatorHook a_hook;
		public Rigidbody myBody;
		public float delta;


		public LayerMask ignoreLayers;

		public void Init()
		{
			anim = GetComponentInChildren<Animator> ();
			enTarget = GetComponent<EnemyTarget> ();
			enTarget.Init (anim);

			myBody = GetComponent<Rigidbody> ();

			a_hook = anim.GetComponent < AnimatorHook> ();
			if (a_hook == null)
			{
				a_hook = anim.gameObject.AddComponent<AnimatorHook>();
			}
			a_hook.Init (null, this);

			ignoreLayers = ~(1 << 9);
		}

		public void Tick()
		{
			delta = Time.deltaTime;
			canMove = anim.GetBool ("canMove");

			if(isInvincible)
			{
				isInvincible = !anim.GetBool ("canMove");

			}
			if(canMove)
			{
				anim.applyRootMotion = false;
			}
		
		}

		public void SetDestination(Vector3 d)
		{
			if(!hasDestination)
			{
				hasDestination = true;
				agent.isStopped = false;
				agent.SetDestination (d);
				targetDestination = d;
			}
		}

		public void DoDamage(float v)
		{
			if (isInvincible)
				return;
			health -= v;
			isInvincible = true;
			anim.Play ("damage_1");
			anim.applyRootMotion = true;
			anim.SetBool ("canMove", false);
		}


	}
}


