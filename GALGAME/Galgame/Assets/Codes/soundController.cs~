using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个类专门用来播放音效
public class soundController : MonoBehaviour {

	private  AudioSource theSource;
	private  AudioClip 	theClip ;

	private  void makeStart()
	{
		theSource = this.GetComponent <AudioSource> ();
		if (theSource == null)
			theSource = this.transform.root.GetComponent<AudioSource> ();
	}
 
	public void playSound(thePlotItem InP)
	{
		theSource.Stop ();
		string nameIn = InP.soundName;
         if (string.IsNullOrEmpty (nameIn))
				return;
			//没有输入或者没有不安化就不加载
			//print("loadMusic -- " + nameIn);
		AudioClip 	theClip = Resources .Load("Sounds/"+ nameIn) as AudioClip;
		if (theClip == null  ) 
		{
			print ("没有加载成功");
			return;
		}
		else 
		{
			theSource.PlayOneShot (theClip);
			print ("play sound");
		}
 
	}

	void Start () 
	{
		makeStart ();
	}
	
 
}
