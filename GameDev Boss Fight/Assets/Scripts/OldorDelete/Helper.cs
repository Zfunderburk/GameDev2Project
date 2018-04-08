using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AP
{
	public class Helper : MonoBehaviour
{	

		[Range(0,1)]
		public float Vertical;
		[Range(-1,1)]
		public float Horizontal;

		public bool playAnim;
		public string[] oh_attacks;
		public string[] th_attacks;

		public bool twoHanded;
		public bool enableRootMotion;
		public bool useItem;
		public bool interacting;
		public bool lockon;

		Animator anim;
	
		// Use this for initialization
		void Start ()
		{
			anim = GetComponent<Animator> ();
		}
	
		// Update is called once per frame
		void Update () 
		{
			
			enableRootMotion = !anim.GetBool ("canMove");
			anim.applyRootMotion = enableRootMotion;

			interacting = anim.GetBool ("interacting");

			if (lockon==false)
			{
				Horizontal = 0;
				Vertical = Mathf.Clamp01 (Vertical);
			}

			anim.SetBool ("lockon", lockon);


			if (enableRootMotion)
			{
				return;
			}

			if (useItem)
			{
				anim.Play ("use_item");
				useItem = false;
			}

			if (interacting)
			{
				playAnim = false;
				Vertical = Mathf.Clamp (Vertical, 0, 0.5f); //can not run while using item
			}

			anim.SetBool ("two_handed", twoHanded);

			if (playAnim)
			{
				string targetAnim;

				if(!twoHanded)
				{
					int r = Random.Range (0, oh_attacks.Length);
					targetAnim = oh_attacks [r];

					if (Vertical > 0.5f)
					{
						targetAnim = "oh_attack_3"; 
					}
				}
				else
				{
					int r = Random.Range (0, th_attacks.Length);
					targetAnim = th_attacks [r];
				}

				if (Vertical > 0.5f)
				{
					targetAnim = "oh_attack_3"; 
				}

				Vertical = 0;

				anim.CrossFade (targetAnim, 0.2f);
	
				playAnim = false;
			}
			anim.SetFloat ("Vertical", Vertical);
			anim.SetFloat ("Horizontal", Horizontal);


		}
	}
}
