using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个类抓木门用于控制背景音乐 
public class MusicController : MonoBehaviour {

	private AudioSource theBackMusicController;
	private string theBackMusicNameNow = "";//为了防止额外的多次加载，做一个小小的判定


	public void playBackMusic(thePlotItem InP)
	{
		playBackMusic(InP .musicName);
	}

	public AudioClip theClipNow ;

	//只有在与当前播放的音乐名字不同的时候才会调用
	//因此在这里已经不需要判断
	public void playBackMusic(string nameIn  , bool smoothChange = true)
	{
		if (string.IsNullOrEmpty (nameIn) || nameIn.Equals (theBackMusicNameNow))
			return;
		//没有输入或者没有不安化就不加载
		//print("loadMusic -- " + nameIn);
		theClipNow  = Resources .Load("music/"+ nameIn) as AudioClip;
		if (theClipNow  == null) 
		{
			//print ("没有加载成功");
			return;
		}
		if(theBackMusicController == null)
			theBackMusicController = this.GetComponent <AudioSource> ();
		theBackMusicNameNow = nameIn;

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

	// Use this for initialization
	void Start () {
		theBackMusicController = this.GetComponent <AudioSource> ();
		theBackMusicController.loop = true;
	}

	//注销，减少回调个数
	// Update is called once per frame
	//void Update () {
		
	//}
}
