using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class systemInformations : MonoBehaviour {

	//think 是旁白，不显示人名的时候用
	private static string [] proName = {"alice","fione","jik","Caim" , "noOne"} ;
	private static string [] showName = {"艾丽斯","菲奥奈","吉克" , "凯伊姆" , ""};

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
