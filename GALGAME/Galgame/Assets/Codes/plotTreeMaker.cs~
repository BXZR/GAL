using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class plotTreeMaker : MonoBehaviour {

	//文本剧本树
	//最开头的第一位表示剧本文件编号
	//例如 100,001 在剧本文件1
	//又如 200,001 在剧本文件2
	//	100,001,100号分支第001句话
	//	100,002,100号分支第002句话
	//	110,001,110号分支第001句话
	//	120,001,120号分支第001句话
	//
	//
	//	110,120都是100的子分支
	//	111,112都是110的子分支
	//	也就是说每一个节点最多出9个分支1-9，每一个分支一共可以存储999句话
	//
	//  并且，这个剧本树只可以有最多三层分支

	public GameObject theProfabForItem;
	private List<thePlotItem> theStartItems; 
	private  Dictionary < int ,  List<thePlotItem>>  theStartItemSaver;//用于初始化建立剧本树的脚本
	private List<int> theIDS; //字典中key的List

	private Transform theRootTranform;//保留一个树根的引用，用于返回和控制器连接在一起

	//这是一个用于缓冲的List
	List<thePlotItem> theBranch ;

	public  Transform  makeATree(TextAsset thePlot)
	{
		theStartItems = new List<thePlotItem> ();
		create (thePlot);
		sortInBranch ();
		setParentsForBranchPrivate ();
		setBranchParent ();

		return theRootTranform;//返回树根
	}


	//建立所有的plotItem并作简单的初始化
	//根据一个剧本控制生成一棵树
	void create(TextAsset thePlot)
	{
		string[] theInformation = thePlot.text.Split ('\n');
		for (int j = 0; j < theInformation.Length; j++) 
		{
			if (theInformation[j].StartsWith ("//") || string .IsNullOrEmpty (theInformation[j]))
				continue;
			//"//"是一个文本编辑用的分隔符号，顺带用于注释
			try
			{
				//在这里需要追加更加细致的设定和分割
				//并且首位随意添加空格
				string[] theInformationIn = theInformation [j].Split (',');
				int thePlotBranchID = Convert.ToInt32 (theInformationIn[0].Trim());
				int theBranchTalkID = Convert.ToInt32 (theInformationIn [1].Trim());

				string theSpeekerName = theInformationIn [2].Trim();
				/*
                  speakerName  说话的人
                  SpeakerPictureName 说话的人的图的特征
                  speakerName_picName 就是资源的名字
                  例如 alice_happy
                  至于中文名称显示翻译，用一个数组做映射就行，反正也没几个人
                */
				string player1 = theInformationIn[3].Trim();//参与说话的第一个人
				string player2 = theInformationIn[4].Trim();//参与说话的第二个人
				string player3 = theInformationIn[5].Trim();//参与说话的第三个人

				string theBackPictureName = theInformationIn [6].Trim();
				string theTalkInformation = theInformationIn [7].Trim();
				string title = theInformationIn [8].Trim();//额外标记

				string musicName = theInformationIn [9].Trim();//背景音乐名称
				string soundName = theInformationIn [10].Trim();//音效模名称
				//print("music :" + musicName +"   sound :"+ soundName);

				string aimIDIN = theInformationIn [11].Trim();
				int aimID  = -1;
				if (string .IsNullOrEmpty( theInformationIn [11].Trim()) == false)
				{
					try
					{
					aimID  = Convert .ToInt32(theInformationIn [11].Trim());//跳转目标
					}
					catch
					{
						aimID = -2;
					}
				}
				GameObject theNewItem = GameObject.Instantiate <GameObject> (theProfabForItem);
				thePlotItem theItem = theNewItem.GetComponent <thePlotItem> ();
				theItem.makeCreate1 (thePlotBranchID , theBranchTalkID , theSpeekerName ,theBackPictureName,theTalkInformation ,title);
				//print("====="+player1 +"======"+player2 +"======"+player3);
				theItem .makeCreate2(player1 , player2 , player3,musicName ,soundName,aimID);
				theStartItems.Add (theItem);//直接按照分支进行整理，但是顺序和父子关系在这一步还没有确定

			}
			catch
			{
				//这一行可能是空行，直接跳过就可以了
				continue;
			}
		}

	}

	//切分每一个分支
	//每一个分支自身排序，分支内部处理
	void sortInBranch()
	{
		//建立了完整的字典
		theIDS = new List<int> ();
		for (int i = 0; i < theStartItems.Count; i++) 
		{
			if (theIDS.Contains (theStartItems [i].thePlotBranchID) == false) 
			{
				theIDS.Add (theStartItems [i].thePlotBranchID);
			}
		}
		theStartItemSaver = new Dictionary<int, List<thePlotItem>> ();


		for(int i = 0; i < theIDS.Count ; i++)
		{
			theBranch = new List<thePlotItem> ();
			for (int j = 0; j < theStartItems.Count; j++) 
			{

				if (theStartItems [j].thePlotBranchID == theIDS [i])
				{
					theBranch.Add (theStartItems [j]);
				
				}
			}
			theStartItemSaver.Add (theIDS [i], theBranch);
		}

		//测试用的输出方法
		//		for(int i= 0;i< theStartItemSaver[theIDS [0]] .Count ; i++)
		//		{
		//			print (theStartItemSaver[theIDS [0]][i].theBranchTalkID +" ------ ");
		//		}

		//字典的每一个分支内部排序
		for (int i = 0; i < theIDS.Count; i++)
		{
			theBranch = theStartItemSaver[theIDS[i]];//获得这个分支的所有item用来排序
			//sort(theBranch);
			quickSort(theBranch, 0,theBranch.Count - 1);

			//测试用的输出方法
			//string show = "";
			//for (int j = 0; j < theBranch.Count; j++)
			//	show  += theBranch[j].theBranchTalkID+"  ";
			//print (show);
		}
	}

	//小的排序方法
	//最从小到大排序的
	void sort(List <thePlotItem> theP)
	{
		for (int i = 0; i < theP.Count; i++) 
		{
			for(int j = i;j< theP .Count ; j++)
			{
				if(theP[i].theBranchTalkID > theP [j].theBranchTalkID)
				{
					thePlotItem temp = theP[j];
					theP [j] = theP [i];
					theP [i] = temp;
				}

			}
		}
		//输出检查排序是否完成
		//for (int i = 0; i < theP.Count; i++)
		//	print (theP[i] +"  ");
	}

	void quickSort(List <thePlotItem> theP ,int low,int high)
	{
		if(low>= high)
			return;
		
		int first = low;
		int last = high;
		thePlotItem keyValue = theP[low];
		while(low<high)
		{
			while(low<high && theP [high].theBranchTalkID >=  keyValue.theBranchTalkID)
				high--;
			theP [low] = theP [high];
			while(low<high && theP [low].theBranchTalkID <= keyValue.theBranchTalkID)
				low++;
			theP [high] = theP [low];
		}
		theP [low] = keyValue;
		quickSort(theP , first, low - 1);
		quickSort(theP , low + 1, last); 
	}

	//建立游戏物体的父子关系
	//单个分支之间的父子关系
	void setParentsForBranchPrivate()
	{
		 
		//字典的每一个分支内部排序
		for (int i = 0; i < theIDS.Count; i++)
		{
			List<thePlotItem> theBranch  =  theStartItemSaver[theIDS[i]];
			for(int j = 0 ; j< theBranch .Count-1  ;j++)
			{
				//print (theBranch [j].name + " vs p");
				//print (theBranch [j].gameObject.name);
				theBranch [j+1].gameObject.transform.SetParent (theBranch [j].gameObject.transform);
			}
		}
	}


	//建立各个分支之间的父子关系
	//如果有分支的话，每一个分支都是都是在结尾的时候才会真的进行分叉活动
	//排布方式为第一层为1，固有的
	//第二层1-9
	//第三层1-9
	void setBranchParent()
	{		 
		//第一步，寻找root那个100项是根，不可撼动
		List<thePlotItem> theRoot  = new List<thePlotItem> ();
		List<  List<thePlotItem> > theSecondLayer = new List<List<thePlotItem>> ();
		List<  List<thePlotItem> > theThirdLayer = new List<List<thePlotItem>> ();
		List<  int > theSecondLayerID = new List<int> ();
		List<  int > theThirdLayerID = new List<int> ();
		//字典的每一个分支内部排序
		for (int i = 0; i < theIDS.Count; i++) 
		{
			int A = theIDS [i] / 100;
			int B = theIDS [i] % 100 - theIDS [i] % 10;
			int c = theIDS [i] % 10;
			//这个剧本文件的总root
			if (A >= 1 && B == 0 && c == 0) 
			{
				theRoot = theStartItemSaver [theIDS [i]];
			}
			//这个剧本文件的第二层root
			else if (A >= 1 && B != 0 && c ==0) 
			{
				theSecondLayer.Add( theStartItemSaver [theIDS [i]]);
				theSecondLayerID.Add (B);//保留十位数字
			} 
			//余下的都是叶子节点
			else 
			{
				theThirdLayer.Add( theStartItemSaver [theIDS [i]]);
				theThirdLayerID.Add (B);
			}
		}

		//第二层的所有的头结点都是root的末尾的子节点
		thePlotItem theEndOfRoot = theRoot [theRoot .Count -1];
		theRootTranform = theRoot [0].transform;//保留树根引用
		//print (theEndOfRoot.name + " ++++++");
		//区分出层之后，开始建立父子关系
		for (int i = 0; i < theSecondLayer.Count; i++) 
		{
			//print (theSecondLayer [i] [0].name +"-----");
			theSecondLayer [i] [0].transform.SetParent (theEndOfRoot.transform );
		}
		//叶子结点需要嵌套循环了
		for (int i = 0; i < theThirdLayer.Count; i++)
		{
			for (int j = 0; j < theSecondLayer.Count; j++) 
			{
				//十位数相同，父子关系已经确定了	
				if (theThirdLayerID [i] == theSecondLayerID [j]) 
				{
					//第二层的所有的头结点都是root的末尾的子节点
					thePlotItem theEndOfRootE = theSecondLayer [j][theSecondLayer [j].Count - 1];
					theThirdLayer [i] [0].transform.SetParent (theEndOfRootE.transform);
				}
			}
		}

	}
}
