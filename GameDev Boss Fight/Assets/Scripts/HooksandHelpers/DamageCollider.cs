using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AP
{

	public class DamageCollider : MonoBehaviour 
	{
        Collider[] childrenColliders;
        public Collider swordColliderFromPlayer;
        public Collider swordEnemy;
        public GameObject handle;
		
		void Update()
		{
            // swordColliderFromPlayer =the weapon that the player is holding collider ;
            //swordColliderFromPlayer = handle.GetComponentInChildren<Collider>();
            childrenColliders = handle.GetComponentsInChildren<Collider>();
            swordColliderFromPlayer = childrenColliders[1];
            //Debug.Log(childrenColliders[1]);
           // Debug.Log(swordColliderFromPlayer);

		}

        void OnTriggerEnter(Collider col)
		{
		    Debug.Log(swordColliderFromPlayer);

			if(col.gameObject.transform.GetChild(0).GetComponent<BossAnimEventReceiver>()) // if it has this component do dmg to it
		              EnemyStates.Instance.DoDamage (5);
            
            if (col.gameObject.GetComponent<PlayerHealth>())
		              PlayerHealth.Instance.TakeDamage(10);
		}
		
	}
}