using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AP
{

	public class WeaponHook : MonoBehaviour 
	{
		public GameObject[] damageCollider;
		public Collider [] damColliders;

		public void OpenDamageColliders()
		{
//			for (int i = 0; i < damageCollider.Length; i++)
//			{
//				damageCollider [i].SetActive (true);
//			}

			for (int i = 0; i < damColliders.Length; i++)
			{				
				damColliders [i].enabled = true;
			}
		}
		public void CloseDamageColliders()
		{
//			for (int i = 0; i < damageCollider.Length; i++)
//			{
//				damageCollider [i].SetActive (false);
//			}

			for (int i = 0; i < damColliders.Length; i++)
			{				
				damColliders [i].enabled = false;
			}
		}






	}
}
