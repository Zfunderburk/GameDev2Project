using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour 
{
	public Transform PlayerPos;

	public EnemyController control;

	void Update()
	{
		if (!control.canMove)
			return;
		else
		{
			transform.LookAt (PlayerPos);
		}
	}

	
}
