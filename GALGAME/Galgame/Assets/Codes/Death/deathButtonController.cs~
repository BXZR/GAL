using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class deathButtonController : MonoBehaviour {

	//控制生成死亡道场的剧情按钮

	public GameObject theDButton;//按钮预设物
	public GameObject theContent;//放置预设物的父物体
	private bool made = false;
	//在Onenables调用(只调用一次)

	//开始场景每一次重新进入这个made值才会更新为false
	//也就是说每一次进入start场景的时候这里才会更新一次
	//这从逻辑上来说是通的，可以减少生成的次数，算是优化
	public void makeButtons()
	{
		if (made)
			return;
		
		if (DeathFile.isStarted == false)
			DeathFile.makeAllStart ();
		//清理一下，其实没有必要因为需要考虑到刷新的问题
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
		made = true;
	}


	void OnEnable()
	{
		makeButtons ();
	}
}
