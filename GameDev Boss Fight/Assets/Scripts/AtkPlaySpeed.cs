using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkPlaySpeed : MonoBehaviour 
{
	public Animator anim;

//	public GameObject[] wepTag;

	public float atkSpeed = 1f;

	void Update ()
	{
		anim.SetFloat("attackSpeed", atkSpeed);


//		wepTag = GameObject.FindGameObjectsWithTag("SmlWep");
//		wepTag = GameObject.FindGameObjectsWithTag("MidWep");
//		wepTag = GameObject.FindGameObjectsWithTag("LrgWep");
//
//		if(gameObject.tag == "SmlWep")
//		{
//			anim.SetFloat("attackSpeed", 1.5f);
//		}
//
//		else if(gameObject.tag == "MidWep")
//		{
//			anim.SetFloat("attackSpeed", 1f);
//		}
//
//		else if(gameObject.tag == "LrgWep")
//		{
//			anim.SetFloat("attackSpeed", .5f);
//		}
	}


}
