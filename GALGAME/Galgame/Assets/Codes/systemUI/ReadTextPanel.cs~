using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadTextPanel : MonoBehaviour {

	 //显示已经读到的最后几条对话
	//之所以用panel而不是直接用text是因为有可能需要功能扩展
	public Text theShowText; //用于显示的文本，减少一次获取对象的过程
	 
	void  OnEnable()
	{
		theShowText.text = systemInformations.getReadText ();
	}
}
