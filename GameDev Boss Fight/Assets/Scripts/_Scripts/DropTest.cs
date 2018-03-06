using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTest : MonoBehaviour {

	public GameObject weapon;

	void Update ()
	{

		if (Input.GetKeyDown ("t"))
		{
			transform.DetachChildren();
			Debug.Log ("Weapon Dropped");
		}

	}



}
