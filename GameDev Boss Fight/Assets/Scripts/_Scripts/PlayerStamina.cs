using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AP
{

	public class PlayerStamina : MonoBehaviour 
	{
	StateManager states;

	public int starterStamina = 100;
	public float currentStamina;
	public Slider StaminaSlider;
	public Image StaminaFill;

	public bool damaged;

	void Awake () 
	{
		currentStamina = starterStamina;
	}


	void Update () 
	{

	}

	}

}
