using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CGModeFile : MonoBehaviour {

	//CG 控制中文件关联的静态方法集合类别
	//因为渗透面比较广，而且好牵扯到文件，所以暂时决定使用静态方法

	//这里没办法了，暂时险写死
	//因为在初始界面就需要生成这个CG文件而这个时候不可能用面板
	private static string[] CGNameForGroup1= 
	{"AliceCG1_Button" , "AliceCG2_Button" , "AliceCG3_Button" , "AliceCG4_Button" , "AliceCG5_Button" , "AliceCG6_Button" , "AliceCG7_Button"  };//艾丽斯
	private static string[] CGNameForGroup2 =
	{"FioneCG1_Button" ,"FioneCG2_Button" ,"FioneCG3_Button" ,"FioneCG4_Button" ,"FioneCG5_Button" ,"FioneCG6_Button" ,"FioneCG7_Button"  };//菲奥奈
	private static string[] CGNameForGroup3 = 
	{"alice_Home_Dark_Button" , "bar_Button" , "home_Button"  , "jik_home_Button" , "killScene_Button"  , "street_Button" , "whole_Button" , "DeadChart_Button"};//其他


	//----------------------------------------只有上面内容是需要配置的-----------------------------------------------------------------------------//
	//保存所有CG的对象的一个队列
	//小队队列是从这列被抽出来的
	public static List <CGSaves> theCG1;
	public static List<CGSaves> theCG2;
	public static List<CGSaves> theCG3;
	//打个标记，有些时候不按照顺序来进行游戏可能会有初始化的问题
	private static bool isStarted = false;

	//如果没有配置文件就需要生成一个配置文件
	//这就意味着所有的初始值都在这里被存储
	//这里有一位这有超级不好的耦合
	public static  void makeAllStart()
	{
		string theCGFilePath =  Application .persistentDataPath+"/CG.txt";
		//print (theCGFilePath );
		//没有文件就创建文件
		if (!File.Exists (theCGFilePath))
		{
			getStaticValues ();
			string information = getSaveStringForString ();
			CreateFile (information, theCGFilePath);
		}
		//有文件就加载
		else
		{
			readFromFile ();
		}
	}

	public static bool checkIsLocked (string CGName)
	{
		bool get = false;

		for (int i = 0; i < theCG1.Count; i++) 
		{

			if (theCG1 [i].CGFileName == CGName) 
			{
				get  = theCG1 [i].isGet;
				break;
			}
		}
		for (int i = 0; i < theCG2.Count; i++) 
		{
			if (theCG2 [i].CGFileName == CGName) 
			{
				get  = theCG2 [i].isGet;
				break;
			}
		}
		for (int i = 0; i < theCG3.Count; i++) 
		{
			
			if (theCG3 [i].CGFileName == CGName) 
			{
				get  = theCG3 [i].isGet;
				break;
			}
		}
		//print (CGName + "--" + get);
		return get;
	}

	/*
	 *  用这种方法来激活CG
		CGModeFile.CGActive("AliceCG1_Button");
		CGModeFile.CGActive("AliceCG2_Button");
		CGModeFile.CGActive("AliceCG3_Button");
		CGModeFile.CGActive("AliceCG4_Button");
		CGModeFile.CGActive("AliceCG5_Button");
		CGModeFile.CGActive("AliceCG6_Button");
		CGModeFile.CGActive("AliceCG7_Button");


		CGModeFile.CGActive("FioneCG1_Button");
		CGModeFile.CGActive("FioneCG2_Button");
		CGModeFile.CGActive("FioneCG3_Button");
		CGModeFile.CGActive("FioneCG4_Button");
		CGModeFile.CGActive("FioneCG5_Button");
		CGModeFile.CGActive("FioneCG6_Button");
		CGModeFile.CGActive("FioneCG7_Button");

		CGModeFile.CGActive("alice_Home_Dark_Button");
		CGModeFile.CGActive("bar_Button");
		CGModeFile.CGActive("home_Button");
		CGModeFile.CGActive("jik_home_Button");
		CGModeFile.CGActive("killScene_Button");
		CGModeFile.CGActive("street_Button");
		CGModeFile.CGActive("whole_Button");
       */

	public static void CGActive(string CGName)
	{
		if(isStarted == false)
		makeAllStart ();//防止出现没有初始化的问题


		bool isfound = false;

		for (int i = 0; i < theCG1.Count; i++) 
		{
			if (theCG1 [i].CGFileName == CGName) 
			{
				theCG1 [i].isGet = true;
				isfound = true;
				break;
			}
		}
		for (int i = 0; i < theCG2.Count; i++) 
		{
			if (theCG2 [i].CGFileName == CGName) 
			{
				theCG2 [i].isGet = true;
				isfound = true;
				break;
			}
		}
		for (int i = 0; i < theCG3.Count; i++) 
		{
			if (theCG3 [i].CGFileName == CGName) 
			{
				theCG3 [i].isGet = true;
				isfound = true;
				break;
			}
		}

		//重新保存CG文件
		//if (isfound)
		//{
			//print ("CGModeFile ---" + isfound);
			//print("收集到了CG");
		//}

	}
	//为了避免文件IO的频繁操作，实际上只有每一次存档的时候才会真正地记录CG手机的情况
	//也就是说未保存的游戏的CG是不会被收集到的
	public static void saveCGFile()
	{
		string theCGFilePath = Application.persistentDataPath + "/CG.txt";
		string information = getSaveStringForString ();
		CreateFile (information, theCGFilePath);
	}

	private static void readFromFile()
	{
		//读取数据
		    string theCGFilePath =  Application .persistentDataPath+"/CG.txt";
			string informationRead = "";
			//读取数据
		    FileStream aFile = new FileStream (theCGFilePath, FileMode.OpenOrCreate);
		    StreamReader sw = new StreamReader (aFile);
			informationRead = sw.ReadToEnd ();
			sw.Close ();
			sw.Dispose ();
	  //分析数据
		string [] theSplited = informationRead.Split(';');
		theCG1 = new List<CGSaves> ();
		theCG2 = new List<CGSaves> ();
		theCG3 = new List<CGSaves> ();
		//第一组
		string  [] CP = theSplited[0].Split(',');
		//print (CP.Length +" = CP.length");
		for (int i = 0; i < CP.Length; i+=4)
		{
			theCG1.Add (new CGSaves (CP [i], Convert.ToInt32 (CP [i + 1]), Convert.ToBoolean (CP [i + 2]), Convert.ToInt32 (CP [i + 3])));

		}
		//第二组
		CP = theSplited[1].Split(',');
		for (int i = 0; i < CP.Length; i+=4)
		{
			theCG2.Add (new CGSaves (CP [i], Convert.ToInt32 (CP [i + 1]), Convert.ToBoolean (CP [i + 2]), Convert.ToInt32 (CP [i + 3])));
		}
		//第三组
		CP = theSplited[2].Split(',');
		for (int i = 0; i < CP.Length; i+=4)
		{
			theCG3.Add (new CGSaves (CP [i], Convert.ToInt32 (CP [i + 1]), Convert.ToBoolean (CP [i + 2]), Convert.ToInt32 (CP [i + 3])));
		}

 
	}



	//面板数值转化为静态数值
	private static void getStaticValues()
	{
		//动态转静态并简单处理
		theCG1 = new List<CGSaves> ();
		theCG2 = new List<CGSaves> ();
		theCG3 = new List<CGSaves> ();
		for (int i = 0; i < CGNameForGroup1.Length; i++)
			theCG1.Add (new CGSaves (CGNameForGroup1 [i], 1, false, i));
		for (int i = 0; i < CGNameForGroup2.Length; i++)
			theCG2.Add (new CGSaves (CGNameForGroup2 [i], 2, false, i));
		for (int i = 0; i < CGNameForGroup3.Length; i++)
			theCG3.Add (new CGSaves (CGNameForGroup3 [i], 3, false, i));

		//其实按照上面的做法已经是正好的顺序了
		//在这里也仅仅是为了保靠
		theCG1.Sort((p1, p2) => p1.indexForGroup.CompareTo(p2.indexForGroup));
		theCG2.Sort((p1, p2) => p1.indexForGroup.CompareTo(p2.indexForGroup));
		theCG3.Sort((p1, p2) => p1.indexForGroup.CompareTo(p2.indexForGroup));
	}


	//将所有对象的转成字符串保存
	private static string getSaveStringForString()
	{
		//保存成字符串
		string saveString = "";
		for (int i = 0; i < theCG1.Count; i++)
		{
			saveString += theCG1 [i].ChangeString ();
			if(i<(theCG1.Count -1))
				saveString += ",";
		}
		saveString += ";";
		for (int i = 0; i < theCG2.Count; i++)
		{
			saveString += theCG2 [i].ChangeString ();
			if(i<(theCG2.Count -1))
				saveString += ",";
		}
		saveString += ";";
		for (int i = 0; i < theCG3.Count; i++)
		{
			saveString += theCG3 [i].ChangeString ();
			if(i<(theCG3.Count -1))
				saveString += ",";
		}
		return saveString;
	}




	private  static  void  CreateFile(string information , string path)
	{
			//没有配置文件就新建一个
		 	string informationSave = information;
		    FileStream aFile = new FileStream(path , FileMode.Create);
				StreamWriter sw = new StreamWriter(aFile);
				sw.Write(informationSave);
				sw.Close();
				sw.Dispose();
	}



}

//这个类是一个在这个脚本里使用的辅助类
//建立几个属性之间的逻辑联系并且包装
public class CGSaves
{
	public string CGFileName = "";//CG文件名字
	public int groupIndex = 1;//CG分组
	public bool isGet = false;//是否已经解锁了这个CG
	public int indexForGroup = 0;//组内排序需要这个标记
	public CGSaves(string fileName , int groupIn , bool iGetIn ,int indexIn)
	{
		CGFileName = fileName;
		groupIndex = groupIn;
		isGet = iGetIn;
		indexForGroup = indexIn;
	}
	public string ChangeString()
	{
		return CGFileName + "," + groupIndex + "," + isGet + "," + indexForGroup;
	}
}