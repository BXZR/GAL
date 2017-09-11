using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个类是这个游戏的剧本类，也就是总体控制类
//这个类将会控制整个游戏的流程，但是只能够控制每一个流程的抽象内容
//具体的流程内容将由thePlotItem来进行

//原计划是每一个剧情长度片一个场景
//用一个总控的文本控制单元来获得和跳转进度
using UnityEngine.EventSystems;

public class thePlot : MonoBehaviour {

	public TextAsset [] thePlots;//剧本文件在这里保存
	private plotTreeMaker theTreeMake;//创建剧本树的控制单元
	private List<thePlotItem> roots;//局本树丛的小树根
	private thePlotItem  theItemNow = null;//当前控制的剧本帧引用 
	private textController theTextController;//文本显示控制单元
	private choiceController theChoiceController;//分支选择控制单元
	private UIController theUIController;//非文本显示的UI控制单元
//-------------------------------三阶段的初始化------------------------------------------------------//
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

		theItemNow = theRoot.GetComponent<thePlotItem> ();//初始化当前处理的项目
	}

	//其余元素的初始化，有些初始化只应该在剧本树建立完成之后做
	//在这个方法里面初始化各种控制单元
	void makeValueStart()
	{
		theTextController = this.GetComponent <textController> ();//文本控制单元
		theChoiceController = this .GetComponent <choiceController>();//分支选择控制单元
		theUIController = this.GetComponent <UIController>();//非文本显示的UI控制单元
	}
//---------------------------------------------------------------------------------------------------//

//-------------------------------剧本树的相关操作------------------------------------------------------//
	//鼠标点击空白部分就意味着galgame要跳转到下一行剧本了
	void moveToNextItem()
	{
		//剧本帧为空就不做操作了
		if (theItemNow == null)
			return;

		//如果是一个分支节点
		if (theItemNow.isASpitRoot ()) 
		{
			print ("这是一个分支节点，有待选择");

			//进入选择界面，在这里不标明要走哪一条路线，在UI选择的时候才会真的有所选择
			theChoiceController .openSelect(theItemNow .getChilds());

			//可选功能，在选择的时候关闭文本框
			//theTextController.shutTEXT ();

		} 
		else 
		{
			//theTextController.openTEXT ();
			theItemNow = theItemNow.moveToNext (); 
			playTheItem(theItemNow);
		}
	}


//操作剧本树单元的方法，这个程序真正用来玩的方法
//事实上，这个方法只会对单个分支节点生效
  public	void playTheItem(thePlotItem theItem = null)
	{
		//简单的防护措施
		if (theItem == null) 
		{
			print ("没有可控制的剧本元素");
			return;
		}
		theItemNow = theItem;
		//各种控制单元对这个单元的操作
		theTextController.setTheString (theItem);
		theUIController.makeShow (theItem);	
	}



//------------------------------------控制方法------------------------------------------------//
	//全是鼠标点击
	//如果射线命中的是UI，就执行UI操作，否则就是一般的鼠标嗲你操作
	//返回的是UI类别，不同类别的操作不同
	//1 -> 对话文本框
	int isOperatingUI ()
	{

		//因为我写的比较少，所以目前也就支持PC和按照两种平台的简单交互了
		if(Application .platform == RuntimePlatform.Android)
		{
		  if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) 
			{
				//print  ("当前触摸在UI上");
				return 1;
			} 
			else 
			{
				//print  ("当前没有触摸在UI上");
				return 0;
			}

		}
		else if (EventSystem.current.IsPointerOverGameObject ())
		{
			//print  ("当前触摸在UI上");
			return 1;
		} 
		else 
		{
			//print  ("当前没有触摸在UI上");
			return 0;
		}

		 

//下面这个方法是使用纯粹的2D模式而不是UI的方法显示的时候使用的
//但是这种方法对文本的兼容性一般，暂时先放在这里
//		Ray mRay = Camera.main.ScreenPointToRay (Input.mousePosition);
//		RaycastHit2D mHi = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
//
//		if (mHi.collider != null )
//		{
//
//			if (mHi.collider.gameObject.transform.tag == "UI") 
//			{
//				print ("UI");
//				return 1;
//			}
//		}
//		return 0;

	}
		
	//控制单元的操作方法
	public	void turnMake(int type = 0 )
	{
		//暂时先取消按在不同的地方有不同的效果的选定

		switch (type)
		{
		case 0: //不是UI操作
			{
				//点击文本框的时候，第一下是显示整个文本，第二下才是跳过
				if (!theTextController.isShowOver ()) 
				{
					//print ("本剧本元素直接全文显示");
					theTextController.showAll ();
				} 
				else 
				{
					//print ("跳转到下一个剧本元素");
					moveToNextItem ();	
				}
			}
			break;
		case 1:
			{
				//点击文本框的时候，第一下是显示整个文本，第二下才是跳过
				if (!theTextController.isShowOver ()) 
				{
					//print ("本剧本元素直接全文显示");
					theTextController.showAll ();
				} 
				else 
				{
					//print ("跳转到下一个剧本元素");
					moveToNextItem ();	
				}
			}
			break;
		}

	}

//------------------------------------系统自然回调方法------------------------------------------------//

	void Start () 
	{
		makeStart ();
		InitTheTree(thePlots [0]);
		makeValueStart ();
		//首先要为所有的控制单元初始化一个被控制的剧本元素
		playTheItem(theItemNow);
	}

	void Update () 
	{

		//为了迎合安卓和PC，仅仅使用这一种操作方法
		//打算使用射线进行区分
		if (Input.GetMouseButtonDown (0)) 
		{   
			int type = isOperatingUI ();
			turnMake(type);	 

		}
	}
}
