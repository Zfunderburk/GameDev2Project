using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AP
{
	//Time 51.02 on part 2

public class StateManager : MonoBehaviour 
	{
		[Header("INput")]
		public float vertical, horizontal;
		public float moveAmount;
		public Vector3 moveDir;

		[Header("Stats")]
		public float moveSpeed = 2f;
		public float runSpeed = 3.5f;
		public float rotateSpeed = 5;

		[Header("States")]
		public bool run;

		public GameObject activeModel;
		public Animator anim;
		public Rigidbody myBody;


		public float delta;

		public void Init()
		{
			SetUpAnimator ();
			myBody = GetComponent<Rigidbody> ();
			myBody.angularDrag = 999;
			myBody.drag = 4;
			myBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		}

		void SetUpAnimator()
		{
			if (activeModel == null)
			{
				anim = GetComponentInChildren<Animator> ();
				if (anim == null)
				{
					Debug.Log ("You forgot a model.");
				}
				else
				{
					activeModel = anim.gameObject;
				}
			}
			if (anim == null)
			{
				anim = activeModel.GetComponent<Animator> ();
			}
			anim.applyRootMotion = false;
		}

		public void FixedTick(float d)
		{
			delta = d;


			//myBody.drag = (moveAmount > 0) ? 0 : 4; this is the same as the if else underneath
			if (moveAmount > 0)
			{
				myBody.drag = 0;
			}
			else
			{
				myBody.drag = 4;
			}

			float targetSpeed = moveSpeed;
			if (run)
			{
				targetSpeed = runSpeed;
			}

			myBody.velocity = moveDir * (targetSpeed * moveAmount);
			HandleMovementAnimations ();
		}

		void HandleMovementAnimations()
		{
			anim.SetFloat ("Vertical", moveAmount, 0.4f, delta);
		}

	}
}