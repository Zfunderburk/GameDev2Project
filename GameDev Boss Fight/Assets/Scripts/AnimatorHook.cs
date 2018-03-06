using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AP
{
	public class AnimatorHook : MonoBehaviour
	{
		Animator anim;
		StateManager states;

		public void Init(StateManager st)
		{
			states = st;
			anim = st.anim;
		}

		void OnAnimatorMove()
		{
			if (states.canMove)
				return;
			
			states.myBody.drag = 0;
			float multiplier = 1;									//fat rolls and what not

			Vector3 delta = anim.deltaPosition;
			delta.y = 0;											//no changing delta y
			Vector3 v = (delta * multiplier) / states.delta;
			states.myBody.velocity = v;
		}

	}
}
