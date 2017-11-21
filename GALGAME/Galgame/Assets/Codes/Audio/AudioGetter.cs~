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
	public int changeMode = 0;//切换音乐的模式
	//目前有两种切换音乐的模式
	//第一种：立刻切换 0
	//第二种：等待当前音乐播放完之后切换 1
	//第三种：根本就不切换 2
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
	public MusicController  GetMusicController(bool changeClipImmediate = false)
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
			
				if (theSource.isPlaying == false ||changeClipImmediate )
				{
					//print("the clip ~~~" );
					theSource.Play ();
				}
			} 
		}
		catch 
		{
			try
			{
				theSource = this.transform .Find("musicController").gameObject.GetComponent<AudioSource> ();
				if (theClip != null)
				{
					theSource.clip = this.theClip;
					theSource.Play ();
				}
			}
			catch 
			{
				this.gameObject.AddComponent<AudioSource> ();
				theSource = this.gameObject.GetComponent<AudioSource> ();
				if (theClip != null) {
					theSource.clip = this.theClip;
					theSource.Play ();
				}
			}
		}



		if (theSource.GetComponent <MusicController> ()) 
		{
			//print ("source is got");
			theController = theSource.GetComponent <MusicController> ();
		}
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
			if (theSource != null ) 
			{
				//为了确保每一次的缓慢切换都是正确的并且没有冲突的
				StopCoroutine(theWaitPlay());
				//首先要确保一定有音乐播放
				if(theSource .clip == null && theClip != null)
				{
					theSource.clip = theClip;
				}

				//正常的切换工作
				if(changeMode == 0)
					theSource.clip = theClip;
				else if(changeMode == 1)//等待切换切换为这个音乐
					StartCoroutine(theWaitPlay());
				//一定要做的就是播放
				//如果正在播放就不切换，保持平滑
				if (theSource.isPlaying == false )
					theSource.Play ();

			} 
		}
		catch 
		{
			try
			{
				theSource = this.transform .Find("musicController").gameObject.GetComponent<AudioSource> ();
				print("use child music controller");
				if (theClip != null)
				{

					theSource.clip = this.theClip;
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
	}


	//等待当前音乐播放完成再切换
	IEnumerator theWaitPlay()
	{
		//print ("music change when end");
		//print ("wait time: " +theSource.clip.length);
		yield return new WaitForSeconds (theSource.clip.length);
		theSource.clip = this.theClip;
		theSource.Play ();
	}

	void Start ()
	{
		if(startPlay)
		playerSource ();
		
		//theController = GetMusicController ();
	}
}
