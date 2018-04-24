using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AP
{
    public class DamagePlayer : MonoBehaviour
    {
        public Collider swordEnemy;
       // public AP.WeaponHook weapon1;

        void OnCollisionEnter(Collision collision)
		{
            Debug.Log(collision.gameObject);
            Debug.Log("we collided");
            if (collision.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(5).GetChild(0).GetChild(0).GetChild(0).tag == "enemyWeapon")
            {
                Debug.Log("in if");
                PlayerHealth.Instance.TakeDamage(7);
                //weapon1.OpenDamageColliders();
            }
		}

		//void OnTriggerEnter(Collider col)
        //{
        //    // Debug.Log(swordColliderFromPlayer);
        //    Debug.Log(col);
        //    if (col.gameObject.GetComponent<PlayerHealth>())
        //        PlayerHealth.Instance.TakeDamage(10);
        //}
	}
}
