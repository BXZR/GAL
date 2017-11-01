using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadTextPanel : MonoBehaviour {

	 //显示已经读到的最后几条对话
	//之所以用panel而不是直接用text是因为有可能需要功能扩展
	//public Text theShowText; //用于显示的文本，减少一次获取对象的过程
	 
	public  GameObject theProfabText;//预设物也就是显示模板
	//排版用Grid来托管
	public AudioSource theAudioSouce;

	void makeFlash()
	{
       
		readTextButton[] childs = this.GetComponentsInChildren<readTextButton> ();
		for (int i = 0; i < childs.Length; i++)
			Destroy (childs[i].gameObject);

		//因为用到的是队列进行保存，并不能直接用下标，所以做一次简单的折中，
		//这个地方当然应该是可以优化的
		List<string> readText = new List<string> ();
		List<string> readSpeakName = new List<string> ();
		foreach (string readGet in systemInformations.theReadText)
			readText.Add (readGet);
		foreach (string speakGet in systemInformations.theReadTextSpeak)
			readSpeakName.Add (speakGet);
		for (int i = 0; i < readText.Count; i++) 
		{
			GameObject theTextShowUse = GameObject.Instantiate<GameObject> ( theProfabText);
			theTextShowUse.GetComponentInChildren<Text> ().text = readText [i];
			theTextShowUse.GetComponentInChildren<readTextButton> ().makeStart (readSpeakName[i] , theAudioSouce);
			theTextShowUse.transform.SetParent (this.transform);
		}
	}

	//回调方法，自动调用实现刷新
	void  OnEnable()
	{
		makeFlash ();
	}
}
