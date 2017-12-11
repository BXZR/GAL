using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CGModeButton : MonoBehaviour {

	//CG模式的按钮，有两种功能
	//1切换CG分组
	//2选择打开大图

	private string pictureName = "";
	private Image theBigPicture;
	private bool isGet = false;
	//创建的时候调用，用于初始化
	public  void  makeStart(string pictureNameIn , Image theImageBig  , CGModeController controllerIn )
	{
		pictureName = pictureNameIn;
		theBigPicture = theImageBig;
		theController = controllerIn;
		isGet = CGModeFile.checkIsLocked (this.pictureName);
		//按钮上面加载的都是小图
		if(isGet)
			this.GetComponent <Button>().image.sprite = systemInformations.makeLoadSprite("backPictureForButton/"+pictureNameIn);
	}


	//切换CG分组
	public int indexForGroup = 1;//分组编号
	public CGModeController theController;//因为调用这个控制单元的方法
	public void changeCGGroup()
	{
		//已经封装好，传入参数就好
		theController.makeCGSelectButton (indexForGroup);
	}

	//打开大图
	public void openBigPicture()
	{
		if (isGet == false)
			return;//CG未解锁，神都不
		string pictureNameUse =pictureName.Remove(pictureName.LastIndexOf("_Button"));
		theBigPicture.GetComponent <Image> ().sprite = systemInformations.makeLoadSprite ("backPicture/"+pictureNameUse);
		theBigPicture.gameObject.SetActive (true);
	}

	//加载图片
	//这个方法被统一使用为systeminformation的方法
    //public  Sprite makeLoadSprite(string textureName)
	//{
	//	//textureName = "people/noOne";
	//	Texture2D theTextureIn = Resources.Load <Texture2D> (textureName);
	//	return Sprite .Create(theTextureIn,new Rect (0,0,theTextureIn.width,theTextureIn.height),new Vector2 (0,0));
	//}

}
