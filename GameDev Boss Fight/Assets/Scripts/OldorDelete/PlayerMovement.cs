using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//COMMENTS ARE PLACED UNDERNEATH THE LINE OF CODE THEY ARE REFERING TO
public class PlayerMovement : MonoBehaviour
{

	public Transform playerCam, character, centerPoint;

	private float mouseX, mouseY;
	private float moveX, moveY;
	public float mouseSensitivity =10f;
	public float mouseYPosition = 1.5f;

	private float moveFB, moveLR;
	public float moveSpeed = 5f;

	private float zoom;
	public float zoomSpeed = 1;

	public float zoomMin = -2f;
	public float zoomMax = -10f;

	public float rotationSpeed = 10f;

	public float dodgeDistance = 5f;


	// Use this for initialization
	void Start () 
	{
		zoom = -5;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButton("DPD"))
		{
			zoom -= zoom + zoomSpeed;
		}
		if (Input.GetButton("DPUP"))
		{
			zoom += zoom - zoomSpeed;
		}
			
		if (zoom > zoomMin)
		{
			zoom = zoomMin;
		}
		if (zoom < zoomMax)
		{
			zoom = zoomMax;
		}

		playerCam.transform.localPosition = new Vector3 (0, 0, zoom);


		if (Input.GetAxis("RightJoyLR") < 0 ||Input.GetAxis("RightJoyLR") > 0 )
		{
			moveX += Input.GetAxis ("RightJoyLR");
			//changing this to being -= will invert the control
		}
		if (Input.GetAxis ("RightJoyUD") < 0 || Input.GetAxis ("RightJoyUD") > 0) 
		{
			moveY += Input.GetAxis ("RightJoyUD");
			//changing this to being -= will invert the control
		}
		//This is to control the camera using the right stick of a controller

		moveY = Mathf.Clamp (moveY, -60f, 60f);
		//so that when you look all the way up or down you do not switch to the other 

		playerCam.LookAt (centerPoint);
		centerPoint.localRotation = Quaternion.Euler (moveY, moveX, 0);

		moveFB = Input.GetAxis ("Vertical") * moveSpeed; 
		//this will be the left joystick for character movement
		moveLR = Input.GetAxis ("Horizontal") * moveSpeed;

		Vector3 movement = new Vector3 (moveLR, 0, moveFB);
		//we have no jump mechanic so 0 in the Y so we do not move up and down
		movement = character.rotation * movement;
		//changes direction based off of the camera rotation

		character.GetComponent<CharacterController> ().Move (movement * Time.deltaTime);
		//using Unity player controller can call Move
		centerPoint.position = new Vector3 (character.position.x, character.position.y + mouseYPosition, character.position.z);
		//so that the center point will follow and be where the character is 

		if (Input.GetAxis("Vertical") > 0||Input.GetAxis("Vertical")  < 0)
		{
			Quaternion turnAngle = Quaternion.Euler (0, centerPoint.eulerAngles.y, 0);
			character.rotation = Quaternion.Slerp (character.rotation, turnAngle, Time.deltaTime * rotationSpeed); 
			//this makes the character rotation to wherever you are moving to smooth
		}


		//DODGING
		if (Input.GetButton("Dodge"))
		{
			Debug.Log("Button Works");
			//base case no directional input



			if (Input.GetAxis("Vertical") > 0||Input.GetAxis("Vertical")  < 0)
			{
				//moving on the z postition
				//up and down
			}
			if (Input.GetAxis("Horizontal") > 0||Input.GetAxis("Horizontal")  < 0)
			{
				// moving on the x position
				//left and right
			}
		}

	}
}
