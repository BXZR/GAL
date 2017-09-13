using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	//这个类专门用来控制说话的时候现实的图和名字

	/*
	 * 文本框的人物图和背景大图某一时刻是唯效果非常复杂，这个需额外的处理
	 * 值得注意的就是整个界面有两个部分组成
	 * 一部分是UI
	 * 还有一部分是纯粹的2D的Sprite的构成的图像
	 * 其中，背景图北京众人物的大图都是sprite
	 * 
	*/

	public SpriteRenderer theBackPicture;//背景大图
	public SpriteRenderer [] bigPeoplePictures;//说话的时候的人物大图

	//public Image theShowPictureInSceen;//用于显示在场景里的大人物图
	public Image theShowPictureInText;//在文本框显示的图
	public Text theName;//文本框显示的名字

	private string theBackName = "";//保留的背景图的引用，因为背景图不用每一次都更新
	private string thePeopleSmallPictureName = "people/noOne";//说话的时候人物小图的标记

	void Start () 
	{

	}


	public void makeShow(thePlotItem theItem)
	{
		makeBigPicture(theItem);
		makeSmallPicture (theItem);
		makeBackPicture (theItem);
	 
	}

	//说话的时候的人物大图
	void makeBigPicture(thePlotItem theItem)
	{
		int count = 0;
		List <string > picNames = new List<string> ();

		for (int i = 0; i < theItem.thePartyplayers.Length; i++) 
		{
			if (theItem.thePartyplayers [i].Equals ("noOne") == false)
				picNames.Add (theItem.thePartyplayers [i]);
		}
		switch (picNames.Count) 
		{
		   //第零种类情况，说话的时候不显示人，这种情况需要把所有的大图都做成"noOne"
		case 0: 
			{
				for (int i = 0; i < bigPeoplePictures.Length; i++) {
					bigPeoplePictures [i].sprite =  makeLoadSprite ("people/noOne");

				}
			}
			break;
			//第一种情况，说话的只有一个人，所以显示的是中间的图
		case 1:
			{
				for (int i = 0; i < bigPeoplePictures.Length; i++) {
					bigPeoplePictures [i].sprite = makeLoadSprite ("people/noOne");
				}
				bigPeoplePictures [1].sprite = makeLoadSprite ("people/big/" +picNames [0]) ;
			}
			break;
			//第二种情况，说话的有两个人所以是两边的图被显示
		case 2:
			{
				for (int i = 0; i < bigPeoplePictures.Length; i++) {
					bigPeoplePictures [i].sprite = makeLoadSprite ("people/noOne");
				}
				bigPeoplePictures [0].sprite = makeLoadSprite ( "people/big/" +picNames [0]);
				bigPeoplePictures [2].sprite = makeLoadSprite ("people/big/" +picNames [1]);
			}
			break;
			//第三种情况，说话的有三个人,，所以三张图都显示
		case 3:
			{
				for (int i = 0; i < bigPeoplePictures.Length; i++) {
					bigPeoplePictures [i].sprite = makeLoadSprite ("people/big/" + picNames [i]);
				}
			}
			break;
		}
       



		
	}

	//说话的时候的小图
	void makeSmallPicture(thePlotItem theItem)
	{
		if (string.IsNullOrEmpty (theItem.theSpeekerName))
		{
			//不显示
			theShowPictureInText.sprite = makeLoadSprite ("people/noOne");
			thePeopleSmallPictureName = "noOne";
		} 
		else 
		{
			if (theItem.theSpeekerName.Equals (thePeopleSmallPictureName) == false)
			{
				string textureName = "people/small/" + theItem.theSpeekerName;
				theShowPictureInText.sprite = makeLoadSprite (textureName);
			}
		}
	}

	//背景图
	void makeBackPicture(thePlotItem theItem)
	{
		if ( string .IsNullOrEmpty(theItem.theBackPictureName)) 
		{
			//显示默认背景图，没有不行啊
			//这也是显示出来的第一张的图了
			theBackPicture.sprite = makeLoadSprite ("backPicture/home");
			theBackName = "home";
		} 
		else 
		{
			//只有指定换图的时候才会换图
			if ( string .IsNullOrEmpty (theItem.theBackPictureName) == false && theItem.theBackPictureName.Equals (theBackName) == false  )
			{
				//print ("back picture change");
				theBackName = theItem.theBackPictureName;
				theBackPicture.sprite = makeLoadSprite ("backPicture/"+ theBackName);
			}

		}
	}

	public Sprite makeLoadSprite(string textureName)
	{
		//textureName = "people/noOne";
		Texture2D theTextureIn = Resources.Load <Texture2D> (textureName);
		return Sprite .Create(theTextureIn,new Rect (0,0,theTextureIn.width,theTextureIn.height),new Vector2 (0,0));
	}
}
