using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个类专门用于保留屏幕右侧的三个按钮
//文件存储系统的按钮事件方法
public class dataSystmButtons : MonoBehaviour {

	public GameObject thePanelForDataSystem;//被控制显示的面板
	public GameObject theAllController;//总控制器
	private saveLoadController theDataController;//文件存储系统的控制器
	private thePlot thePlotController;//剧本控制器

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
}
