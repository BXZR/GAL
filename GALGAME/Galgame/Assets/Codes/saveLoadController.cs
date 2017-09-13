using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//这个类专门用于读写存档
using System.IO;
using System;


public class saveLoadController : MonoBehaviour {

	//存档思路
//	获取到的是当前的plotItem的ID和plot文件的文件名，
//	首先全部加载相关的plot文件，然后用查询的方法定位到ID对应的plotItem
//	最后赋值到总控制单元的itemNow里面就可以了


	public void saveItem(thePlotItem theItem, int saveID = 0)
	{
		if (theItem == null)
			return;


		string theSaveInfotmation = theItem.ThePlotItemID.ToString();
		saveInformation (getPath(saveID) , theSaveInfotmation);
	    
	}

	public void loadItem(int saveID = 0)//0,1,2号码的存档
	{
		if (saveID > 2 || saveID < 0)
			return;
		string informationGet =  loadInformation(getPath(saveID));
		if (string.IsNullOrEmpty (informationGet)) 
		{
			//读取的信息不能用
			return;
		}
		int theItemID = Convert.ToInt32 (informationGet);
		int thePlotID = theItemID / 100000;

		//销毁并且重新建立一棵树
		thePlot controller = this.GetComponent <thePlot> ();

		for (int i = 0; i < controller.roots.Count; i++)
			DestroyImmediate (controller .roots [i].gameObject);
		controller.roots = new List<thePlotItem> ();

		controller.thePlotNow = Resources.Load <TextAsset>("thePlots/thePlot"+thePlotID);
		controller.makeAllStart ();
//		//根据ID查找对应的item
		thePlotItem [] allItemNow = controller.GetComponentsInChildren<thePlotItem>();
		thePlotItem nowUse = null;
		//print ("loadAll = " + allItemNow.Length);
		for (int i = 0; i < allItemNow.Length; i++) 
		{
			//print ("----" + allItemNow [i].ThePlotItemID + "------" + theItemID);
			if (allItemNow [i].ThePlotItemID == theItemID) 
			{
				nowUse = allItemNow [i];
				break;
			}
		}
		if (nowUse != null)
			controller.playTheItem (nowUse);
	}

	//这个用于强制跳转
	public void loadItemForSkip(int aimID)//0,1,2号码的存档
	{
		int theItemID = aimID;
		int thePlotID = theItemID / 100000;

		//销毁并且重新建立一棵树
		thePlot controller = this.GetComponent <thePlot> ();

		for (int i = 0; i < controller.roots.Count; i++)
			DestroyImmediate (controller .roots [i].gameObject);
		controller.roots = new List<thePlotItem> ();

		controller.thePlotNow = Resources.Load <TextAsset>("thePlots/thePlot"+thePlotID);
		controller.makeAllStart ();
		//		//根据ID查找对应的item
		thePlotItem [] allItemNow = controller.GetComponentsInChildren<thePlotItem>();
		thePlotItem nowUse = null;
		//print ("loadAll = " + allItemNow.Length);
		for (int i = 0; i < allItemNow.Length; i++) 
		{
			//print ("----" + allItemNow [i].ThePlotItemID + "------" + theItemID);
			if (allItemNow [i].ThePlotItemID == theItemID) 
			{
				nowUse = allItemNow [i];
				break;
			}
		}
		if (nowUse != null)
			controller.playTheItem (nowUse);
	}

	private string getPath(int saveID)
	{

		string thePath = "";
		//打算用streamingAsset来做存档，文件的路径不同需要做一下区分
		if (Application.platform == RuntimePlatform.Android)
		{
			//thePath = "jar:file:///data/app/com.Suck.gal_huiyi.apk/!/assets/" + "save" + saveID +".txt";
			thePath = Application .persistentDataPath+"/save" + saveID +".txt";
		} 
		else
		{
			//StreamingAssets 确实很犀利但是安卓只有只读的权限，所以受限制了
			//当然PC端两种方法都可以了
			//但是为了保持统一，也方便测试，暂时只使用datapath
			//thePath = Application.dataPath + "/StreamingAssets/save" + saveID + ".txt";
			thePath = Application .persistentDataPath+"/save" + saveID +".txt";
		}
		return thePath;
	}

	//下面是很万能的较底层的存储方法
	//我已经好几个工程不断引用这段代码了

	/**********************************一般读写文件方法*********************************/
	//单个信息写入适合这种方法
	public void saveInformation( string fileName , string information )
	{
		FileStream aFile = new FileStream( fileName , FileMode.OpenOrCreate);
		StreamWriter sw = new StreamWriter(aFile);
		sw.Write(information);
		sw.Close();
		sw.Dispose();
	}

	public string loadInformation(string fileName)
	{
		using (StreamReader reader= File.OpenText (fileName))
		{
			string s = 	reader .ReadToEnd();
			reader .Close();
			return s;
		}
	}
	/***********************************************************************************/

	void Update ()
	{

//下面这些是保留下来的测试代码
//		if (Input.GetKeyDown (KeyCode.A)) 
//		{
//			thePlotItem theItem = this.GetComponent <thePlot> ().TheItemNow;
//				saveItem (theItem , 0);
//		}
//		if (Input.GetKeyDown (KeyCode.S)) 
//		{
//			thePlotItem theItem = this.GetComponent <thePlot> ().TheItemNow;
//			saveItem (theItem , 2);
//		}
//		if (Input.GetKeyDown (KeyCode.D)) 
//		{
//			loadItem (2);
//		}
	}
}
