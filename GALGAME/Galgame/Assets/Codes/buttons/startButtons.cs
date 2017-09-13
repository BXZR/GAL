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
		SceneManager.LoadScene ("gal_1");
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
}
