using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AP
{
	

	public class InputHandler : MonoBehaviour
	{
		float vertical, horizontal;

		bool b_input;
		bool a_input;
		bool x_input;
		bool y_input;

		/*bool rb_input;
		float rt_axis;
		bool rt_input;
		bool lb_input;
		float lt_axis;
		bool lt_input;*/

		float b_timer;

		bool rightAxis_down;

		StateManager states;
			
		CameraManager camManager;

		float delta;

		void Start () 
		{
			states = GetComponent<StateManager> ();
			states.Init ();

			camManager = CameraManager.singleton;
			camManager.Init (states);
		}


		void FixedUpdate()
		{
			delta = Time.fixedDeltaTime;
			GetInput ();
			UpdateStates ();
			states.FixedTick (delta);
			camManager.Tick (delta);
		}

		void Update()
		{			
			delta = Time.deltaTime;
			states.Tick (delta);	
			ResetInputNStates ();

		}

		void GetInput()
		{
			vertical = Input.GetAxis ("Vertical");
			horizontal = Input.GetAxis ("Horizontal");
			b_input = Input.GetButton ("b_input");
			a_input = Input.GetButtonDown ("a_input");
			x_input = Input.GetButton ("x_input");
			y_input = Input.GetButton ("y_input");

			rightAxis_down = Input.GetButtonDown ("rightAxis_down");
			if (b_input)
			{
				b_timer += delta;
			}
			//Debug.Log (rightAxis_down);
			/*rt_input = Input.GetButton ("RT"); //button and axis so keyboard or controller
			rt_axis = Input.GetAxis("RT");

			if (rt_axis != 0)
			{
				rt_input = true;
			}

			lt_input = Input.GetButton ("LT");
			lt_axis = Input.GetAxis ("LT");
			if (lt_axis!= 0)
			{
				lt_input = true;
			}

			Debug.Log (rt_input);*/
		}

		void UpdateStates () 
		{
			states.horizontal = horizontal;
			states.vertical = vertical;
		

			Vector3 v = vertical * camManager.transform.forward;
			Vector3 h = horizontal * camManager.transform.right;
			states.moveDir = (v + h).normalized;
			float m = Mathf.Abs (horizontal) + Mathf.Abs (vertical);
			states.moveAmount = Mathf.Clamp01 (m);

			if (b_input && b_timer > 0.5f)
			{
				states.run = (states.moveAmount > 0);
			}
			if (b_input == false && b_timer > 0 && b_timer < 0.5f)
			{
				states.rollInput = true;
			}
			states.rollInput = x_input;
			//if (x_input)
			//states.rt = rt_input;
			//states.rb = rb_input;
			//states.lt = lt_input;
			//states.lb = lb_input;
			states.a = a_input;
			states.x = x_input;
			states.y = y_input;

			if (rightAxis_down)
			{
				states.lockOn = !states.lockOn;
				//states.lockOn = EnemyManager.singleton.GetEnemy (transform.position); //since we hav 1 enemy we dont need to change
				if(states.lockOnTarget == null)
				{
					states.lockOn = false;
				}
				//states.lockOn = false;
				camManager.lockonTarget = states.lockOnTarget;
				//states.lockOnTransform = states.lockOnTarget.GetTarget ();//when i try to change it messes up the lock on that works 
				states.lockOnTransform = camManager.lockOnTransform;
				camManager.lockOn = states.lockOn;
			}

		}
		void ResetInputNStates()
		{
			if (b_input == false)
			{
				b_timer = 0;
			}
			if (states.rollInput)
			{
				states.rollInput = false;
			}
			if(states.run)
			{
				states.run = false;
			}
		}
	}
}
