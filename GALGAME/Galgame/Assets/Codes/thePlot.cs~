using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个类是这个游戏的剧本类，也就是总体控制类
//这个类将会控制整个游戏的流程，但是只能够控制每一个流程的抽象内容
//具体的流程内容将由thePlotItem来进行

//原计划是每一个剧情长度片一个场景
//用一个总控的文本控制单元来获得和跳转进度

public class thePlot : MonoBehaviour {

	public TextAsset [] thePlots;//剧本文件在这里保存
	private plotTreeMaker theTreeMake;//创建剧本树的控制单元
	private List<thePlotItem> roots;//局本树丛的小树根
	private thePlotItem  theItemNow;//当前控制的剧本帧引用 
	private textController theTextController;//文本显示控制单元

	//本脚本的初始化，这个是最先初始化的内容
	//初始化第一阶段
	void makeStart()
	{
		theTreeMake = this.GetComponent <plotTreeMaker> ();
		roots = new List<thePlotItem> ();
	}

	//全体剧本树的初始化在这里完成
	//完全自动化处理，只需要在控制面板里面处理好相应的父子关系就可以了
	//初始化第二阶段
	public void InitTheTree(TextAsset thePlot)
	{
		Transform theRoot = theTreeMake.makeATree (thePlot);
		theRoot.transform.SetParent (this.transform);
		//this.GetComponent <allStartController> ().makeATree ();
		int count = this.transform.childCount;
		for(int i=0;i< count; i++)
		{
			Transform theChild = this.transform.GetChild (i);
			if (theChild.GetComponent<thePlotItem> ()) 
			{
				thePlotItem theItem = theChild.GetComponent<thePlotItem> ();
				roots.Add (theItem);
				theItem.makeStart ();//递归初始化
			}
		}
	}


	//鼠标点击空白部分就意味着galgame要跳转到下一行剧本了
	void moveToNextItem()
	{
		//剧本帧为空就不做操作了
		if (theItemNow == null)
			return;

		//如果是一个分支节点
		if (theItemNow.isASpitRoot ()) 
		{
			//进入选择界面，在这里不标明要走哪一条路线，在UI选择的时候才会真的有所选择
		} 
		else 
		{
			theItemNow = theItemNow.moveToNext (); 
		}
	}


	//全是鼠标点击
	//如果射线命中的是UI，就执行UI操作，否则就是一般的鼠标嗲你操作
	bool isOperatingUI ()
	{

		Ray mRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit2D mHi = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

		if (mHi.collider != null )
		{

			if (mHi.collider.gameObject.transform.tag == "UI") 
			{
				print ("UI");
				return true;
			}
		}
		return false;
		
	}

	void Start () 
	{

	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			makeStart ();
			InitTheTree(thePlots [0]);
		}
		//为了迎合安卓和PC，仅仅使用这一种操作方法
		//打算使用射线进行区分
		if (Input.GetMouseButtonDown (0)) 
		{
			if (isOperatingUI ()) 
			{
				//针对UI的操作
			}
			else 
			{
				print ("skip");
				moveToNextItem ();
			}
		}
	}
}
