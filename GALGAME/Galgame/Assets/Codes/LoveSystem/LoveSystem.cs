using System.Collections;
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
			theShowPercentTexts[i].text = (percents[i]*100).ToString("f2")+"%";
			theShowPercentImages [i].fillAmount = percents [i];
		}
		thePlotOverPercentText.text = (systemInformations.getPlotOverPercent () * 100).ToString ("f2") + "%";
	}

	public void makeShowForScene()
	{
		for (int i = 0; i < theShowPercentTexts.Length; i++) 
		{
			theShowPercentTexts[i].text = "不可知";
			theShowPercentImages [i].fillAmount = 1f;
		}
		thePlotOverPercentText.text = (systemInformations.getPlotOverPercent () * 100).ToString ("f2") + "%";
	}

	void OnEnable ()
	{
		if (systemInformations.isScene == false) 
		{
			makeShow (systemInformations.lovePercent);
		}
	}
	//scene模式之下没有必要重复读取，所以只是在start里面做一下设定就足够了
	void Start () 
	{
		if (systemInformations.isScene) 
		{
			makeShowForScene ();
		}

	}
}
