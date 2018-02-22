using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//COMMENTS ARE PLACED UNDERNEATH THE LINE OF CODE THEY ARE REFERING TO
public class PlayerMovement : MonoBehaviour
{

	public Transform playerCam, character, centerPoint;

	private float mouseX, mouseY;
	public float mouseSensitivity =10f;
	public float mouseYPosition = 1f;

	private float moveFB, moveLR;
	public float moveSpeed = 5f;

	private float zoom;
	public float zoomSpeed = 2;

	public float zoomMin = -2f;
	public float zoomMax = -10f;

	public float rotationSpeed = 5f;


	// Use this for initialization
	void Start () 
	{
		zoom = -3;
	}
	
	// Update is called once per frame
	void Update () 
	{
		zoom += Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed; 
		//change this so that it is the up and down on the DPad

		if (zoom > zoomMin)
		{
			zoom = zoomMin;
		}
		if (zoom < zoomMax)
		{
			zoom = zoomMax;
		}

		playerCam.transform.localPosition = new Vector3 (0, 0, zoom);

		if (Input.GetMouseButton (1))
			//Input.GetButton or Joystick button right joystick this is moving the camera
		{
			mouseX += Input.GetAxis ("Mouse X");
			//hopefully same just changed to the right joystick 
			mouseY += Input.GetAxis ("Mouse Y");
			//changing this to being -= will invert the control
		}

		mouseY = Mathf.Clamp (mouseY, -60f, 60f);
		//so that when you look all the way up or down you do not switch to the other 
		playerCam.LookAt (centerPoint);
		centerPoint.localRotation = Quaternion.Euler (mouseY, mouseX, 0);

		moveFB = Input.GetAxis ("Vertical") * moveSpeed; 
		//this will be the left joystick for character movement
		moveLR = Input.GetAxis ("Horizontal") * moveSpeed;

		Vector3 movement = new Vector3 (moveLR, 0, moveFB);
		//we have no jump mechanic so 0 in the Y so we do not move up and down
		movement = character.rotation * movement;

		character.GetComponent<CharacterController> ().Move (movement * Time.deltaTime);
		//using Unity player controller can call Move
		centerPoint.position = new Vector3 (character.position.x, character.position.y + mouseYPosition, character.position.z);

		if (Input.GetAxis("Vertical") > 0||Input.GetAxis("Vertical")  < 0)
		{
			Quaternion turnAngle = Quaternion.Euler (0, centerPoint.eulerAngles.y, 0);
			character.rotation = Quaternion.Slerp (character.rotation, turnAngle, Time.deltaTime * rotationSpeed); 
			//this makes the character rotation to wherever you are moving to smooth
		}

	}
}
