using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AP
{
	public class PlayerStamina : MonoBehaviour 
	{
		StateManager states;

		public float maxStamina = 100f;
		public float currentStamina;
		public float lossSpeed = 3f;
		public float gainSpeed = 5f; 
		public Slider StaminaSlider;
		public Image StaminaFill;

		public StateManager run;

		void Start () 
		{
			StaminaSlider = GetComponent<Slider>();
			currentStamina = maxStamina;
		}


		void Update ()
		{
			CalculateStamina();
		}

		void CalculateStamina ()
		{
			float calcStam = currentStamina / maxStamina;

			if(calcStam <= 0)
			{
				calcStam = 0;
			}

			SetStaminaBar (calcStam);
		}

		void SetStaminaBar (float myStamina)
		{
			StaminaFill.transform.localScale = new Vector3 (myStamina, StaminaFill.transform.localScale.y, StaminaFill.transform.localScale.z);

		}



	}

}
