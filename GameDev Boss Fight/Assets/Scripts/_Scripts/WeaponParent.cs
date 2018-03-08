using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour {

	public Transform Spawn;
	public GameObject handle;
	public Transform weapon;

	public bool dropWeapon;


	void OnTriggerEnter (Collider other)
	{
		transform.parent = handle.transform;								//Parents, but doesnt set to hand Position
		transform.localPosition = new Vector3(0,0,0);						//Sets Weapon to the local position of Hand
		transform.localRotation = Quaternion.identity;						//Sets Weapon to the local rotation of Hand

	}

	void FixedUpdate ()
	{
		if(Input.GetKeyDown ("space"))
		{
			dropWeapon = true;
		}

//		DropWeapon ();

		if(dropWeapon == true)
		{
			weapon.transform.parent = null;

//			weapon.transform.parent = Spawn;
			weapon.transform.position = Spawn.transform.position;
			weapon.transform.rotation = Quaternion.Euler (Vector3.zero);

			dropWeapon = false;
		}


	}

//	void DropWeapon ()
//	{
//		if(dropWeapon == true)
//		{
//			weapon.transform.parent = null;
//		}
//	}


}
