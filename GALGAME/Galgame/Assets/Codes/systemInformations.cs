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
	public static bool isThePanelShows = false;//存档面板是不是显示了
	public static bool isChildPanelShows = false;//有一个字panel用来存储一些不是很常用的按钮
	public static bool isSaving = true;//true表示存档，false表示读档
	//每一次按照说话的长度判断是持续时间(这个数值要比textController的theFlashWaitTime要长才可以)
	public static float plotTimeWait = 0.35f;//剧本中每一个字的时间长度
	//将全局变量赋值成初始值
	public static float textShowTime = 0.2f;//文本显示的时候每一个字的弹出速度
	public static  void  makeFlush()
	{
		isAutoWait = true;//剧本到时间自动跳转
		isThePanelShows = false;//存档面板是不是显示了
		isChildPanelShows = false;//有一个字panel用来存储一些不是很常用的按钮
		isSaving = true;//true表示存档，false表示读档
	}

}
