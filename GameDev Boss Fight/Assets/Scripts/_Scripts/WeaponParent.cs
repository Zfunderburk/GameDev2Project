using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour {


	public GameObject handle;
	public Transform Spawn;
	public Transform weapon;

	public bool dropWeapon;



	void OnTriggerEnter (Collider other)
	{
		transform.parent = handle.transform;								//Parents, but doesnt set to hand Position
		transform.localPosition = new Vector3(0,0,0);						//Sets Weapon to the local position of Hand
		transform.localRotation = Quaternion.identity;						//Sets Weapon to the local rotation of Hand

	}

	void Update ()
	{
		if (Input.GetKeyDown ("space"))
		{
			dropWeapon = true;
			Debug.Log ("Weapon Dropped");
		}

		if(dropWeapon == true)
		{
			weapon.transform.parent = null;

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

//	void DropWeapon ()
//	{
//		
//	}


}
