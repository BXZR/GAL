using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class extraHButtons : MonoBehaviour {

	//死亡道场按钮
	//只有两个功能，按下和初始化
	private int index = -9999;//死亡剧本下标记录
	private bool isOpen = false ;

	public void makeStart(int indexIn , bool openIn , string name = "艾莉丝", string pictureName = "")
	{
		index = indexIn;
		isOpen = openIn;
		Image theImage = this.GetComponent <Image> ();
		theImage.sprite = systemInformations.makeLoadSprite (pictureName);
		if (isOpen) 
		{
			Text theText = this.GetComponentInChildren<Text> ();
			theText.text = "["+name +"]";
			theText.color = Color.yellow;
		}
		else 
		{
			Text theText = this.GetComponentInChildren<Text> ();
			theText.text = "[未解锁]";
			theText.color = Color.gray;
			theImage.color = Color.gray;

		}
	}

	public void toHScene()
	{
		if(isOpen)
		{
			//嘛...也算是一种【死亡回放】.....
			systemInformations.deadPlotIndex = this.index;
			UnityEngine.SceneManagement.SceneManager.LoadScene ("DeadScene");//进入到死亡道场但是实际上这是一个复用的情况
		}
	}
}
