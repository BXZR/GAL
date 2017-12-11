using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textBackModeSwicher : MonoBehaviour  , configUser {

	//其实就是换一张图
	//因为有时候文本框有点占地方，妨碍看CG，因此给出选项
	public GameObject[] TextBackImages;
	public Text theShowText;
	private int indexUse = 0;
	public ConfigItems theConfigType = ConfigItems.TextBack;
	//用来和配置文件配合使用的标记
	string setting = "1";

	void Start () 
	{
		for (int i = 0; i < TextBackImages.Length; i++) 
		{
			TextBackImages [i].SetActive (false);
		}
		changeMode(0);
		theShowText.text = "默认";
	}


	public void changeTextBackImageMode()
	{
		if (indexUse == 0) 
		{
			changeMode(1);
			theShowText.text = "简洁";
		} 
		else if (indexUse == 1)
		{
			changeMode(0);
			theShowText.text = "默认";
		}
	}

	private void changeMode(int indexAim)
	{
		TextBackImages [indexUse].SetActive (false);
		indexUse = indexAim;
		TextBackImages [indexUse].SetActive (true);
		setting = indexUse.ToString ();
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
		indexUse = savedType < 1f ? 1 : 0;
		changeTextBackImageMode();
		//informationPanel.showInformation (configController. chekcConfigFile());
	}
}
