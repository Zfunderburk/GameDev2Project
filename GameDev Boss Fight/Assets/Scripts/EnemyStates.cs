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
	

        [Header("Values")]
        public float delta;
        public float horizontal;
        public float vertical;


		public LayerMask ignoreLayers;

		public void Init()
		{
			anim = GetComponentInChildren<Animator> ();
			enTarget = GetComponent<EnemyTarget> ();
			enTarget.Init (anim);

			myBody = GetComponent<Rigidbody> ();
            agent = GetComponent<NavMeshAgent>();
            myBody.isKinematic = true;

			a_hook = anim.GetComponent < AnimatorHook> ();
			if (a_hook == null)
			{
				a_hook = anim.gameObject.AddComponent<AnimatorHook>();
			}
			a_hook.Init (null, this);

			ignoreLayers = ~(1 << 9);
		}

		public void Tick(float d)
		{
			delta = d;
			canMove = anim.GetBool ("canMove");

			if(isInvincible)
			{
				isInvincible = !anim.GetBool ("canMove");

			}
			if(canMove)
			{
				anim.applyRootMotion = false;

                MovementAnimation();
			}
            else
            {
                if (anim.applyRootMotion=false)
                    anim.applyRootMotion = true;
            }
		
		}

        public void MovementAnimation()
        {
            Vector3 desiredVel = agent.desiredVelocity;
            Vector3 relative = transform.InverseTransformDirection(desiredVel);

            float v = relative.z;
            float h = relative.x;

            anim.SetFloat("Horizontal", h, 0.2f, delta);
            anim.SetFloat("Vertical", v, 0.2f, delta);

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


