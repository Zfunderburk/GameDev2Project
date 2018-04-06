using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AP
{
	//at 40 minutes part 38
	public class AnimatorHook : MonoBehaviour
	{
		Animator anim;
		StateManager states;
		EnemyStates eStates;
		Rigidbody myBody;

	


		public float rm_multi;	//rootmotion multiplier
		public bool rolling;
		float roll_t;
		float delta;
		AnimationCurve roll_curve;




		public void Init(StateManager st, EnemyStates eSt)
		{
			states = st;
			eStates = eSt;
			if(st != null)
			{
				anim = st.anim;
				myBody = st.myBody;
				roll_curve = st.roll_curve;
				delta = st.delta;
			}
				
			if (eSt != null)
			{
				anim= eSt.anim;
				myBody = eSt.myBody;
				delta = eSt.delta;
			}
				
		}

		public void InitForRoll()
		{
			rolling = true;
			roll_t = 0;
		}

		public void CloseRoll()
		{
			if (rolling == false)
				return;
			
			rm_multi = 1;
			roll_t = 0;
			rolling = false;
		}

		void OnAnimatorMove()
		{
			if (states == null && eStates == null) //this was from an error when he placed the animator hook on the player but mine is auto assinged?
				return;

			if (myBody == null)
				return;

			if(states != null)
			{
				if (states.canMove)
					return;
				delta = states.delta;
			}
			if(eStates != null)
			{
				if (eStates.canMove)
					return;
				delta = eStates.delta;
			}
			
			myBody.drag = 0;

			if (rm_multi == 0)
				rm_multi = 1;

			if (rolling == false)
			{
				Vector3 delta2 = anim.deltaPosition;
				delta2.y = 0;											//no changing delta y
				Vector3 v = (delta2 * rm_multi) / delta;
				myBody.velocity = v;
			}
			else
			{
				roll_t += delta / 0.6f;
				if (roll_t > 1)
				{
					roll_t = 1;
				}

				if (states == null)
					return;

				float zValue = roll_curve.Evaluate (roll_t);
				Vector3 v1 = Vector3.forward * zValue;
				Vector3 relative = transform.TransformDirection (v1);
				Vector3 v2 = (relative * rm_multi) / states.delta;
				myBody.velocity = v2;
			}
		}

		public void OpenDamageColliders() 
		{
			if (states == null)
				return;
			
			states.ls_hook.OpenDamageColliders (); //this is different because he has an inventory to manage and pulls weapons from that
			states.g_hook.OpenDamageColliders ();
			states.d_hook.OpenDamageColliders ();
		}

		public void CloseDamageColliders()
		{
			if (states == null)
				return;
			
			states.ls_hook.CloseDamageColliders ();
			states.g_hook.OpenDamageColliders ();
			states.d_hook.OpenDamageColliders ();
		}

	}
}
