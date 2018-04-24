using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimEventReceiver : MonoBehaviour 
{
	public AP.WeaponHook weapon1;
	public AP.WeaponHook weapon2;
	public AP.WeaponHook weapon3;

	public void OpenColliders ()
	{
		weapon1.OpenDamageColliders ();
		weapon2.OpenDamageColliders ();
		weapon3.OpenDamageColliders ();
//      AP.EnemyStates.Instance.DoDamage(10);

	}


	public void CloseColliders ()
	{
		weapon1.CloseDamageColliders ();
		weapon2.CloseDamageColliders ();
		weapon3.CloseDamageColliders ();
	}
}
