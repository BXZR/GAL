using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class choice : MonoBehaviour {

	//这个选项容纳的plotItem
	private thePlotItem theItem;
	//选择控制单元
	private choiceController theController;
	//文字显示的内容
	private Text theShowText;

	//初始化
	//这个过程只可以在控制器里面执行，并且最好只可以执行一次
	public  void  makeStart( choiceController theCotrollerIn)
	{
		this.theController = theCotrollerIn;
		theShowText = this.GetComponentInChildren<Text> ();
	}

	//填充选项
	//在这里可以看到分支选项的第一层选项给出的是一个选项介绍而并不是剧本内容
	//这样做是为了与剧本树进行兼容
	public void makeTheSelectShow(thePlotItem INP)
	{
		this.theItem = INP;
		this.theShowText.text =  INP.theTitleName;
	}

	//用于UI点击事件触发的方法
	public void Select()
	{
		theController.GetComponent <thePlot> ().playTheItem (this.theItem);
		theController.GetComponent <textController> ().openTEXT ();
		theController.shutSelect ();
	}

	//优化一点点，尽可能减少回调......
	// Use this for initialization
	//void Start () {
		
	//}
	
	// Update is called once per frame
	//void Update () {
		
	//}
}
