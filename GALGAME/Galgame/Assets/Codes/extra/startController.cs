using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startController : MonoBehaviour {

	//一开始的时候UI是不生效的
	public GameObject UIUseRoot;
	private AudioSource theAudioSource;
	public GameObject DarkStarter;//最开始的黑色屏幕
	public GameObject theStartSceneEffect;//粒子效果
	private movieController theMovioController;
	// Use this for initialization
	private  AudioGetter theAudioGetter;//真正控制唯一不销毁音乐控制单元的控制器
	private  static bool isStarted = false;
	private  bool isStartedThisTurn = false;//本次打开是不是已经开始
	void Start () {
		theAudioGetter = this.GetComponent <AudioGetter> ();
		theAudioSource = theAudioGetter.GetSource ();
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
		theAudioGetter.playerSource ();
		//DarkStarter.gameObject.SetActive (false);
		//这里有一个渐变的效果，这个原先是是一个直接关闭
		//花上两秒的时间渐入时间有一点长了
		DarkStarter.GetComponent<effectSlowIn>().makeChangeOut(1.25f);
	    UIUseRoot.gameObject.SetActive (true);
		theStartSceneEffect.SetActive (true);
		//没有必要做这一步，当然求稳健的话是可以的
		//theAudioSource.gameObject.SetActive (true);
		theAudioSource.Play ();
		theAudioSource.loop = true;
		if(!isStarted)
		{
		//以下功能关乎文件，但是很多东西都被保存在了静态变量中
		//所以从文件进行初始化的功能只在最开始的时候做一次就可以了
		  configController.createConfigFileIfNull ();//如果没有配置文件就建立默认的配置文件
		  SceneModeFile.InitValues();
		  CGModeFile.makeAllStart();//生成CG文件
		  DeathFile.makeAllStart();
		  systemInformations.makeOlotOverAllCount ();//获得总剧本长度
		  systemInformations.LoadPlotOver();
		  extraHFile.makeAllStart ();//额外特典标记
		}
		systemInformations.flash ();//默认回到初始界面时间scale变回原状
		isStarted = true;
		isStartedThisTurn = true;
		Invoke("trueStart",1.5f);
	}

	//第二段的收尾工作
	void trueStart()
	{
		//print ("darkStarterEnded");
		DarkStarter.gameObject.SetActive (false);
		this.enabled = false;//因为在这个物体上面有协程调用，如果直接Destroy携程也会跟着关闭
		//因此采用了比较折中的做法，用enable关闭减少update中的判断
	}

	float timer = 0f;//需要等待一段时间否则不可以有效果
	float timerMax = 1f;//一个简单的阀值

	//这个功能是为了给出一种操作的解决方案
	void Update () 
	{
		//UIUseRoot.gameObject.activeInHierarchy是一个标记
		//实际上这个可以用一个额外的标记但是用这个物体作为标记就可以了
		if (! isStartedThisTurn )
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
}
