using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fontButton : MonoBehaviour {
 //这个类专门用来控制更换字体的按钮
	bool isNormal = true;//一共两种字体，用bool作为区分的方法
	public  Text theTextForTalk;//实际上只有人物说话的text用于字体更换
	public void  changeFont()
	{
		if (isNormal) 
		{
			isNormal = false;
			Font theFont = Resources.Load <Font>("Font/GB2312");
			theTextForTalk.font = theFont; 
			this.GetComponentInChildren<Text> ().text = "仿宋";
		} 
		else 
		{
			isNormal = true;
			Font theFont = Resources.Load <Font>("Font/words");
			theTextForTalk.font = theFont; 
			this.GetComponentInChildren<Text> ().text = "繁体";
		}
	}
}
