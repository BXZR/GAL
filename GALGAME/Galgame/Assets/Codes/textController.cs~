using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textController : MonoBehaviour {

	//效果，通过某个方法传入一个字符串，然后通过用“蹦字”的方式显示出一串文字
	//推荐用协程和Invoke但是需要注意不可以重复开启，需要加上标记
	//此外在销毁的时候需要取消调用

	public Text theShowInformationText;//用于显示信息的那个Text
	private bool isStarted = false;
	private string theInformation = "";//用于展示的完整信息文本（完整版）
	private int indexNow = 0;//完整文本的下标获取值
	private string theShowString = "";//正式用于显示的字符串
	private float theFlashWaitTime = 0.2f;//Invoke的更新速率，这个数值越小更新越快
	private string showName = "【???】";//显示的任务的名字
	//对外调用接口
	//外面的方法只会知道这一个方法并加以调用
	//此外这个方法仅仅会被调用一次
	public void setTheString(thePlotItem theItem)
	{
		theShowInformationText.color = systemInformations.getTextColor (theItem.ThePlotItemID);
		string getName = systemInformations.getShowNameWithProName (theItem.theSpeekerName.Split ('_')[0]);
			if(string .IsNullOrEmpty (getName) == false)
			{
			showName = "【" + getName+ "】";
			}
			else
			{
			showName = "";
			}
		//下面是具体的工作细节，上面是标记量
		this.theInformation =  theItem .theTalkInformation;
		//记录面板，作为已经读到的内容(分隔符不算)
		if (theInformation.StartsWith ("---") == false) 
		{
			systemInformations.saveRead (showName + "\n" + theInformation , theItem.speakSoundName);
		}
		indexNow = 0;//这个可以更稳妥地关掉显示文本的显示器
		theShowString = "";

	}

	public void setTheString(string theInformationGet)
	{
			//下面是具体的工作细节，上面是标记量
			this.theInformation = theInformationGet;
			indexNow = 0;//这个可以更稳妥地关掉显示文本的显示器
		    theShowString = "";
 			
	}

	public void showAll()//直接显示所有的内容
	{
		theShowInformationText.text =showName+"\n  "+ theInformation;
		indexNow = 999999;
	}

 

	public bool isShowOver()//所有需要显示的信息是否都显示完
	{
		//这是一个外部查询用的方法
		//在这个类里面没有必要使用
		if (indexNow < theInformation.Length)
			return false;
		return true;
	}

	public void openTEXT()
	{
		theShowInformationText.transform .parent.gameObject.SetActive (true);
	}

	public void shutTEXT()
	{
		theShowInformationText.transform .parent.gameObject.SetActive (false);
	}

	private	void makeShowUpdate()
	{
		if (indexNow < theInformation.Length) 
		{
			theShowString += theInformation [indexNow];
			//更新显示的文本就可以了
			theShowInformationText .text = showName+"\n  "+theShowString;//显示文本，必有改动吧，纯文本可能会有各种小问题吧
			indexNow ++;
		}
	}

	private void makeUpdate()
	{
		//这个时间需要配合更新总时间长度，实际上直接使用invoke做更新也行，但是可动态配置的特性没有这样做好
		theFlashWaitTime -= 0.05f;
		if (theFlashWaitTime < 0) 
		{
			theFlashWaitTime = systemInformations.textShowTime;
			makeShowUpdate ();
		}
	}
	void Start () 
	{
		//值得注意的是TimeScale也可以影响InvokeRepeate
		//因此直接这样写不会出错，还可以减少一定计算量
		InvokeRepeating ("makeUpdate",0f,0.05f);
	}


	//void Update ()
	//{
		
	//}

	void OnDestroy()//非常必要
	{
		CancelInvoke ();//取消Invoke调用
	}

}