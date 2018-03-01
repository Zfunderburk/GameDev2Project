using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour 
{

//	public GameObject player;
	public Animator Door;

//	private bool triggerEnter;
//	private bool triggerExit;



	void OnTriggerEnter (Collider other)
	{
		DoorDown();
	}

	void OnTriggerExit (Collider other)
	{
		DoorUp();
	}

	void DoorDown ()
	{
		Door.SetBool ("DoorLower", true);
		Door.SetBool ("DoorRise", false);
	}

	void DoorUp ()
	{
		Door.SetBool ("DoorLower", false);
		Door.SetBool ("DoorRise", true);
	}
}
