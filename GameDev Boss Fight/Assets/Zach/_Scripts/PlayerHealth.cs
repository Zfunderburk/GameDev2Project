using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour 
{

	public int starterHealth = 100;
	public float currentHealth;
	public Slider healthSlider;
	public Image healthFill;

	public bool damaged;

	void Awake () 
	{
		currentHealth = starterHealth;
	}
	

	void Update () 
	{
		
	}

	public void TakeDamage (int amount)
	{
		damaged = true;
		currentHealth -= amount;
		healthSlider.value = currentHealth;
		if (currentHealth <= 0)
		{
			//Need to make a death function
		}
			
	}
}
