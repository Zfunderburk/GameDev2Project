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
		public float lossSpeed = 15f;
		public float gainSpeed = 20f; 
		public Slider staminaSlider;
		public Image staminaFill;

		public Animator anim;

		void Start () 
		{
			currentStamina = maxStamina;
		}


		void Update ()
		{
			CalculateStamina();
			//Debug.Log (currentStamina);
		}

		void CalculateStamina ()
		{
			if(Input.GetKey(KeyCode.LeftShift))
			{
				staminaSlider.value -= lossSpeed * Time.deltaTime;
				currentStamina -= lossSpeed * Time.deltaTime;
			}

			else 
			{
				currentStamina += gainSpeed * Time.deltaTime;
				staminaSlider.value += gainSpeed * Time.deltaTime;

			}

			currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

			if(currentStamina <= 1)
			{
				anim.SetBool ("run", false);
			}

		}
	}

}

