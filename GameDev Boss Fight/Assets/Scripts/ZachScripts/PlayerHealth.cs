using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour 
{
    public static PlayerHealth Instance { get; protected set; }
	public int starterHealth = 100;
	public float currentHealth;
	public Slider healthSlider;
    public Image healthFill;
    Animator anim;

	public bool damaged;

	void Awake () 
	{
        if (Instance != null)
        {
            print("more than one player health script in scene");
        }
        else
        {
            Instance = this;
        }
		currentHealth = starterHealth;
        anim = GetComponentInChildren<Animator>();
    }
	

	void Update () 
	{
		
	}

	public void TakeDamage (float amount)
	{
		damaged = true;
		currentHealth -= amount;
		healthSlider.value = currentHealth;
        //anim.Play("damage_2");
        //anim.applyRootMotion = true;
		if (currentHealth <= 0)
		{
			//Need to make a death function
		}
			
	}
}
