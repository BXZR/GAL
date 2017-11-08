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
		else 
		{
			UIUseRoot.gameObject.SetActive (false);
			theMovioController = this.GetComponent <movieController> ();
			theMovioController.startMovie ();
			if (Application.platform == RuntimePlatform.Android) 
			{
				//因为安卓版本的视频播放实际上是使用了另一个程序，所以在播放的过程中就可以将主界面打开了
				UIUseRoot.gameObject.SetActive (true);
				Invoke ("shutDarkStarter" , 1f);//效果就是为了黑屏一秒（但是说实话这个其实也有一点画蛇添足，在update里面已经做了）
				Invoke ("makeStart", 1f);//如果在那之前没有被“开始”，就自动开始
			}
		}

		//重置标记
		systemInformations.makeFlashForScene();
	}

	void shutDarkStarter()
	{
		DarkStarter.gameObject.SetActive (false);
	}
	void makeStart()
	{
		//DarkStarter.gameObject.SetActive (false);
		//这里有一个渐变的效果，这个原先是是一个直接关闭
		DarkStarter.GetComponent<effectSlowIn>().makeChangeOut(1.5f);
	    UIUseRoot.gameObject.SetActive (true);
		theAudioSource.gameObject.SetActive (true);
		theAudioSource.Play ();
		theAudioSource.loop = true;
	    isStarted = true;
		configController.createConfigFileIfNull ();//如果没有配置文件就建立默认的配置文件
		SceneModeFile.InitValues();
		CGModeFile.makeAllStart();//生成CG文件
		Invoke("trueStart",1.75f);
	}

	//第二段的收尾工作
	void trueStart()
	{
		//print ("darkStarterEnded");
		DarkStarter.gameObject.SetActive (false);
		Destroy (this.gameObject);
	}

	float timer = 0f;//需要等待一段时间否则不可以有效果
	float timerMax = 1f;//一个简单的阀值
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
		if (timer > timerMax)
		{
			if (Input.anyKey || Input.GetMouseButtonDown (0)) 
			{
				makeStart ();
			}
		}
	}
}
