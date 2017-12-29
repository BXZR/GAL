﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class readTextClolorChangeSiwtcher : MonoBehaviour  , configUser {

	//其实就是换一张图
	//因为有时候文本框有点占地方，妨碍看CG，因此给出选项
	public Text theShowText;
	private int indexUse =1;
	public ConfigItems theConfigType = ConfigItems.readTextChangeColor;
	//用来和配置文件配合使用的标记
	string setting = "1";

	void Start () 
	{
		changeMode(1);
	}


	//外部方法
	public void makeChange()
	{
		if (indexUse == 0)
			changeMode (1);
		else
			changeMode (0);
	}

	private void changeMode(int indexAim)
	{
		indexUse = indexAim;
		setting = indexUse.ToString ();
		systemInformations.readTextColorChange = (indexUse == 1 )? 1 : 0;
		theShowText.text =( indexUse == 1 )? "开启" : "关闭";
	}

	//用来统一外部调用的方法（来自于接口抽象）
	public void saveToConfig ()
	{
		configController.setConfigItem (theConfigType , setting);
	}

	public void  loadFromConfig ()
	{
		//初始化的时候尝试读取配置文件，没有文件默认返回1
		float savedType  = configController.getConfigItem (theConfigType);
		indexUse = savedType < 1f ? 0 : 1;
		changeMode (indexUse);
		//informationPanel.showInformation (configController. chekcConfigFile());
	}
}
