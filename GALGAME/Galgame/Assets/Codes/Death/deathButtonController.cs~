using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class deathButtonController : MonoBehaviour {

	//控制生成死亡道场的剧情按钮

	public GameObject theDButton;//按钮预设物
	public GameObject theContent;//放置预设物的父物体

	//在Onenables调用(只调用一次)
	public void makeButtons()
	{
		if (DeathFile.isStarted == false)
			DeathFile.makeAllStart ();
		//清理一下，其实没有必要
		deathButton[] theDs = theContent.GetComponentsInChildren<deathButton> ();
		for (int i = 0; i < theDs.Length; i++)
			Destroy (theDs [i].gameObject);
		//生成按钮
		for (int i = 0; i < DeathFile.theDeadPlayIndex.Count; i++) 
		{
			GameObject theButton = GameObject.Instantiate<GameObject> (theDButton);
			deathButton DB = theButton.GetComponent <deathButton> ();
			DB.makeStart ( Convert.ToInt32(DeathFile.theDeadPlayIndex[i]), DeathFile.theDeadOpen[i] , DeathFile.theDeathName[i]);
			theButton.transform.SetParent (theContent.transform);
			theButton.transform.localScale = new Vector3 (1, 1, 1);
		}
	}


	void OnEnable()
	{
		makeButtons ();
	}
}
