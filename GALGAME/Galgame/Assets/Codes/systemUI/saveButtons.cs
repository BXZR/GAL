﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//system面板中那些save存档的按钮
using UnityEngine.UI;
using System.IO;


public class saveButtons : MonoBehaviour {

	 //存档编号
	//注意：存档的编号从1开始
	//默认的quicksave的存档编号是0
	public int saveIndex = 0;
	public GameObject theAllController;//总控制器
	private saveLoadController theDataController;//文件存储系统的控制器
	private thePlot thePlotController;//剧本控制器
	private saveLoadPicture pictureMaker;//截图控制单元
	private Image theSavePicture = null;//保存截图显示

	void Start () {
		theDataController = theAllController.GetComponent <saveLoadController> ();
		thePlotController = theAllController.GetComponent <thePlot> ();
		pictureMaker = theAllController.GetComponent <saveLoadPicture> ();
		//saveLoadPicture名字是固定的
		theSavePicture = this.transform.Find ("saveLoadPicture").GetComponent<Image>();
		pictureMaker.getCamera ();
		makeFlash ();
	}



	public void  quickSave()
	{
		if (systemInformations.isScene == false) 
		{
			theDataController.saveItem (thePlotController.TheItemNow, saveIndex);
			savePicture ();
			StartCoroutine (loadPicture ());
			informationPanel.showInformation ("存档");
		} 
		else
		{
			informationPanel.showInformation ("回忆模式下不能快速存档");
		}
	}
	public void  makeSaveOrLoad()
	{
		if (systemInformations.isScene) 
		{
			informationPanel.showInformation ("回忆模式不能存档或者读档");
		} 
		else 
		{
			if (systemInformations.isSaving)
			{
				theDataController.saveItem (thePlotController.TheItemNow, saveIndex);
				savePicture ();
				StartCoroutine (loadPicture ());
				CGModeFile.saveCGFile ();
				SceneModeFile.saveSceneFile ();
				informationPanel.showInformation ("存档");
			}
			else 
			{
				theDataController.loadItem (saveIndex);
				informationPanel.showInformation ("读档");
			}
		}
	}

	//存档的时候的截图
	void  savePicture()
	{
		StartCoroutine ( pictureMaker.OnScreenCapture2(this .saveIndex));
	}

	//更新sysytempabel的时候调用，使得自身的子物体图像可以显示为存档贴图
	public void makeFlash()
	{
		StartCoroutine (loadPicture());
	}

	IEnumerator loadPicture()
	{
		yield return  new WaitForSeconds (0.1f);
		string  path   = @"file:///" + Application .persistentDataPath+"/save"  + this.saveIndex + ".jpg";
		string chaeckPath =  Application .persistentDataPath+"/save"  + this.saveIndex + ".jpg";
		//print (path);
		if (File.Exists (chaeckPath)) 
		{
			WWW theWWW = new WWW (path);
			yield return theWWW;
			//加载图片
			Texture2D theTextureIn = theWWW.texture;
			if (theTextureIn != null && theSavePicture != null) 
			{
				theSavePicture.sprite = Sprite.Create (theTextureIn, new Rect (0, 0, theTextureIn.width, theTextureIn.height), new Vector2 (0, 0));
			} 
			else 
			{
				print ("load fail!");
			}
		}

	}

}
