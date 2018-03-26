using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AP
{
	public class AIHandler : MonoBehaviour 
	{
		public AIAttacks[] ai_attacks;

		public EnemyStates estates;

		void Start()
		{
			
		}

		void Update()
		{
			
		}
	}


	[System.Serializable]
	public class AIAttacks
	{
		public int weight;
		public float minAngle;
		public float maxAngle;
		public string targetAnim;
	}
}

