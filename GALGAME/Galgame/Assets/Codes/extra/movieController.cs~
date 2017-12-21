#define PC
#undef PC
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movieController : MonoBehaviour {

	//用预处理的方法来分开PC和安卓平台功能
	#if PC
	public MovieTexture movTexture;
	#endif 
	private bool startOnPC = false;
	void Start () 
	{
		 

	}
	void OnGUI()
	{
		if(startOnPC)
		{
		//绘制电影纹理
			#if PC
			Screen.fullScreen = false;
			GUI.DrawTexture (new Rect (0,0, Screen.width, Screen.height),movTexture,ScaleMode.StretchToFill);  
			//播放/继续播放视频
			if(!movTexture.isPlaying)
			{
			  movTexture.Play();
			}
			#endif 

		//GUI.DrawTexture (new Rect (0,0, Screen.width, Screen.height),movTexture,ScaleMode.StretchToFill);  

//		if(GUILayout.Button("播放/继续"))
//		{
			//播放/继续播放视频
			//if(!movTexture.isPlaying)
			//{
				//movTexture.Play();
			//}

//		}
//
//		if(GUILayout.Button("暂停播放"))
//		{
//			//暂停播放
//			movTexture.Pause();
//		}
//
//		if(GUILayout.Button("停止播放"))
//		{
//			//停止播放
//			movTexture.Stop();
//		}
		}
	}

	public void startMovie()
	{
		string thePath;
		//打算用streamingAsset来做存档，文件的路径不同需要做一下区分
		#if PC
		Invoke("changeToStart" , 0.1f);//做一个延迟来消除分辨率错误
		#else
		if (Application.platform == RuntimePlatform.Android)
		{
			Handheld.PlayFullScreenMovie ("OP2.mp4",Color .black,FullScreenMovieControlMode.Hidden);
		} 
		#endif


	}

	void changeToStart()
	{
		startOnPC = true;
	}

	void endMovio()
	{
		#if PC
		#else
		Handheld.StopActivityIndicator ();
		#endif
	}
		
//	void Update () {
//		if (Input.GetKeyDown (KeyCode.A)) 
//		{
//			print ("mc");
//			startMovie ();
//		}
//		if (Input.GetKeyDown (KeyCode.S))
//			endMovio ();
//	}
}
