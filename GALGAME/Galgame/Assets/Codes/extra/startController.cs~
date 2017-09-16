using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startController : MonoBehaviour {

	//一开始的时候UI是不生效的
	public GameObject UIRoot;
	public AudioSource theAudioSource;
	private movieController theMovioController;
	// Use this for initialization
	void Start () {
 
			UIRoot.gameObject.SetActive (false);
			theMovioController = this.GetComponent <movieController> ();
			theMovioController.startMovie ();
		if(Application .platform == RuntimePlatform.Android)
		   UIRoot.gameObject.SetActive (true);
	}


	void makeStart()
	{
		UIRoot.gameObject.SetActive (true);
		theAudioSource.gameObject.SetActive (true);
		theAudioSource.Play ();
		theAudioSource.loop = true;
		Destroy (this.gameObject);
	}



	// Update is called once per frame
	void Update () {
		if (Input.anyKey || Input .GetMouseButtonDown(0))
		{
			makeStart ();
		}
	}
}
