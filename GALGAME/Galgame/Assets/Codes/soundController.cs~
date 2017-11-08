using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个类专门用来播放音效
//同时也可以用来播放语音音效
public class soundController : MonoBehaviour {

	private  AudioSource theSource;
	private  AudioClip 	theClip ;

	private  void makeStart()
	{
		theSource = this.GetComponent <AudioSource> ();
		if (theSource == null)
			theSource = this.transform.root.GetComponent<AudioSource> ();
	}
 
	//ispPeople如果是true就说明是语音
	public void playSound(thePlotItem InP , bool isPeople = false)
	{
		if(theSource!= null)
		theSource.Stop ();
			//没有输入或者没有不安化就不加载
			//print("loadMusic -- " + nameIn);
		AudioClip theClip  = null;
		if (!isPeople) 
		{
			string nameIn = InP.soundName;
			if (string.IsNullOrEmpty (nameIn))
				return;
			 theClip = Resources.Load ("Sounds/" + nameIn) as AudioClip;
		}
		else
		{
			string nameIn = InP.speakSoundName;
			if (string.IsNullOrEmpty (nameIn))
				return;
			theClip = Resources.Load ("Speaks/" + nameIn) as AudioClip;
		}

		if (theClip == null  ) 
		{
			//print ("没有加载成功");
			return;
		}
		else 
		{
			makeStart ();
			theSource.PlayOneShot (theClip);
			//print ("play sound");
		}
 
	}

	void Start () 
	{
		makeStart ();
	}
	
 
}
