using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimEventReceiver : MonoBehaviour 
{
    
    public AP.WeaponHook weapon1;

	private void Start()
	{
        weapon1.damColliders[0].enabled = false;
        //Debug.Log(weapon1.damColliders[0]);
     
	}

	public void OpenBossColliders()
    {
        weapon1.OpenDamageColliders();
//      PlayerHealth.Instance.TakeDamage(7);

    }


    public void CloseBossColliders()
    {
        weapon1.CloseDamageColliders();
    }
	
}
