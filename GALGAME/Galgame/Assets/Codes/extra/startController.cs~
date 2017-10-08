using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startController : MonoBehaviour {

	//一开始的时候UI是不生效的
	public GameObject UIUseRoot;
	public AudioSource theAudioSource;
	public GameObject DarkStarter;//最开始的黑色屏幕
	private movieController theMovioController;
	// Use this for initialization


	private  static bool isStarted = false;

	void Start () {
		if (isStarted)
			makeStart ();
		//片头曲只会播放一次
		else {
			UIUseRoot.gameObject.SetActive (false);
			theMovioController = this.GetComponent <movieController> ();
			theMovioController.startMovie ();
			if (Application.platform == RuntimePlatform.Android) 
			{
				//因为安卓版本的视频播放实际上是使用了另一个程序，所以在播放的过程中就可以将主界面打开了
				UIUseRoot.gameObject.SetActive (true);
				DarkStarter.gameObject.SetActive (false);
			}
		}
	}


	void makeStart()
	{
		DarkStarter.gameObject.SetActive (false);
	    UIUseRoot.gameObject.SetActive (true);
		theAudioSource.gameObject.SetActive (true);
		theAudioSource.Play ();
		theAudioSource.loop = true;
		Destroy (this.gameObject);
	    isStarted = true;
	}



	// Update is called once per frame
	void Update () {
		if (Input.anyKey || Input .GetMouseButtonDown(0))
		{
			makeStart ();
		}
	}
}
