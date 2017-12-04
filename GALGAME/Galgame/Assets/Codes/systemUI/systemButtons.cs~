using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个类专门用于处理在system大面板中需要处理的按钮逻辑
//而且仅限于逻辑控制按钮
using UnityEngine.UI;
public class systemButtons : MonoBehaviour {

	//是否开启自动模式
	//所谓自动模式就是一个人说完话之后一段时间之后自动跳转到下一句对话

	//保存引用，不重复this.GetComponentInChildren<Text> ().text
	//但是说实话这种小优化对于并不经常使用的text来讲用处并不大
	private Text theShowText ;
	void Start ()
	{
		theShowText = this.GetComponentInChildren<Text> ();
	}
	public void autoSwitcher()
	{
		systemInformations. isAutoWait = !systemInformations. isAutoWait;
		systemInformations.isAutoNowSave = systemInformations.isAutoWait;
		if (systemInformations. isAutoWait) 
		{
			informationPanel.showInformation ("开启自动播放模式");
			theShowText.text = "已开启";
		} 
		else 
		{
			informationPanel.showInformation ("关闭自动播放模式");
			theShowText.text = "已关闭";
		}
	}

	//存档还是读档
	public void saveLoadSwitcher()
	{
		systemInformations. isSaving = !systemInformations. isSaving;
		if (systemInformations. isSaving) 
		{
			informationPanel.showInformation ("切换为存档模式");
			theShowText.text = "存档";
		} 
		else 
		{
			informationPanel.showInformation ("切换为读档模式");
			theShowText.text = "读档";
		}
	}
}
