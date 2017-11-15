using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deathButton : MonoBehaviour {

   //死亡道场按钮
   //只有两个功能，按下和初始化
	private int index = -9999;//死亡剧本下标记录
	private bool isOpen = false ;
	public Sprite theOpenSprite;
	public void makeStart(int indexIn , bool openIn , string name = "意外身亡")
	{
		index = indexIn;
		isOpen = openIn;
		if (isOpen) 
		{
			Text theText = this.GetComponentInChildren<Text> ();
			theText.text = "["+name +"]";
			theText.color = Color.yellow;
			this.GetComponent <Image> ().sprite = theOpenSprite;

		}
		else 
		{
			Text theText = this.GetComponentInChildren<Text> ();
			theText.text = "[尚未激活]";
			theText.color = Color.gray;

		}
	}

	public void toDeathScene()
	{
		if(isOpen)
		{
		systemInformations.deadPlotIndex = this.index;
		UnityEngine.SceneManagement.SceneManager.LoadScene ("DeadScene");//进入到死亡道场
		}
	}
}
