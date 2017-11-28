using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个类抓木门用于控制背景音乐 
[RequireComponent (typeof(AudioSource))]
public class MusicController : MonoBehaviour {

	private AudioSource theBackMusicController;

	//记录下一个plotItem的audioSource并且异步加载
	//换音乐的时候如果名字相同就可以直接赋值
	//这只是一个非常单一的缓冲
	//毕竟音乐比较大，过大的缓冲本身会不会对游戏性能造成影响恐怕也需要考虑一下
	private AudioClip theClipPool;//目标音乐池

	public void playBackMusic(thePlotItem InP)
	{
		//print (InP.musicName +"-------------------------------");
		playBackMusic(InP .musicName);
	}

	public AudioClip theClipNow ;

	//直接传入clip的模式
	public void playWithClip(AudioClip theClip, bool smoothChange = true)
	{
		theClipNow  = theClip;
		if (theClipNow  == null) 
		{
			//print ("没有加载成功");
			return;
		}
		if(theBackMusicController == null)
			theBackMusicController = this.GetComponent <AudioSource> ();
		systemInformations. theBackMusicNameNow = theClip.name;

		if (smoothChange == false)
		{
			//立即加载切换
			theBackMusicController.clip = theClipNow;
			theBackMusicController.Play ();
		}
		else {
			//渐变式切换
			StartCoroutine (smoothVolumeChange ());
		}
	}

	//只有在与当前播放的音乐名字不同的时候才会调用
	//因此在这里已经不需要判断
	public void playBackMusic(string nameIn  , bool smoothChange = true)
	{
		//没有输入或者没有不安化就不加载
		//print("loadMusic -- " + nameIn );
		if (string.IsNullOrEmpty (nameIn) || nameIn.Equals (systemInformations.theBackMusicNameNow))
			return;

		//如果这个目标音乐正好是上一个被保存目标音乐
		//就直接赋值
		if (theClipPool != null && theClipPool.name == nameIn)
		{
			theClipNow = theClipPool;
			return;
		}
		//如果没有就需要加在音乐
		theClipPool = theClipNow;
		theClipNow  = Resources .Load("music/"+ nameIn) as AudioClip;
		if (theClipNow  == null) 
		{
			print ("背景音乐没有加载成功");
			return;
		}
		//print ("has the clip "+ theClipNow.name);
		if(theBackMusicController == null)
			theBackMusicController = this.GetComponent <AudioSource> ();
		//print (theBackMusicController.gameObject .name +" has audiosSource");
		systemInformations.theBackMusicNameNow = nameIn;

		if (smoothChange == false)
		{
			//立即加载切换
			theBackMusicController.clip = theClipNow;
			theBackMusicController.Play ();
		}
		else {
			//渐变式切换
			//StartCoroutine (smoothVolumeChange ());
			StartCoroutine( "smoothChange", 0.8f);
		}
	}

	//这是一种柔和的声音渐变方式
	IEnumerator smoothVolumeChange()
	{
		if(theBackMusicController == null)
			theBackMusicController = this.GetComponent <AudioSource> ();
		//记录当前声音数值
		//同时也是减少数量的百分比加成
		//1总音量的时候每0.1秒减少0.1
		//0.4总音量的时候每0.1秒减少0.04
		float volumeSave = theBackMusicController.volume;

		for (int i = 0; i < 5; i++) 
		{
			yield return new WaitForSeconds (0.1f);
			theBackMusicController.volume -= 0.2f * volumeSave;
		}
		theBackMusicController.clip = theClipNow ;
		theBackMusicController.Play ();
		for (int i = 0; i < 8; i++) 
		{
			yield return new WaitForSeconds (0.1f);
			theBackMusicController.volume += 0.2f * volumeSave;
		}
	}

	//这是使用插值法做的声音柔和渐变的方法2
	//duration是声音变化使用的总时间
	//因此有两个部分，声音变小和声音变大
	IEnumerator smoothChange(float duration)
	{
		if(theBackMusicController == null)
			theBackMusicController = this.GetComponent <AudioSource> ();
		float volumeSave = theBackMusicController.volume;
		float timePart = duration / 2;
		float timerNow = 0f;
		float step = 0.02f;
		while (timerNow < timePart) 
		{
			//timerNow / timePart 补间律
			theBackMusicController.volume = Mathf.Lerp (theBackMusicController.volume, 0.0f, timerNow / timePart);
			yield return new WaitForSeconds (step);
			timerNow += step;
		}
		theBackMusicController.clip = theClipNow ;
		theBackMusicController.Play ();
	    //接下来倒着算一次
		while (timerNow > 0) 
		{
			theBackMusicController.volume = Mathf.Lerp (theBackMusicController.volume, volumeSave, (timePart - timerNow )/ timePart);
			yield return new WaitForSeconds (step);
			timerNow -= step;	
		}
	}


	// Use this for initialization
	void Start () {
		theBackMusicController = this.GetComponent <AudioSource> ();
		if(theBackMusicController != null)
		theBackMusicController.loop = true;
	}

	//注销，减少回调个数
	// Update is called once per frame
	//void Update () {
		
	//}
}
