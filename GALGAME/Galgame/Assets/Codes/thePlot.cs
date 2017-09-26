﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个类是这个游戏的剧本类，也就是总体控制类
//这个类将会控制整个游戏的流程，但是只能够控制每一个流程的抽象内容
//具体的流程内容将由thePlotItem来进行

//原计划是每一个剧情长度片一个场景
//用一个总控的文本控制单元来获得和跳转进度
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class thePlot : MonoBehaviour {

	public TextAsset thePlotNow;//最初的剧本文件在这里保存，之后所有的跳转都由剧本自动执行
	private plotTreeMaker theTreeMake;//创建剧本树的控制单元
	public  List<thePlotItem> roots;//局本树丛的小树根

	private textController theTextController;//文本显示控制单元
	private choiceController theChoiceController;//分支选择控制单元
	private UIController theUIController;//非文本显示的UI控制单元

	private MusicController theMusicController;//背景音乐控制单元
	private saveLoadController theDataController;//存档控制单元，用于强制跳转

	//不使用invoke因为这个需要非常频繁地开关
	public float watiForSkipTimer = 1000;
	public bool isAutoWait = true;//默认自动开启自动跳转模式

	private thePlotItem  theItemNow = null;//当前控制的剧本帧引用 

	//其实就是跳转用，一开始的时候可以跳转到不同的地方
	//事实上这个是给开发者用的接口
	public int startID = -99;//默认初始ID


	//控制时间速度
	private float speedModeTimeScale= 17.0f;
	private float normalModeTimeScale = 1.0f;

	public thePlotItem TheItemNow
	{
		get
		{
			return theItemNow;
		}
	}
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
		watiForSkipTimer = theItemNow .waitTimeForAutoSkip;
	}

	//其余元素的初始化，有些初始化只应该在剧本树建立完成之后做
	//在这个方法里面初始化各种控制单元
	void makeValueStart()
	{
		theTextController = this.GetComponent <textController> ();//文本控制单元
		theChoiceController = this .GetComponent <choiceController>();//分支选择控制单元
		theUIController = this.GetComponent <UIController>();//非文本显示的UI控制单元

		theMusicController = this.transform .GetComponentInChildren<MusicController> ();//音频比较特殊，需要父子关系，因为需要同时播放多个音效，音乐
		theDataController = this.GetComponent <saveLoadController>();

		Time.timeScale = normalModeTimeScale ;//强制规定时间速度，用于加速
	}

	public  void  makeAllStart()
	{
		makeStart ();
		InitTheTree(thePlotNow);
		makeValueStart ();

	
	}
//---------------------------------------------------------------------------------------------------//

//-------------------------------剧本树的相关操作------------------------------------------------------//
	//鼠标点击空白部分就意味着galgame要跳转到下一行剧本了
	void moveToNextItem()
	{
		//剧本帧为空就不做操作了
		if (theItemNow == null)
			return;

		//直接跳转的权限高于一切，虽然大多数情况下此项为空
		if (theItemNow.aimPlotItemID > 0) 
		{
			theDataController.loadItemForSkip(theItemNow.aimPlotItemID);
			return;
		}

		//如果是一个分支节点
		if (theItemNow.isASpitRoot ()) 
		{
			//print ("这是一个分支节点，有待选择");
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
  public  void playTheItem(thePlotItem theItem = null)
	{
		//简单的防护措施
		if (theItem == null) 
		{
			print ("没有可控制的剧本元素");
			SceneManager.LoadScene ("start");
			return;
		}
		theItemNow = theItem;
		watiForSkipTimer = theItemNow .waitTimeForAutoSkip;
		//各种控制单元对这个单元的操作
		theTextController.setTheString (theItem);
		theUIController.makeShow (theItem);	
		theMusicController.playBackMusic (theItem);
	}


	//判断当前的剧本元素是不是已经完成了
	private bool isTheItemNowOver()
	{
		bool isOver = true ;
		return isOver && theTextController.isShowOver ();
	}

//------------------------------------控制方法------------------------------------------------//
	//全是鼠标点击
	//如果射线命中的是UI，就执行UI操作，否则就是一般的鼠标嗲你操作
	//返回的是UI类别，不同类别的操作不同
	//0 -> 不是UI
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
		//这是两个平台的不同判断方法
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
		//这是因为有一些UI的button等功能也是要提供的，但是这个点击事件与鼠标点击事件实际上有一点冲突
		//所以用这种方法区分点击在不同位置的功能
		//点击到UI上就立即完成本剧本元素的显示，并且触发UI的事件
		//点击到不是UI的地方，第一次可以完成本剧本元素的显示，下一次点击可以跳到下一个剧本元素
		switch (type)
		{
		  case 0: //不是UI操作
			{
				//点击文本框的时候，第一下是显示整个文本，第二下才是跳过
				//这个只是因为个人比较喜欢这样的方式，当然也可以直接
				//theTextController.showAll ();
				//moveToNextItem ();	
				//快速跳过
				if (!isTheItemNowOver()) 
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
		case 1: //是的，是UI操作
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
					//moveToNextItem ();	
				}
			}
			break;
		}
	}
 
//------------------------------------系统自然回调方法------------------------------------------------//

	void Start () 
	{
		makeAllStart ();
		//首先要为所有的控制单元初始化一个被控制的剧本元素
		if (startButtons.loadMemory) {
			//因为只有一个应用的存档，倒是简单了
			//说起来这个是自动存档的原理
			this.theDataController.loadItem ();

		} 
		else if (startID > 0)
		{
			theDataController.loadItemForSkip(startID);
			startID = -99;
		}
		else
		{
			playTheItem(theItemNow);
		}
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
		//这是自动跳转的处理，用的是一个统一的时间
		//如果这个时候玩家点击鼠标，调用了moveToNextItem ();
		//时间就会刷新
		if (isAutoWait) 
		{
			if (watiForSkipTimer > 0)
			{
				watiForSkipTimer -= Time.deltaTime;
				if (watiForSkipTimer < 0) 
				{
					moveToNextItem ();
				}
			}
		}

		if (Application.platform != RuntimePlatform.Android) 
		{
			if (Input.GetKey (KeyCode.LeftControl))
			{
				Time.timeScale = speedModeTimeScale;
			}
			if (Input.GetKeyUp (KeyCode.LeftControl)) 
			{
				Time.timeScale = normalModeTimeScale ;
			}
		}
	}
}
