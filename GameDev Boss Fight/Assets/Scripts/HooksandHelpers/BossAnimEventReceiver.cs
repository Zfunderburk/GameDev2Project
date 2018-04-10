using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimEventReceiver : MonoBehaviour 
{
    public AP.WeaponHook weapon1;

	//private void Start()
	//{
 //       weapon1.damColliders[].enabled = false;
	//}

	public void OpenBossColliders()
    {
        weapon1.OpenDamageColliders();
    }


    public void CloseBossColliders()
    {
        weapon1.CloseDamageColliders();
    }
	
}
