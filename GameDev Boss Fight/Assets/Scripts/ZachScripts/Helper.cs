using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour {

	Animator anim;

	[Range(0, 1)]
	public float vertical;


	void Start () 
	{
		anim = GetComponent<Animator> ();
	}


	void Update () 
	{

		anim.SetFloat ("Vertical", vertical);

	}
}
