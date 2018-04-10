using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AP
{

	public class DamageCollider : MonoBehaviour 
	{
        public Collider swordColliderFromPlayer;

		private void Update()
		{
           // swordColliderFromPlayer =the weapon that the player is holding collider ;
		}
		void OnTriggerEnter(Collider other)
		{
            Debug.Log(other);
            EnemyStates eStates = other.transform.transform.GetComponentInParent<EnemyStates> ();

			if (eStates == null)
			{
				return;
			}

			eStates.DoDamage (5);
		}

	}
}