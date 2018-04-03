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


		void Start () 
		{
			currentStamina = maxStamina;
		}


		void Update ()
		{
			CalculateStamina();
		}

		void CalculateStamina ()
		{
			if(Input.GetKey(KeyCode.LeftShift))
			{
				staminaSlider.value -= lossSpeed * Time.deltaTime;
			}

			else 
			{
//				StartCoroutine(StaminaPause());
				staminaSlider.value += gainSpeed * Time.deltaTime;
			}

//			currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

			if (currentStamina >= maxStamina)
			{
				currentStamina = maxStamina;
			}

			else if(currentStamina <= 0)
			{
				currentStamina = 0;
				states.run = false;
			}

		}

		IEnumerator StaminaPause ()
		{
			print ("Pause");
			yield return new WaitForSeconds(10);
			print ("End Pause");
		}



	}

}

//		void SetStaminaBar (float myStamina)
//		{
//			StaminaFill.transform.localScale = new Vector3 (myStamina, StaminaFill.transform.localScale.y, StaminaFill.transform.localScale.z);
//
//		}

//			float calcStam = currentStamina / maxStamina;
//
//			if(calcStam <= 0)
//			{
//				calcStam = 0;
//			}
//
//			SetStaminaBar (calcStam);
