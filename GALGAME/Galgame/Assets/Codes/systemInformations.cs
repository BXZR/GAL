﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class systemInformations : MonoBehaviour {

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
		
}
