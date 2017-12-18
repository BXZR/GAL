using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class startButtons : MonoBehaviour {

	public int index = -99;//编号，用于定位存档，能使用的有0-9
	private Image theSavePicture = null;//保存截图显示
	//这个存档按钮的提示文本，显示的是存档最后被修改的事件
	private Text theShowTimeText;

	void Start () 
	{
		if(index >=0 && index <=9)
		theSavePicture = this.transform.Find ("saveLoadPicture").GetComponent<Image>();
		try
		{
		  theShowTimeText = this.transform.Find ("TimeTextShow").GetComponentInChildren<Text> ();
		  if(theShowTimeText) 
			theShowTimeText.text = "空档";
		}
		catch
		{
			//print ("这不是用来显示存档的按钮，所以没有此项配置");
		}
		StartCoroutine (loadPicture());
	}



	//从头开始
	public void makeStart()
	{
		systemInformations . loadMemory = false;
		SceneManager.LoadScene ("forFront");

	}

	//带存档的开始
	public void loadStart()
	{
		string chaeckPath =  Application .persistentDataPath+"/save"  + this.index + ".jpg";
		if (File.Exists (chaeckPath) && index >= 0 && index <= 9) 
		{
			//没有存档就点击无效
			systemInformations.loadMemory = true;
			systemInformations.indexForLoad = this.index;
			SceneManager.LoadScene ("gal_1");
		}
	}

	//结束游戏
	public void  exitGame()
	{
		Application.Quit ();
	}

	//回到初始界面
	public void  backStartScene()
	{
		SceneManager.LoadScene ("start");
	}

	IEnumerator loadPicture()
	{
		yield return  new WaitForSeconds (0.05f);
		string  path   = @"file:///" + Application .persistentDataPath+"/save"  + this.index+ ".jpg";
		string chaeckPath =  Application .persistentDataPath+"/save"  + this.index + ".jpg";
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

			//加载修改时间
			FileInfo fi = new FileInfo(chaeckPath);
			if (theShowTimeText) 
			{
				//因为快速存档是没有这个时间标记的，所以需要做一下判断
				theShowTimeText.text = fi.LastWriteTime.ToString ();
			}

		} 

	}
}
