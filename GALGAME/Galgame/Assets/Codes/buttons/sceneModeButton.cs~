using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sceneModeButton : MonoBehaviour {
	//scene模式的选择按钮

	//传入剧本的编号 
	//在回忆模式之下只需要给出从头到尾的剧本帧“坐标”就好
	private int indexForStart = 0;
	private int indexForEnd = 0;


	public void makeStart(int indexStart , int indexEnd , string nameIn = "--" , bool isOpened = false)
	{
		indexForStart = indexStart;
		indexForEnd = indexEnd;
		Text theText = this.GetComponentInChildren<Text> ();
		if (isOpened)
		{
			theText.text =  "["+nameIn +"]";
			//更改背景图片功能在这里写
			this.GetComponent<Image>().sprite = makeLoadSprite("backPictureForButton/FioneCG3_Button");
		}
		else
		{
			theText.text = "[未解锁]";
		}
	}

	//按钮点击的事件
	public void makeSceneSelected ()
	{
		print ("from "+indexForStart +"   to "+indexForEnd);
	}

	//加载图片
	public  Sprite makeLoadSprite(string textureName)
	{
		//textureName = "people/noOne";
		Texture2D theTextureIn = Resources.Load <Texture2D> (textureName);
		return Sprite .Create(theTextureIn,new Rect (0,0,theTextureIn.width,theTextureIn.height),new Vector2 (0,0));
	}

}
