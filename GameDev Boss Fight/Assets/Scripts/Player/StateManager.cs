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
        public bool rollInput;


        [Header("Stats")]
        public float moveSpeed = 2f;
        public float runSpeed = 6f;
        public float rotateSpeed = 5;
        public float toGround = 0.5f;
        public float rollSpeed = 1;

        [Header("States")]
        public bool run;
        public bool onGround;
        public bool lockOn;
        public bool inAction;
        public bool canMove;

        [Header("Other")]
        public EnemyTarget lockOnTarget;
        public Transform lockOnTransform;
        public AnimationCurve roll_curve;
        public WeaponHook d_hook;
        public WeaponHook g_hook;
        public WeaponHook ls_hook;

        public GameObject activeModel;
        public Animator anim;
        public Rigidbody myBody;
        public AnimatorHook a_hook;

        public float delta;

        public LayerMask ignoreLayers;

        float _actionDelay;

        public void Init()
        {
            SetUpAnimator();
            myBody = GetComponent<Rigidbody>();
            myBody.angularDrag = 999;
            myBody.drag = 4;
            myBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            a_hook = activeModel.GetComponent<AnimatorHook>();
            if (a_hook == null)
            {
                a_hook = activeModel.AddComponent<AnimatorHook>();
            }
            a_hook.Init(this, null);

            gameObject.layer = 8;
            ignoreLayers = ~(1 << 9);

            anim.SetBool("onGround", true);

            ls_hook.CloseDamageColliders(); //in videos this is dealt with on inventory manager which i am not using
            d_hook.CloseDamageColliders();
            g_hook.CloseDamageColliders();
        }

        void SetUpAnimator()
        {
            if (activeModel == null)
            {
                anim = GetComponentInChildren<Animator>();
                if (anim == null)
                {
                    Debug.Log("You forgot a model.");
                }
                else
                {
                    activeModel = anim.gameObject;
                }
            }
            if (anim == null)
            {
                anim = activeModel.GetComponent<Animator>();
            }
            anim.applyRootMotion = false;
        }

        public void FixedTick(float d)
        {
            delta = d;

            DetectAction();

            if (inAction)
            {
                anim.applyRootMotion = true;

                _actionDelay += delta;
                if (_actionDelay > 0.3f)
                {
                    inAction = false;
                    _actionDelay = 0;
                }
                else
                {
                    return;
                }

            }

            canMove = anim.GetBool("canMove");

            if (!canMove)
            {
                return;
            }

            //a_hook.rm_multi = 1;
            a_hook.CloseRoll();
            HandleRolls();


            anim.applyRootMotion = false;

            //myBody.drag = (moveAmount > 0|| onGround ==false) ? 0 : 4; this is the same as the if else underneath
            if (moveAmount > 0 || onGround == false)
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

            Vector3 targetDir = (lockOn == false) ? moveDir //if lockon is false then use move dir
                : (lockOnTransform != null) ? //if it isnt then check this 
                lockOnTransform.transform.position - transform.position : //if it isnt null then use these
                moveDir; // otherwise use the moveDir

            targetDir.y = 0;
            if (targetDir == Vector3.zero)
            {
                targetDir = transform.forward;
            }
            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, delta * moveAmount * rotateSpeed);
            transform.rotation = targetRotation;


            anim.SetBool("lockon", lockOn);

            if (lockOn == false)
                HandleMovementAnimations();
            else
                HandleLockOnAnimations(moveDir);
        }



        //void Combo()
        //{
        //    string targetAnim = null;
        //    if (a)
        //    {
        //        targetAnim = "oh_attack_1";
        //        if (allowCombo)
        //        {
        //            //combo
        //            //Debug.Log("COMBO");
        //        }
        //        else
        //        {
        //            Debug.Log("NOCOMBO");
        //        }
        //        StopCoroutine("WaitTime");
        //        StartCoroutine("WaitTime");
        //    }

        //}

        float timeWindow = 2;
        bool allowCombo = false;
        IEnumerator WaitTime()
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

        public void DetectAction()
        {
            float timer=0;
            
            if (canMove == false)
                return;

            if (a == false)
                return;

            string [] attackAnimtions;
            attackAnimtions = new string[3];
            attackAnimtions[0] = "oh_attack_1";
            attackAnimtions[1] = "oh_attack_2";
            attackAnimtions[2] = "oh_attack_3";

            string targetAnim = null;

            if (a)
            {
                anim.CrossFade(attackAnimtions[0], 0.2f);
                timer = Time.deltaTime;
                
            }
            //Debug.Log(timer);
            //if(timer < 2f)
            //{
            //    if (a)
            //    {
            //        anim.CrossFade(attackAnimtions[1], 0.2f);
            //    }
            //}
            //if (a)
            //{
            //    timer = delta;
            //    targetAnim = "oh_attack_1";
            //    anim.CrossFade(targetAnim, 0.2f);
            //    //StartCoroutine("WaitTime");
            //}

            //if (allowCombo)
            //{
            //    Debug.Log("COMBO");
            //    targetAnim = "oh_attack_2";
            //    anim.CrossFade(targetAnim, 0.2f);
            //    allowCombo = false;
            //}
            
         
            //else
                //Debug.Log(" NO  COMBO");
            //StopCoroutine("WaitTime");
            //StartCoroutine("WaitTime");
        
    
		
			if (string.IsNullOrEmpty (targetAnim))
				return;

			canMove = false;
			inAction = true;
			//anim.CrossFade (targetAnim, 0.2f);
			//myBody.velocity = Vector3.zero;
		}

		public void Tick(float d)
		{
			delta = d;
			onGround = OnGround ();
			anim.SetBool ("onGround", onGround);
		}

		void HandleRolls()
		{
			if (!rollInput)
				return;
			float v = vertical;
			float h = 0;

			/*if (lockOn == false)
			{
				v = (moveAmount > 0.3f) ? 1 : 0; //if moveamount is less than 0.3 then set it to 1 otherwise set to 0
				h = 0;
			}
			else
			{
				if (Mathf.Abs (v) < 0.3f)
					v = 0;
				if (Mathf.Abs (h) < 0.3f)
					h = 0;
			}*/

			if(v!=0)
			{
				if (moveDir == Vector3.zero) // this is another way of doing the rolls but it is worse imo and doesnt work as well
					moveDir = transform.forward;
				Quaternion targetRot = Quaternion.LookRotation (moveDir);
				transform.rotation = targetRot;
				a_hook.InitForRoll ();
				a_hook.rm_multi = rollSpeed;
			}
			else
			{
				a_hook.rm_multi = 1.3f;
			}
			anim.SetFloat ("Vertical", v);
			anim.SetFloat ("Horizontal", h);

			canMove = false;
			inAction = true;
			anim.CrossFade ("Rolls", 0.2f);

		}

		void HandleMovementAnimations()
		{
			anim.SetBool ("run", run);
			anim.SetFloat ("Vertical", moveAmount, 0.4f, delta);
		}

		void HandleLockOnAnimations(Vector3 moveDir)
		{
			Vector3 relativeDir = transform.InverseTransformDirection (moveDir);
			float h = relativeDir.x;	//horizontal
			float v = relativeDir.z;	//vertical

			anim.SetFloat ("Vertical", v, 0.2f, delta);
			anim.SetFloat ("Horizontal", h, 0.2f, delta);
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