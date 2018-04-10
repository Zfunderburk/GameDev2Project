using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AP
{

	public class DamageCollider : MonoBehaviour 
	{
        //public Collider swordColliderFromPlayer;
		void OnTriggerEnter(Collider other)
		{
            EnemyStates eStates = other.transform.transform.GetComponentInParent<EnemyStates> ();

			if (eStates == null)
			{
				return;
			}

			eStates.DoDamage (5);
		}

	}
}