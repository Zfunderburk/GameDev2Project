using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AP
{
    public class DamagePlayer : MonoBehaviour
    {

        public Collider enemySword;
        PlayerHealth pHealth;
		void OnTriggerEnter(Collider other)
		{
            Debug.Log("we are in the trigger");
           // PlayerHealth pHealth = enemySword.transform.GetComponentInParent<PlayerHealth>();

            //if (pHealth == null)
                //return;
            pHealth.TakeDamage(5);
		}
	}
}
