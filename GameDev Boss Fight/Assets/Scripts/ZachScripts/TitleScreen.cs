using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AP
{

public class TitleScreen : MonoBehaviour {

	public Canvas playerStat;
	public Canvas title;

	public GameObject camManager;


	void Start () 
	{
		playerStat.enabled = false;
		camManager.GetComponent<CameraManager>().enabled = false;
	}

	public void Play ()
	{
		playerStat.enabled = true;
		title.enabled = false;
		camManager.GetComponent<CameraManager>().paused = true;
	}


}

}
