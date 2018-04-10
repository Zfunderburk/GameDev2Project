using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimEventReceiver : MonoBehaviour 
{
    public AP.WeaponHook weapon1;

    public void OpenBossColliders()
    {
        weapon1.OpenDamageColliders();
    }


    public void CloseBossColliders()
    {
        weapon1.CloseDamageColliders();
    }
	
}
