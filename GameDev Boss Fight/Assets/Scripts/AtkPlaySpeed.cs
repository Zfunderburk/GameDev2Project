using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkPlaySpeed : MonoBehaviour 
{
	public Animator anim;
	public Animation atk;

	public float atkSpeed;

	void Start () 
	{
		anim = GetComponent<Animation>();
	}

	void Update ()
	{
		atk["oh_attack_1"].speed = atkSpeed;
	}


}
