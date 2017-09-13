using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个类专门用于保留屏幕右侧的三个按钮
//文件存储系统的按钮事件方法
using UnityEngine.UI;


public class dataSystmButtons : MonoBehaviour {

	public GameObject thePanelForDataSystem;//被控制显示的面板
	public GameObject theAllController;//总控制器
	private saveLoadController theDataController;//文件存储系统的控制器
	private thePlot thePlotController;//剧本控制器

	public Sprite theAutoPicture;//自动运行的图
	public Sprite theNotAutoPicture;//不自动运行的图

	private bool isThePanelShows = false;//存档面板是不是显示了

	// Use this for initialization
	void Start () {
		theDataController = theAllController.GetComponent <saveLoadController> ();
		thePlotController = theAllController.GetComponent <thePlot> ();
	}

	public void saveData()
	{
		//事实上为了简化操作仅仅使用了一个存档，当然可以扩展，但是手机端没什么必要
		//所以为了统一处理就暂时仅仅设置呢一个存档
		theDataController.saveItem (thePlotController .TheItemNow,0);
		informationPanel.showInformation ("存档成功");
	}

	public void loadDate()
	{
		theDataController.loadItem (0);
		informationPanel.showInformation ("读档成功");
	}

	
	public void makeStart()
	{
		isThePanelShows = !isThePanelShows;
		thePanelForDataSystem.gameObject.SetActive (isThePanelShows);
	}

	//是否开启自动模式
	//所谓自动模式就是一个人说完话之后一段时间之后自动跳转到下一句对话
	public void autoSwitcher()
	{
		thePlotController.isAutoWait = !thePlotController.isAutoWait;
		if (thePlotController.isAutoWait) 
		{
			this.GetComponent <Image> ().sprite = theAutoPicture;
			informationPanel.showInformation ("开启自动播放模式");
		} 
		else 
		{
			this.GetComponent <Image> ().sprite = theNotAutoPicture;
			informationPanel.showInformation ("关闭自动播放模式");
		}
	}
}
