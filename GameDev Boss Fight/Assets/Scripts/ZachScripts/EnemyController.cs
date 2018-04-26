using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour 
{
	//public Transform player;
	public GameObject spawnPoint;



	public Canvas enemyHealth;

	public bool canMove;
	private float range;
	private float minDistance = 1f;
	private float speed = 3f;

	Vector3 Movement;


	void Start ()
	{
		enemyHealth.enabled = false;
		canMove = false;

	}

	public void OnTriggerEnter (Collider other)
	{
		enemyHealth.enabled = true;
		canMove = true;

		if(other.gameObject.tag == "Player")
		{
			gameObject.GetComponent<WeaponParent>().enabled = false;
		}
			
	}

}
