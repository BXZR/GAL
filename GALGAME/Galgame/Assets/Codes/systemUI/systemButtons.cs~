using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个类专门用于处理在system大面板中需要处理的按钮逻辑
//而且仅限于逻辑控制按钮
using UnityEngine.UI;
public class systemButtons : MonoBehaviour {

	//是否开启自动模式
	//所谓自动模式就是一个人说完话之后一段时间之后自动跳转到下一句对话
	public void autoSwitcher()
	{
		systemInformations. isAutoWait = !systemInformations. isAutoWait;
		if (systemInformations. isAutoWait) 
		{
			informationPanel.showInformation ("开启自动播放模式");
			this.GetComponentInChildren<Text> ().text = "已开启";
		} 
		else 
		{
			informationPanel.showInformation ("关闭自动播放模式");
			this.GetComponentInChildren<Text> ().text = "已关闭";
		}
	}

	//存档还是读档
	public void saveLoadSwitcher()
	{
		systemInformations. isSaving = !systemInformations. isSaving;
		if (systemInformations. isSaving) 
		{
			informationPanel.showInformation ("切换为存档模式");
			this.GetComponentInChildren<Text> ().text = "存档";
		} 
		else 
		{
			informationPanel.showInformation ("切换为读档模式");
			this.GetComponentInChildren<Text> ().text = "读档";
		}
	}
}
