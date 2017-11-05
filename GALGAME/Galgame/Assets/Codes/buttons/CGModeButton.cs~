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

	//创建的时候调用，用于初始化
	public  void  makeStart(string pictureNameIn , Image theImageBig  , CGModeController controllerIn)
	{
		pictureName = pictureNameIn;
		theBigPicture = theImageBig;
		theController = controllerIn;
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
		string pictureNameUse =pictureName.Remove(pictureName.LastIndexOf("_Button"));
		theBigPicture.GetComponent <Image> ().sprite = theController.makeLoadSprite ("backPicture/"+pictureNameUse);
		theBigPicture.gameObject.SetActive (true);
	}

}
