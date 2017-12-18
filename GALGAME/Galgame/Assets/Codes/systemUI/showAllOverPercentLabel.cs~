using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showAllOverPercentLabel : MonoBehaviour {

	//显示剧本完成度的UI

	Text theText;

	void OnEnable()
	{
		if (theText == null)
			theText = this.GetComponent <Text> ();
		theText.text = "剧本完成度\n" + (systemInformations.getPlotOverPercent ()*100).ToString("f2")+"%";
	}
}
