using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个类专门用于保留屏幕右侧的按钮
//文件存储系统的按钮事件方法
using UnityEngine.UI;


public class dataSystmButtons : MonoBehaviour {

	public GameObject thePanelForDataSystem;//被控制显示的面板
	public GameObject theAllController;//总控制器
	public GameObject theChildPanel;//子panel
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
		systemInformations . isThePanelShows = !systemInformations . isThePanelShows;
		thePanelForDataSystem.gameObject.SetActive (systemInformations . isThePanelShows);
		if (systemInformations . isThePanelShows == false) 
		{
			if (theChildPanel) 
			{
				systemInformations . isChildPanelShows = false;
				theChildPanel.gameObject.SetActive (false);
			} 
			else 
			{
				//print (this.gameObject.name +" -----");
			}
		}

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
		//print ("isChildPanelShows = " + isChildPanelShows);
		systemInformations . isChildPanelShows = !systemInformations . isChildPanelShows;
		theChildPanel.gameObject.SetActive (systemInformations . isChildPanelShows);
	}


}
