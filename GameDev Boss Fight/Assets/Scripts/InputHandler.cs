using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AP
{
	

public class InputHandler : MonoBehaviour
{
	float vertical, horizontal;

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



	void GetInput()
	{
		vertical = Input.GetAxis ("Vertical");
		horizontal = Input.GetAxis ("Horizontal");
	}
	// Update is called once per frame
	void UpdateStates () 
	{
			states.horizontal = horizontal;
			states.vertical = vertical;

			Vector3 v = vertical * camManager.transform.forward;
			Vector3 h = horizontal * camManager.transform.right;
			states.moveDir = (v + h).normalized;
			float m = Mathf.Abs (horizontal) + Mathf.Abs (vertical);
			states.moveAmount = Mathf.Clamp01 (m);


	}
}
}
