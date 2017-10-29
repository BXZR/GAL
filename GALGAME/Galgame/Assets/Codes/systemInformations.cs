﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class systemInformations : MonoBehaviour {

	public  static bool loadMemory = false;//是否加载存档
	public static int indexForLoad = -99;//存档的编号
	public static bool canControll = true;//在渐入转场的时候鼠标是不可以有效果的
	//think 是旁白，不显示人名的时候用
	private static string [] proName = {"alice","fione","jik","Caim" ,"mert","fione", "aozi","noOne"} ;
	private static string [] showName = {"艾丽斯","菲奥奈","吉克" , "凯伊姆" ,"梅尔特", "菲奥奈", "奥兹",""};

	public static string getShowNameWithProName(string pro)
	{
		for (int i = 0; i < proName.Length; i++)
		{
			if (proName [i].Equals (pro))
				return showName [i];
		}
		return "???";
	}

	//----------------------system panel的相关记录逻辑---------------------------//
	public static  bool isAutoWait = true;//剧本到时间自动跳转
	public static bool isAutoNowSave = true; //是否自动跳转的副本
	public static bool isThePanelShows = false;//存档面板是不是显示了
	public static bool isChildPanelShows = false;//有一个字panel用来存储一些不是很常用的按钮
	public static bool isSaving = true;//true表示存档，false表示读档（面板相同，用这个标记来区分功能）

	//----------------------快进控制--------------------------//
	//控制时间速度
	private static float speedModeTimeScale= 17.0f;
	private static float normalModeTimeScale = 1.0f;

	private static  bool isSkiping = false;//是否在进行快进
	public static bool ISSkiping { get {return isSkiping;}}
	//全局唯一的控制是否快进的方法
	public  static void skipControll()
	{
		if (isSkiping == false) 
		{
			isSkiping = true;
			Time.timeScale = speedModeTimeScale;
		}
		else 
		{
			isSkiping = false;
			Time.timeScale = normalModeTimeScale;
		}
	}

	//每一次按照说话的长度判断是持续时间(这个数值要比textController的theFlashWaitTime要长才可以)
	public static float plotTimeWait = 0.35f;//剧本中每一个字的时间长度
	//将全局变量赋值成初始值
	public static float textShowTime = 0.2f;//文本显示的时候每一个字的弹出速度
	public static  void  makeFlush()
	{
		isAutoWait = true;//剧本到时间自动跳转
		isThePanelShows = true;//存档面板是不是显示了
		isChildPanelShows = false;//有一个字panel用来存储一些不是很常用的按钮
		isSaving = true;//true表示存档，false表示读档
	}

	//----------------------已经阅读的内容记录---------------------------//
	public  static Queue<string> theReadText = new Queue<string> ();
	private static  int theReadLength = 25;//最多记录25条最新的已读内容
	//记录最新一条已经读到的内容
	public static void saveRead (string information)
	{
		//如果数据量不多，直接插入就好
		if (theReadText.Count < theReadLength) 
		{
			theReadText.Enqueue(information );
			//print ("TheCountNow = " + theReadText.Count );
		}
		else
		{
			//print("Delete : " + theReadText.Dequeue());
			theReadText.Dequeue ();
			theReadText.Enqueue(information);
			//print ("TheCountNow = " + theReadText.Count );

			//print ("---------------------");
			//print (getReadText());
			//print ("---------------------");
		}
	}

	public  static  string  getReadText()
	{
		string readTextInformation = "";
		foreach (string theSaved in theReadText)
		{
			readTextInformation += theSaved +"\n\n";
		}
		return readTextInformation;
	}
}
