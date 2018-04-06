using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkPlaySpeed : MonoBehaviour 
{
	public Animator anim;

	public float atkSpeed = 1f;

	void Update ()
	{
		anim.SetFloat("attackSpeed", atkSpeed);
	}


}
