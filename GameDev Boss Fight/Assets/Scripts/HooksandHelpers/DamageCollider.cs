using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AP
{

	public class DamageCollider : MonoBehaviour 
	{
        Collider[] childrenColliders;
        public Collider swordColliderFromPlayer;
        public GameObject handle;
        //EnemyStates eStates;


		
		private void Update()
		{
            // swordColliderFromPlayer =the weapon that the player is holding collider ;
            //swordColliderFromPlayer = handle.GetComponentInChildren<Collider>();
            childrenColliders = handle.GetComponentsInChildren<Collider>();
            swordColliderFromPlayer = childrenColliders[1];
            Debug.Log(childrenColliders[1]);
           // Debug.Log(swordColliderFromPlayer);

		}

        void OnTriggerEnter(Collider swordColliderFromPlayer)
		{
            
            EnemyStates eStates = swordColliderFromPlayer.transform.GetComponentInParent<EnemyStates> (); // this line is the issue
            Debug.Log(swordColliderFromPlayer);
			if (eStates == null)
			{
				return;
			}

			eStates.DoDamage (5);
		}

	}
}