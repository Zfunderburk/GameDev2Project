﻿using System.Collections;
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

		StateManager states;
			
		CameraManager camManager;

		float delta;

		void Start () 
		{
			states = GetComponent<StateManager> ();
			states.Init ();

			camManager = CameraManager.singleton;
			camManager.Init (this.transform);
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
		}

		void GetInput()
		{
			vertical = Input.GetAxis ("Vertical");
			horizontal = Input.GetAxis ("Horizontal");
			b_input = Input.GetButton ("b_input");
			a_input = Input.GetButton ("a_input");
			x_input = Input.GetButton ("x_input");
			y_input = Input.GetButton ("y_input");





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

			if (b_input)
			{
				states.run = (states.moveAmount > 0);
			}
			else
			{
				states.run = false;
			}
			//states.rt = rt_input;
			//states.rb = rb_input;
			//states.lt = lt_input;
			//states.lb = lb_input;
			states.a = a_input;
			states.x = x_input;
			states.y = y_input;

		}
	}
}
