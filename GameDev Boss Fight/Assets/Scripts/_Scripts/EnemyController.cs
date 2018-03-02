using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour 
{
	public GameObject enemy;
	public GameObject spawnPoint;



	public Canvas enemyHealth;

	private bool canMove;
	private float range;
	private float minDistance = 1f;
	private float speed = 3f;

	Vector3 Movement;


	void Start ()
	{
		enemyHealth.enabled = false;

	}

	void OnTriggerEnter (Collider other)
	{
		enemyHealth.enabled = true;
		canMove = true;
	}

	void Update()
	{
		
	}
}
