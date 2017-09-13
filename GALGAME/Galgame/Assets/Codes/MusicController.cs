using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个类抓木门用于控制背景音乐和音效
public class MusicController : MonoBehaviour {

	private AudioSource theBackMusicController;
	private string theBackMusicNameNow = "";//为了防止额外的多次加载，做一个小小的判定

	public void playBackMusic(thePlotItem InP)
	{
		playBackMusic(InP .musicName);
	}

	public AudioClip theClipNow ;

	public void playBackMusic(string nameIn)
	{
		if (string.IsNullOrEmpty (nameIn) || nameIn.Equals (theBackMusicNameNow))
			return;
		//没有输入或者没有不安化就不加载
		print("loadMusic -- " + nameIn);
		theClipNow  = Resources .Load("music/"+ nameIn) as AudioClip;
		if (theClipNow  == null) 
		{
			print ("没有加载成功");
			return;
		}
		theBackMusicController.clip = theClipNow ;
		theBackMusicController.Play ();
	}

	// Use this for initialization
	void Start () {
		theBackMusicController = this.GetComponent <AudioSource> ();
		theBackMusicController.loop = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
