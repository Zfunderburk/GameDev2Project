using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AP
{
	//Time first half on part 3

public class StateManager : MonoBehaviour 
	{
		[Header("Input")]
		public float vertical, horizontal;
		public float moveAmount;
		public Vector3 moveDir;
		//public bool rt, rb, lt, lb;
		public bool a, x, y;

		[Header("Stats")]
		public float moveSpeed = 2f;
		public float runSpeed = 3.5f;
		public float rotateSpeed = 5;
		public float toGround = 0.5f;

		[Header("States")]
		public bool run;
		public bool onGround;
		public bool lockOn;
		public bool inAction;
		public bool canMove;

		public GameObject activeModel;
		public Animator anim;
		public Rigidbody myBody;
		public AnimatorHook a_hook;

		public float delta;

		public LayerMask ignoreLayers;

		float _actionDelay;

		public void Init()
		{
			SetUpAnimator ();
			myBody = GetComponent<Rigidbody> ();
			myBody.angularDrag = 999;
			myBody.drag = 4;
			myBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

			a_hook = activeModel.AddComponent<AnimatorHook>();
			a_hook.Init (this);

			gameObject.layer = 8;
			ignoreLayers = ~(1 << 9);

			anim.SetBool ("onGround", true);
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
	
			DetectAction ();

			if (inAction)
			{
				anim.applyRootMotion = true;

				_actionDelay += delta;
				if(_actionDelay > 0.3f)
				{
					inAction = false;
					_actionDelay = 0;
				}
				else
				{
					return;
				}

			}

			canMove = anim.GetBool ("canMove");

			if(!canMove)
			{
				return;
			}

			anim.applyRootMotion = false;

			//myBody.drag = (moveAmount > 0|| onGround ==false) ? 0 : 4; this is the same as the if else underneath
			if (moveAmount > 0|| onGround == false)
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
			if (onGround)
			{
				myBody.velocity = moveDir * (targetSpeed * moveAmount);
			}

			if (run)
			{
				lockOn = false;
			}

			if (!lockOn)
			{
				Vector3 targetDir = moveDir;
				targetDir.y = 0;
				if (targetDir == Vector3.zero)
				{
					targetDir = transform.forward;
				}
				Quaternion tr = Quaternion.LookRotation (targetDir);
				Quaternion targetRotation = Quaternion.Slerp (transform.rotation, tr, delta * moveAmount * rotateSpeed);
				transform.rotation = targetRotation;
			}


			HandleMovementAnimations ();
		}





		float timeWindow = 2;
		bool allowCombo = false;
		IEnumerator WaitTime ()
		{
			allowCombo = true;
			float timeElapsed = 0;
			while (timeElapsed < timeWindow)
			{
				timeElapsed += Time.deltaTime;
				yield return null;
			}

			allowCombo = false;
		}

		void Combo()
		{
			string targetAnim = null;
			if (a)
			{
				targetAnim = "oh_attack_1";
				if (allowCombo)
				{
					//combo
					Debug.Log("COMBO");
				}
				else 
				{
					Debug.Log ("NOCOMBO");
				}
				StopCoroutine ("WaitTime");
				StartCoroutine ("WaitTime");
			}

		}


		public void DetectAction()
		{
			float betweenAtt = 0;
			if (canMove == false)
				return;
			
			if (a == false)
				return;

			string targetAnim = null;
			/*
			if (a) 
			{
				


				targetAnim = "oh_attack_1";



				if (allowCombo)
				{

					// Combo
					Debug.Log ("COMBO");
				}
				else
					Debug.Log (" NO  COMBO");

			
				StopCoroutine ("WaitTime");
				StartCoroutine ("WaitTime");
			}*/
				
			
		

			if (string.IsNullOrEmpty (targetAnim))
				return;

			canMove = false;
			inAction = true;
			anim.CrossFade (targetAnim, 0.2f);
			//myBody.velocity = Vector3.zero;
		}

		public void Tick(float d)
		{
			delta = d;
			onGround = OnGround ();
			anim.SetBool ("onGround", onGround);
		}

		void HandleMovementAnimations()
		{
			anim.SetBool ("run", run);
			anim.SetFloat ("Vertical", moveAmount, 0.4f, delta);
		}

		public bool OnGround()
		{
			bool r = false;

			Vector3 origin = transform.position + (Vector3.up * toGround);
			Vector3 dir = -Vector3.up;
			float dis = toGround + 0.3f;
			RaycastHit hit;
			if (Physics.Raycast (origin, dir, out hit, dis,ignoreLayers))
			{
				r = true;
				Vector3 targetPosition = hit.point;
				transform.position = targetPosition;
			}

			return r;
		}

	}
}