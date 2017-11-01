using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fontButton : MonoBehaviour {
 //这个类专门用来控制更换字体的按钮
	bool isNormal = true;//一共两种字体，用bool作为区分的方法
	public  Text theTextForTalk;//实际上只有人物说话的text用于字体更换

	public ConfigItems theConfigType = ConfigItems.Font;

	//因为默认是enabled == false的，所以不可以在start里面读取配置文件
	void Awake ()
	{  
		//初始化的时候尝试读取配置文件，没有文件默认返回1
		float savedFontType  = configController.getConfigItem (theConfigType);
		//返回1说明是normal
		//返回2说明是仿宋
		isNormal = savedFontType <= 1f ? false : true;//这里是需要反过来的，返回1说明是normal也就是变化之后需要编程normal
		changeFont();
		informationPanel.showInformation (configController. chekcConfigFile());
	}       

	//关闭设置面板的时候才会进行一次IO
	void OnDisable()
	{
		configController.flashConfig ();
	}

	public void  changeFont()
	{
		if (isNormal) 
		{
			tosFont1 ();
		} 
		else 
		{
			toFont2 ();
		}
	}

	private void tosFont1()
	{
		isNormal = false;
		Font theFont = Resources.Load <Font>("Font/GB2312");
		theTextForTalk.font = theFont; 
		this.GetComponentInChildren<Text> ().text = "仿宋";
		configController.setConfigItem (theConfigType , "2");
	}

	private void toFont2()
	{
		isNormal = true;
		Font theFont = Resources.Load <Font>("Font/words");
		theTextForTalk.font = theFont; 
		this.GetComponentInChildren<Text> ().text = "繁体";
		configController.setConfigItem (theConfigType , "1");
	}
}
