﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoveSystem : MonoBehaviour {

	//攻略度系统，实际上就是好感度
	//下面两者一一对应，顺序不能乱
	public  Text [] theShowPercentTexts;//用来显示好感百分比的Text
	public Image  [] theShowPercentImages;//用来显示百分比的环形slider图片
	public Text thePlotOverPercentText;//用来显示剧本完成百分比的text
	public void makeShow(float [] percents)
	{
		for (int i = 0; i < percents.Length; i++) 
		{
			theShowPercentTexts[i].text = (percents[i]*100).ToString("f0")+"%";
			theShowPercentImages [i].fillAmount = percents [i];
		}
	}

	void OnEnable () 
	{
		makeShow (systemInformations.lovePercent);
		thePlotOverPercentText.text = (systemInformations.plotOverPercent*100).ToString("f2")+"%";
	}
}
