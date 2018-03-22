using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour 
{
//	public StateManager

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
