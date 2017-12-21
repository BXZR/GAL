using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readTextButton : MonoBehaviour {

	//这个按钮是最近已读文字面板中的语音控制按钮
	//语音名称在初始化的时候得到
	//声源，在初始化的时候赋值，是theGameController/speakSoundController的声源组件
	private AudioSource theAudioSource = null;
	private AudioClip theClip = null;//只会加载一次
	public GameObject theButton;//用于控制原因的按钮
	//外部初始化的方法
	//因为有一些没有声音的按钮是没有必要做初始化的
	public void makeStart(string speakSoundName , AudioSource theSoundSource)
	{
		//print ("Speaks/" + speakSoundName);

		if (string.IsNullOrEmpty (speakSoundName))
			Destroy (theButton.gameObject);
		else
		{
			this.theAudioSource = theSoundSource;

			this.theClip = Resources.Load ("Speaks/" + speakSoundName) as AudioClip;
			if (this.theAudioSource == null || this.theClip == null)//初始化失败了就直接自灭
				Destroy (theButton.gameObject);
		}
	}
    
	//点击播放的方法
	public void playSpeakSound()
	{
		//还是从中截断比较好，不带“回声处理”
		if (theAudioSource.isPlaying)
			theAudioSource.Stop ();
		
		theAudioSource.PlayOneShot (theClip);
	}
}
