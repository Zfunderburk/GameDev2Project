using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour {

//	public GameObject weapon1;
//	public GameObject weapon2;
//	public GameObject weapon3;

	public GameObject hand;

//	public Transform handTrans;

//	public Vector3 handPoint;


	void OnTriggerEnter (Collider other)
	{
		transform.parent = hand.transform;									//Parents, but doesnt set to hand Position
		transform.localPosition = new Vector3(0,0,0);						//Sets Weapon to the local position of Hand
		transform.localRotation = Quaternion.identity;						//Sets Weapon to the local rotation of Hand

	}
}
