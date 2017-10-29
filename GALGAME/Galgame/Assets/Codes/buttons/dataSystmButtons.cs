using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个类专门用于保留屏幕右侧的按钮
//文件存储系统的按钮事件方法
using UnityEngine.UI;


public class dataSystmButtons : MonoBehaviour {

	public GameObject thePanelForDataSystem;//被控制显示的面板
	public GameObject theAllController;//总控制器
	public GameObject [] theChildPanel;//子panel
	private saveLoadController theDataController;//文件存储系统的控制器
	private thePlot thePlotController;//剧本控制器

	void Start () {
		theDataController = theAllController.GetComponent <saveLoadController> ();
		thePlotController = theAllController.GetComponent <thePlot> ();
	}

	public void saveData()
	{
		//事实上为了简化操作仅仅使用了一个存档，当然可以扩展，但是手机端没什么必要
		//所以为了统一处理就暂时仅仅设置呢一个存档
		theDataController.saveItem (thePlotController .TheItemNow,0);
		informationPanel.showInformation ("快速存档成功");
	}

	public void loadDate()
	{
		theDataController.loadItem (0);
		informationPanel.showInformation ("快速读档成功");
	}



	public void makeStart()
	{

		//核心是下面这两句
		//对于总开关按钮【0】项目一定是总面板，注意注意
		if (theChildPanel.Length <= 0)
			return;
	    
		if (theChildPanel [0].activeInHierarchy == false) 
		{
			systemInformations.isChildPanelShows = true;
			//Time.timeScale = 0f;//界面开启的时候需要暂停
			systemInformations .isAutoWait = false;
			theChildPanel [0].SetActive (true);
		}
		else
		{
			bool close = false;
			for (int i = 0; i < theChildPanel.Length; i++)
			{
				if (theChildPanel [i].activeInHierarchy)
				{
					close = true;
					break;
				}
			}
			if (close)
			{
				for (int i = 0; i < theChildPanel.Length; i++)
				{
					theChildPanel [i].SetActive (false);
				}
				systemInformations.isChildPanelShows = false;
				systemInformations .isAutoWait = systemInformations .isAutoNowSave;
				print ("systemInformations .isAutoWait " +systemInformations .isAutoWait );
				//Time.timeScale = systemInformations.timeScaleNow;//界面开启的时候需要暂停
			}

		}
//下面是原始的方法，仅做参考，不是什么好方法
//		systemInformations . isThePanelShows = !systemInformations . isThePanelShows;
//		thePanelForDataSystem.gameObject.SetActive (systemInformations . isThePanelShows);
//		if (systemInformations . isThePanelShows == false) 
//		{
//			if (theChildPanel.Length > 0) 
//			{
//				//systemInformations . isChildPanelShows = false;
//				for(int i =0 ; i < theChildPanel .Length ; i++)
//				   theChildPanel[i].gameObject.SetActive (false);
//			} 
//			else 
//			{
//				//print (this.gameObject.name +" -----");
//			}
//		}

	}

	//回到初始界面
	public void  backStartScene()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene ("start");
	}

	public void gameOver()
	{
		Application.Quit ();
	}

	public void  UseChildSysPanelOpen()
	{
		for(int i = 0; i <theChildPanel.Length ; i++)
		theChildPanel[i].gameObject.SetActive ( !theChildPanel[i].gameObject.activeInHierarchy );
	}

	//控制快进与否的按钮
	public  void makeSkipControll()
	{
		systemInformations.skipControll ();
		//额外的一些展示效果
		if (systemInformations.ISSkiping)
			this.GetComponentInChildren<Image> ().color = Color.yellow;
		else
			this.GetComponentInChildren<Image> ().color = Color.white;
	}
}
