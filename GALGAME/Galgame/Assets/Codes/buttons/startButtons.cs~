using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startButtons : MonoBehaviour {

	public  static bool loadMemory = false;//是否加载存档

	//从头开始
	public void makeStart()
	{
		loadMemory = false;
		SceneManager.LoadScene ("forFront");
	}

	//带存档的开始
	public void loadStart()
	{
		loadMemory = true;
		SceneManager.LoadScene ("gal_1");
	}

	//结束游戏
	public void  exitGame()
	{
		Application.Quit ();
	}

	//回到初始界面
	public void  backStartScene()
	{
		SceneManager.LoadScene ("start");
	}
}
