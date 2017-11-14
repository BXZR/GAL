using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tipForAudioController : MonoBehaviour {

	//将音频控制单元设定为全局唯一
	//跨场景物体，这样可以有效减少场景切换的时候音频的切换的问题和开销
	//必须要保证单例模式，否则就会出点多音效的情况

	private static tipForAudioController  theSinglePlayer = null;//全球唯一控制单元标记
	void Start () 
	{
		if (theSinglePlayer == null) 
		{
			theSinglePlayer = this;
			DontDestroyOnLoad (this.gameObject);
		} 
		else
		{
			Destroy (this.gameObject);
		}
	}
	
 
}
