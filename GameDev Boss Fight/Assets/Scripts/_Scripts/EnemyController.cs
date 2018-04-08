using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour 
{
	//public Transform player;
	public GameObject spawnPoint;

	public Canvas enemyHealth;

	Vector3 Movement;

	void Start ()
	{
		enemyHealth.enabled = false;

	}

	void OnTriggerEnter (Collider other)
	{
		enemyHealth.enabled = true;
	}

}
