using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showAllOverPercentLabel : MonoBehaviour {

	//显示剧本完成度的UI

	Text theText;

	//根据开始场景的特性，还是用一个Start来做一次初始化就足够了
	void Start()
	{
		if (theText == null)
			theText = this.GetComponent <Text> ();
		theText.text = "剧本完成度\n" + (systemInformations.getPlotOverPercent ()*100).ToString("f2")+"%";
	}
//	void OnEnable()
//	{
//		if (theText == null)
//			theText = this.GetComponent <Text> ();
//		theText.text = "剧本完成度\n" + (systemInformations.getPlotOverPercent ()*100).ToString("f2")+"%";
//	}
}
