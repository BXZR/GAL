﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个类是每一个流程真正进行的过程
//控制每一个流程的图像，音频，背景和文字的显示
//这个是每一个流程的总控单元

//原计划是这个类指定图像，音频，背景和文字然后穿个其它的对象
//指定的内容由字符串，进行绑定

//目标是只在这里进行一些字符串的初始化，建立每一个剧本流程与各个资源的映射关系
//事实上这个类也仅仅做这一件事情

public class thePlotItem : MonoBehaviour {


	//果然还是更喜欢使用局本树的方法处理
	private  List<thePlotItem>  theChildItems ;//子控制单元 

	//控制直接子类的方法
	//剧本单元的递归初始化从这里开始
	public  void makeStart()
	{
		theChildItems = new List<thePlotItem> ();
		int count = this.transform.childCount;
		for(int i=0;i< count; i++)
		{
			Transform theChild = this.transform.GetChild (i);
			if (theChild.GetComponent<thePlotItem> ()) 
			{
				thePlotItem theItem = theChild.GetComponent<thePlotItem> ();
				theChildItems.Add (theItem);
				theItem.makeStart ();//递归初始化
			}
		}
	}

	//是不是一个具有分支的节点，如果是，需要有特殊的处理方式
	//这个处理的过程不再这里执行，而在主要控制单元里面调用
	//此方法是一个判定标记
	public bool  isASpitRoot()
	{
		if (this.theChildItems.Count > 1)
			return true;
		return false;
	}

	//跳转到下一个节点
	public thePlotItem moveToNext(thePlotItem theAim = null)
	{
		//直接控制跳转
		if (theAim != null)
			return theAim;
		//实际上只有单个分支的节点才会调用这个方法
		//但是稳妥起见还是用下标标记吧
		return this.theChildItems [0];
	}


	void Start () {
		
	}
	

	void Update () {
//		if (Input.GetKeyDown (KeyCode.C))
//			print (this.name + "---" + this.theChildItems.Count + "----" + isASpitRoot ());
	}
}
