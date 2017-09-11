using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class systemInformations : MonoBehaviour {

	private static string [] proName = {"alice"} ;
	private static string [] showName = {"艾丽斯"};

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
