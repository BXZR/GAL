using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class extraHController : MonoBehaviour {

	//额外特典的按钮
	public GameObject theExtraButton;//按钮预设物
	public GameObject theContent;//放置预设物的父物体
	private bool made = false;

	private void makeClear()
	{
		//清理一下，其实没有必要
		extraHButtons[] theDs = theContent.GetComponentsInChildren<extraHButtons> ();
		for (int i = 0; i < theDs.Length; i++)
			Destroy (theDs [i].gameObject);
	}
	//在Onenables调用(只调用一次)
	public void makeButtons()
	{
		//生成按钮
		for (int i = 0; i < extraHFile.buttonPictureName.Length; i++) 
		{
			GameObject theButton = GameObject.Instantiate<GameObject> (theExtraButton);
			extraHButtons DB = theButton.GetComponent <extraHButtons> ();
			DB.makeStart ( extraHFile.HPlotID[i] , extraHFile.loveOpend[i] , extraHFile.PlotName[i] , extraHFile.buttonPictureName[i]);
			theButton.transform.SetParent (theContent.transform);
			theButton.transform.localScale = new Vector3 (1, 1, 1);
		}
	}

	//开始场景每一次重新进入这个made值才会更新为false
	//也就是说每一次进入start场景的时候这里才会更新一次
	//这从逻辑上来说是通的，可以减少生成的次数，算是优化

	void OnEnable()
	{
		if (made)
			return;
		makeClear ();
		//在展示的排序上，死亡道场在前面，H特典在后面
		Invoke ("makeButtons",0.05f);
		//makeButtons ();
		made = true;
	}
}
