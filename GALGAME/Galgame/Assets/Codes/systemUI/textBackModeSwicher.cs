using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textBackModeSwicher : MonoBehaviour {

	//其实就是换一张图
	//因为有时候文本框有点占地方，妨碍看CG，因此给出选项
	public GameObject[] TextBackImages;
	public Text theShowText;
	private int indexUse = 0;

	void Start () 
	{
		for (int i = 0; i < TextBackImages.Length; i++) 
		{
			TextBackImages [i].SetActive (false);
		}
		changeMode(0);
		theShowText.text = "默认";
	}


	public void changeTextBackImageMode()
	{
		if (indexUse == 0) 
		{
			changeMode(1);
			theShowText.text = "简洁";
		} 
		else if (indexUse == 1)
		{
			changeMode(0);
			theShowText.text = "默认";
		}
	}

	private void changeMode(int indexAim)
	{
		TextBackImages [indexUse].SetActive (false);
		indexUse = indexAim;
		TextBackImages [indexUse].SetActive (true);
	}
}
