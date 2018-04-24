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
        //public AP.WeaponHook weapon1;
		
		//void Update()
		//{
  //          // swordColliderFromPlayer =the weapon that the player is holding collider ;
  //          //swordColliderFromPlayer = handle.GetComponentInChildren<Collider>();
  //          childrenColliders = handle.GetComponentsInChildren<Collider>();
  //          swordColliderFromPlayer = childrenColliders[1];
  //          //Debug.Log(childrenColliders[1]);
  //         // Debug.Log(swordColliderFromPlayer);

		//}

        public void OnTriggerEnter(Collider col)
		{
            // Debug.Log(swordColliderFromPlayer);
            //Debug.Log(col);
            if (col.gameObject.transform.GetChild(0).GetComponent<BossAnimEventReceiver>()) // if it has this component do dmg to it
            {
                EnemyStates.Instance.DoDamage(10);
                //weapon1.OpenDamageColliders();
            }
            //if (col.gameObject.GetComponent<PlayerHealth>())
		              //PlayerHealth.Instance.TakeDamage(10);
		}

  //      void OnCollisionEnter(Collision collider)
		//{
  //          Debug.Log("we might be close");
  //          if(collider.gameObject.tag == "enemyWeapon")
  //              PlayerHealth.Instance.TakeDamage(10);
		//}

	}
}