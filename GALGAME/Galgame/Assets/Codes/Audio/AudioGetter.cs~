using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGetter : MonoBehaviour {

	//获取全局不销毁音频组件，并且播放音乐
	//适用于单纯地播放背景音乐
	//并且没有额外的操作的场景

	//给出如果找不到全球不销毁唯一音效播放单元的时候能自己增加音效播放单元的获得方法
	//当然能获得唯一播放单元会减少开销
	//如果找不到就会增大开销

	private AudioSource theSource = null;
	private MusicController theController = null;
	//theClip可以为空，表示并不切换
	public AudioClip theClip;
	public bool forceChange = false;//强制切换音乐
	public bool startPlay = true;//开场就播放
	public AudioSource GetSource()
	{
		if (theSource == null)
		{
			this.gameObject.AddComponent<AudioSource> ();
			theSource = this.GetComponent <AudioSource> ();
		}
		return theSource;
	}
		
	//安全保靠的方法
	public MusicController  GetMusicController()
	{
		try
		{
			theSource = GameObject.Find ("/theAudioSourceForAll").GetComponent<AudioSource>();
			if (theSource != null) 
			{
				if (theClip != null)
					theSource.clip = theClip;
				//一定要做的就是播放
				//如果正在播放就不切换，保持平滑
				if (theSource.isPlaying == false)
					theSource.Play ();
			} 
		}
		catch 
		{
			this.gameObject.AddComponent<AudioSource> ();
			theSource = this.gameObject.GetComponent<AudioSource> ();
			if (theClip != null)
			{
				theSource.clip = this.theClip;
				theSource.Play ();
			}
		}


		if(theSource.GetComponent <MusicController>())
			theController =  theSource.GetComponent <MusicController>();
		else
		{
			theSource.gameObject.AddComponent<MusicController> ();
			theController  =  theSource .GetComponent <MusicController>();
		}
		return this.theController;

	}




	public  void playerSource()
	{
		try
		{
			theSource = GameObject.Find ("/theAudioSourceForAll").GetComponent<AudioSource>();
			if (theSource != null) 
			{
				if (theClip != null)
					theSource.clip = theClip;
				//一定要做的就是播放
				//如果正在播放就不切换，保持平滑
				if (theSource.isPlaying == false || forceChange)
					theSource.Play ();
			} 
		}
		catch 
		{
			this.gameObject.AddComponent<AudioSource> ();
			theSource = this.gameObject.GetComponent<AudioSource> ();
			if (theClip != null)
			{
				theSource.clip = this.theClip;
				theSource.Play ();
			}
		}
	}
	void Start ()
	{
		if(startPlay)
		playerSource ();
		//theController = GetMusicController ();
	}
}
