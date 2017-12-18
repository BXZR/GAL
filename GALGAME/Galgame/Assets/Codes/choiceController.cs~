using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class choiceController : MonoBehaviour {

	//这个类专门用来控制选项
	public Image theBackImage;//灰色背景图
	public choice [] theSelectButtons;

	//创造选择内容
	public void openSelect (List<thePlotItem> theSelects)
	{
		for(int i = 0; i< theSelects .Count ;i++)
		{
			theSelectButtons [i].makeTheSelectShow (theSelects [i]);
			theSelectButtons [i].gameObject.SetActive (true);
		}
		theBackImage.enabled = true;
	}

	//关闭掉选项
	public void shutSelect()
	{
		for (int i = 0; i < theSelectButtons.Length; i++)
		{
			theSelectButtons [i].gameObject.SetActive (false);
		}
		theBackImage.enabled = false;
	}


	// Use this for initialization
	void Start () 
	{
		for (int i = 0; i < theSelectButtons.Length; i++)
		{
			theSelectButtons [i].makeStart (this);
		}
		shutSelect ();
	}
		
}
