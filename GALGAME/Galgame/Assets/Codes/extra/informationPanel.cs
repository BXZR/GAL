using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//信息通告面板的代码,实际上这是一个不断查询的过程
//为了减少，这个部分与游戏其他部分的耦合，就用轮训的方法了
using UnityEngine.UI;


public class informationPanel : MonoBehaviour {

	public static string informationShow = "开始游戏";
	public Color BackColor;//背景图的显示颜色
	private static float showTimer = 2f;
	private static float showTimerAll = 2f;
	private static Text theTextShow;
	private static Image theBackImage;
	private static Color theBackColor;

	public static void  showInformation(string toShow)
	{
		informationShow = toShow; 
		showTimer = showTimerAll;
		theTextShow.text = informationShow;
		theBackImage.color = theBackColor;
	}

	// Use this for initialization
	void Start ()
	{
		theTextShow = this.GetComponentInChildren<Text> ();
		theBackImage = this.GetComponentInChildren<Image> ();
		theBackColor = BackColor;
		InvokeRepeating ("flush" , 0f,1f);

	}


	void flush()
	{
		if (string.IsNullOrEmpty (informationShow) == false) 
		{
			showTimer --;
			if (showTimer < 0) 
			{
				shutShow ();
			}
		}
	}

	void shutShow()
	{
		informationShow = "";
		theTextShow.text = "";
		theBackImage.color = new Color (0,0,0,0);
	}
		
}
