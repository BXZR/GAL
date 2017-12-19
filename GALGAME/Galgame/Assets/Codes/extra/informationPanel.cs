using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//信息通告面板的代码,实际上这是一个不断查询的过程
//为了减少，这个部分与游戏其他部分的耦合，就用轮训的方法了
using UnityEngine.UI;


public class informationPanel : MonoBehaviour {

	public static string informationShow = "开始游戏";
	private static float showTimer = 1.5f;
	private static float showTimerAll = 1.5f;
	private static Text theTextShow;
	private static GameObject thePanel;

	public static void  showInformation(string toShow)
	{
		informationShow = toShow; 
		showTimer = showTimerAll;
		theTextShow.text = informationShow;
		thePanel.SetActive (true);
	}
		
	void Start ()
	{
		thePanel = this.gameObject;
		theTextShow = this.GetComponentInChildren<Text> ();
		showTimer = showTimerAll;
		thePanel.SetActive (false);
	}

	//只有在开启的时候才会Update
	//相较于总的invokeRepeat，来说计算次数大大减少
	void Update()
	{
		showTimer -= Time.deltaTime;
		if (showTimer < 0)
			thePanel.SetActive (false);
	}
			
		
}
