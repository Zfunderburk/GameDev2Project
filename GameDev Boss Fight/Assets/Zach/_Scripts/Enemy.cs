using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
	public Transform PlayerPos;

	private bool canMove;
	private float range;
	private float minDistance = 1f;
	private float speed = 3f;

	Vector3 Movement;


	// Use this for initialization
	void Start () 
	{
		canMove = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (canMove = false)
		{
			Movement = Vector3.zero;
		}
		else
		{
			transform.LookAt (PlayerPos);
			range = Vector3.Distance (transform.position, PlayerPos.position);
			if (range > minDistance)
			{
				transform.position = Vector3.MoveTowards (transform.position, PlayerPos.position, speed * Time.deltaTime);
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		canMove = true;
	}
}
