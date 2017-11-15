using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musicModeButton : MonoBehaviour {

	//这个脚本用来描述音乐模式下的控制按钮
	//这样做的想法是实现自治

	//这个按钮代表的资源名称
	public string theMusicName = "";
	private string thePictureName = "";
	//音频播放单元
	private MusicController theMusicPlayController = null;
	//展现的图
	private Image theImageShow ;
	//这个按钮的TEXT
	private Text thisText ;
	//这个按钮的背景图
	private Image thisImage;
	//纪录已经选择的内容
	private static Image theButtonImageSaved = null;
	private static Text theTextSaved = null;
	//显示正在播放的音乐
	private Text theShowMusicNameText;
	//记录下来已经加载的东西，减少重复的加载过程
	private Sprite theSavedSprite = null;

	public void makeStart(string nameIn ,string pictureNameIn, MusicController MusicControllerIn , Image ImageIn , Text MusicNameText)
	{
		this.theMusicName = nameIn;
		this.thePictureName = pictureNameIn;
		theMusicPlayController = MusicControllerIn ;
		theImageShow = ImageIn;
		thisText  = this.GetComponentInChildren<Text> ();
		thisText.text = nameIn;
		//print (nameIn +"--"+pictureNameIn);
		thisImage = this.GetComponent <Image> ();
		theButtonImageSaved = thisImage;
		theTextSaved = thisText;
		theShowMusicNameText = MusicNameText;
	}


	//按下按钮当然要强制改变音乐
	//但是初始化的时候不用强制改变音乐
	public void playSound (bool changeClip = true )
	{
		print ("playSound");
		if (changeClip)
		{
			if (theMusicPlayController != null)
				theMusicPlayController.playBackMusic (this.theMusicName);
		}
		//print ("people/big/"+thePictureName);
		if (string.IsNullOrEmpty (thePictureName) == false) 
		{
			//判断与减少加载图像的次数
			if(theSavedSprite == null)
				theSavedSprite = makeLoadSprite ("people/big/" + thePictureName);
			theImageShow.sprite = theSavedSprite;
		} 
		else
		{
			//判断与减少加载图像的次数
			theImageShow.sprite = makeLoadSprite ("Extra/MusicNullBack");
		}
		//if (thisImage != theButtonImageSaved )
		{
			theTextSaved.color = Color.white;
			theButtonImageSaved.color = Color.white;

			theButtonImageSaved = thisImage;
			theTextSaved = thisText;

			theTextSaved.color = Color.yellow;
			theButtonImageSaved.color = Color.yellow;
			theShowMusicNameText.text = theTextSaved.text+"~";
		}

	}

	private Sprite makeLoadSprite(string textureName)
	{
		//textureName = "people/noOne";
		Texture2D theTextureIn = Resources.Load <Texture2D> (textureName);
		return Sprite .Create(theTextureIn,new Rect (0,0,theTextureIn.width,theTextureIn.height),new Vector2 (0,0));
	}
}
