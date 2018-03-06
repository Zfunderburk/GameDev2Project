using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour {

	public GameObject handle;



	void OnTriggerEnter (Collider other)
	{
		transform.parent = handle.transform;								//Parents, but doesnt set to hand Position
		transform.localPosition = new Vector3(0,0,0);						//Sets Weapon to the local position of Hand
		transform.localRotation = Quaternion.identity;						//Sets Weapon to the local rotation of Hand

	}

	void DropWeapon ()
	{
		if (Input.GetKeyDown ("t"))
		{
			transform.parent = null;
			Debug.Log ("Weapon Dropped");
		}
	}


}
