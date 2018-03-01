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
		public bool rt, rb, lt, lb;

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

		public void DetectAction()
		{
			if (canMove == false)
				return;
			
			if (rb == false && rt == false && lb == false && lt == false)
				return;

			string targetAnim = null;

			if(rb)
				targetAnim = "oh_attack_1";
			if(rt)
				targetAnim = "oh_attack_2";
			if(lb)
				targetAnim = "oh_attack_3";
			if(lt)
				targetAnim = "th_attack_1";

			if (string.IsNullOrEmpty (targetAnim))
				return;

			canMove = false;
			inAction = true;
			anim.CrossFade (targetAnim, 0.2f);
			myBody.velocity = Vector3.zero;
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