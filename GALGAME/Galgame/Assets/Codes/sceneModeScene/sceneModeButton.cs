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
	private bool isOpened = false;
	private string aimsceneName = "gal_1";//跳转到这个场景
	public void makeStart(int indexStart , int indexEnd , string nameIn = "--" , bool isOpenedIn = false)
	{
		indexForStart = indexStart;
		indexForEnd = indexEnd;
		isOpened = isOpenedIn;
		Text theText = this.GetComponentInChildren<Text> ();
		if (isOpened)
		{
			theText.text =  "["+nameIn +"]";
			//更改背景图片功能在这里写
			this.GetComponent<Image>().sprite = systemInformations. makeLoadSprite("UI/sceneBook");
		}
		else
		{
			theText.text = "[未解锁]";
		}
	}

	//按钮点击的事件
	public void makeSceneSelected ()
	{
		if (isOpened)
		{
            //只有在激活条件下才会有效
			//print ("from " + indexForStart + "   to " + indexForEnd);
			systemInformations.makeScene (indexForStart,indexForEnd);
			UnityEngine.SceneManagement.SceneManager.LoadScene (aimsceneName);
		}
	}

	//加载图片
	//这个反复噶已经被同意为systeminformations的方法
	//public  Sprite makeLoadSprite(string textureName)
	//{
	//	//textureName = "people/noOne";
	//	Texture2D theTextureIn = Resources.Load <Texture2D> (textureName);
	//	return Sprite .Create(theTextureIn,new Rect (0,0,theTextureIn.width,theTextureIn.height),new Vector2 (0,0));
	//}

}
