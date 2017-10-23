using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startOpenShutButton : MonoBehaviour {

	//这个处理开始界面的开关问题
	//因为需要保留引用，但是又不希望保留太多的引用
	public GameObject thePanel;

	public void  OpenOrShut()
	{
		if (thePanel.gameObject.activeInHierarchy)
			thePanel.gameObject.SetActive (false);
		else
			thePanel.gameObject.SetActive (true);
	}

}
