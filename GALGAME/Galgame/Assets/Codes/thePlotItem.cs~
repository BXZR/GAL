using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个类是每一个流程真正进行的过程
//控制每一个流程的图像，音频，背景和文字的显示
//这个是每一个流程的总控单元

//原计划是这个类指定图像，音频，背景和文字然后穿个其它的对象
//指定的内容由字符串，进行绑定

//目标是只在这里进行一些字符串的初始化，建立每一个剧本流程与各个资源的映射关系
//事实上这个类也仅仅做这一件事情

public class thePlotItem : MonoBehaviour {

	//剧本元素全世界唯一标志
	//在初始化的进行计算得到
	//这个标志用于存档和跳转
	//顺带一提，存档的标志有两个，一个是ID一个是文件名
	private int thePlotItemID = 100000000;//唯一标识的ID，这个只读
	public int ThePlotItemID 
	{
		get//只读
		{
			return thePlotItemID;
		}
	}
	public int thePlotBranchID = 0;
	public int theBranchTalkID = 0;
	public string theSpeekerName = "";//这个剧本元素说话的人
	public string theBackPictureName = "";//背景图
	public string theTalkInformation = "";
	public string theTitleName = "";//额外的缩略标记，用于选项，实际上只有选项分支节点才会显示
	public string [] thePartyplayers = {"" , "" , ""}; //在屏幕显示的一共可以有三个
	public string musicName = "";//背景音乐名字
	public string soundName = "";//音效名字
	public string speakSoundName = "";//语音音效的名字
	public string extraAction = "";//额外动作脚本配置
	public int aimPlotItemID = 0;//支持随意跳转，这一项标记目标plotItem的ID
	//经过这个item能够对三个可攻略人物的好感度的影响
	//目前这是一个非常简单的做法
	//每一个人的戏份多，好感度也就越多
	//这个的实现基于存档和读档
	public float [] loveValue = {0,0,0};
	//果然还是更喜欢使用局本树的方法处理
	private  List<thePlotItem>  theChildItems ;//子控制单元 

	public float waitTimeForAutoSkip = 10f;//这个剧本元素的时间，超过这个时间自动跳到下一个

	//第一阶段的初始化
	public void makeCreate1(int thePlotIn,int theBranchIn,string speekerNameIn,string backPictureNameIn ,string theTalkIn,string titleIn)
	{
		 thePlotBranchID = thePlotIn;
		 theBranchTalkID = theBranchIn;
		 theTalkInformation = theTalkIn;
		theSpeekerName = speekerNameIn;
		theBackPictureName = backPictureNameIn;
		theTitleName = titleIn;
		thePlotItemID = thePlotBranchID * 1000 + theBranchTalkID;
		this.gameObject.name = thePlotBranchID + "_" + theBranchTalkID;
		waitTimeForAutoSkip = theTalkInformation.Length * systemInformations .plotTimeWait;

	}
	//扩展方法2
	public void makeCreate2(string player1, string player2 , string player3, string musicNameIn ,string soundNameIn,int  aimID )
	{
		if(string .IsNullOrEmpty (player1))
			thePartyplayers [0] = "noOne";
		else
			thePartyplayers [0] = player1;
		if(string .IsNullOrEmpty (player2))
			thePartyplayers [1] = "noOne";
		else
			thePartyplayers [1] = player2;
		if(string .IsNullOrEmpty (player3))
			thePartyplayers [2] = "noOne";
		else
			thePartyplayers [2] = player3;

		musicName = musicNameIn;
		soundName = soundNameIn;
		aimPlotItemID = aimID;
	}

	//扩展方法3
	public void  makeCreate3(string theSpeakSoundNameIn,string extraActionIn)
	{
		speakSoundName = theSpeakSoundNameIn;
		extraAction = extraActionIn;
		makeLoveValue ();
	}


	//额外初始化方法1
	//实际上只有这三个人会有好感度系统
	private void  makeLoveValue()
	{
		string theSpeakInName = theSpeekerName.Split ('_') [0];
		switch(theSpeakInName)
		{
		case "alice":
			{
				loveValue [0] = 0.001f;
				break;
			}
		case "fione":
			{
				loveValue [1] = 0.001f;
				break;
			}
		case "jik":
			{
				loveValue [2] = 0.001f;
				break;
			}
		}
	}

	//对外增加好感度的方法
	public void  makeAddLoveValue()
	{
		for (int i = 0; i < systemInformations.lovePercent.Length; i++) 
		{
			systemInformations.lovePercent [i] += loveValue [i];//增加好感度
			if (systemInformations.lovePercent [i] >= 1 && extraHFile.loveOpend [i]== false)
			{
				extraHFile.loveOpend [i] = true;//好感度只需要超过1一次就可以激活特典
				informationPanel.showInformation("特典《"+ extraHFile.PlotName[i]+"》已经被激活");
			}
			systemInformations.lovePercent [i] = Mathf.Clamp (systemInformations.lovePercent [i],0f,1f);
		}
	}

	//这个方法在创建树的时候进行
	//控制直接子类的方法
	//剧本单元的递归初始化从这里开始
	public  void makeStart()
	{
		theChildItems = new List<thePlotItem> ();
		int count = this.transform.childCount;
		for(int i=0;i< count; i++)
		{
			Transform theChild = this.transform.GetChild (i);
			if (theChild.GetComponent<thePlotItem> ()) 
			{
				thePlotItem theItem = theChild.GetComponent<thePlotItem> ();
				theChildItems.Add (theItem);
				theItem.makeStart ();//递归初始化
			}
		}
	}


	public List<thePlotItem> getChilds()
	{
		return this.theChildItems;
	}

	//是不是一个具有分支的节点，如果是，需要有特殊的处理方式
	//这个处理的过程不再这里执行，而在主要控制单元里面调用
	//此方法是一个判定标记
	public bool  isASpitRoot()
	{
		//原始的基础方法
		//if (this.theChildItems.Count > 1)
		//	return true;
		//return false;

		//下面的这种方法似乎更加简洁也更快
		return (this.theChildItems.Count > 1);
	}

	//跳转到下一个节点
	//单个分支的节点用这种方式跳转
	public thePlotItem moveToNext(thePlotItem theAim = null)
	{
		//直接控制跳转
		if (theAim != null)
			return theAim;
		//实际上只有单个分支的节点才会调用这个方法
		//但是稳妥起见还是用下标标记吧
		if(this.theChildItems .Count >0)
		return this.theChildItems [0];

		return null;
	}

 

	//void Start () {
		
	//}
	

	//void Update () {
//		if (Input.GetKeyDown (KeyCode.C))
//			print (this.name + "---" + this.theChildItems.Count + "----" + isASpitRoot ());
	//}
}
