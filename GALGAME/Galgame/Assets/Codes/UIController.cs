using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	//这个类专门用来控制说话的时候现实的图和名字

	/*
	 * 文本框的人物图和背景大图某一时刻是唯效果非常复杂，这个需额外的处理
	 * 
	*/
	public Image theBackPicture;//背景大图
	//public Image theShowPictureInSceen;//用于显示在场景里的大人物图
	public Image theShowPictureInText;//在文本框显示的图
	public Text theName;//文本框显示的名字
	private string theBackName = "";//，保留的背景图的引用，因为背景图不用每一次都更新


	public void makeShow(thePlotItem theItem)
	{
		if (string.IsNullOrEmpty (theItem.theSpeekerName))
		{
			//不显示
			theShowPictureInText.sprite = makeLoadSprite ("people/noOne");
		} 
		else 
		{
			print ("f");
			string textureName = "people/" + theItem.theSpeekerName + "_" + theItem.theSpeekerPictureType;
			theShowPictureInText.sprite  = makeLoadSprite (textureName);
		}

		if (string.IsNullOrEmpty (theBackName)) 
		{
			//显示默认背景图，没有不行啊
			//这也是显示出来的第一张的图了
			theBackPicture.sprite = makeLoadSprite ("backPicture/home");
			theBackName = "home";
		} 
		else 
		{
			//只有指定换图的时候才会换图
			if ( string .IsNullOrEmpty(theItem .theBackPictureName) == false && theItem.theBackPictureName.Equals (theBackName) == false)
			{
				print ("back picture change");
				theBackName = theItem.theBackPictureName;
				theBackPicture.sprite = makeLoadSprite ("backPicture/"+ theBackName);
			}

		}
	}

	Sprite makeLoadSprite(string textureName)
	{
		Texture2D theTextureIn = Resources.Load <Texture2D> (textureName);
		return Sprite .Create(theTextureIn,new Rect (0,0,theTextureIn.width,theTextureIn.height),new Vector2 (0,0));
	}
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
