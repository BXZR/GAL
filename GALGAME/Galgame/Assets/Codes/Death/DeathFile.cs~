using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DeathFile : MonoBehaviour {
	//这个类存储死亡场景的记录
	private static List<string> theDeadIndex;//死亡剧本帧下标
	public static List<string> theDeadPlayIndex;//死亡剧本帧下标
	public static List<bool> theDeadOpen;//是不是已经激活这个道场
	public static List<string> theDeathName;//死亡名称
	public static bool isStarted = false;//开启标记

	//初始化
	public static  void makeAllStart()
	{
		//print ("start for death file");
		string theDeathFilePath =  Application .persistentDataPath+"/Death.txt";
		//print (theCGFilePath );
		//没有文件就创建文件
		if (!File.Exists (theDeathFilePath))
		{
			makeInitValues ();
			string information = getSaveString ();
			CreateFile (information, theDeathFilePath);
		}
		//有文件就加载
		else
		{
			readFromFile ();
		}
		isStarted = true;
	}

	//每一个剧本帧检查是不是死了，死了就激活相关的死亡剧本
	public static bool  setDead(string indexIn)
	{

		if (isStarted == false)
			makeAllStart ();//没做初始化就做一次，反正也就只能做一次

		bool isfind = false;

		for (int i = 0; i < theDeadIndex.Count; i++) 
		{
			if (indexIn == theDeadIndex [i]) 
			{
				systemInformations.deadPlotIndex = Convert.ToInt32( theDeadPlayIndex[i]);
				theDeadOpen [i] = true;
				isfind = true;

			}
		}
		//激活死亡就保存一次
		if (isfind)
		{
			string theDeadFilePath =  Application .persistentDataPath+"/Death.txt";
			string information = getSaveString ();
			CreateFile (information, theDeadFilePath);
		}
		//因为要跳转场景，所以还是应该做一点安全措施
		if (systemInformations.deadPlotIndex <= 0)
			return false;

		return isfind;//如果死了就需要进入道场，返回一个
	}



	//从文件获得初始化信息
	private static void readFromFile()
	{
		//刷新存储空间
		theDeadIndex = new List<string> ();
		theDeadOpen = new List<bool> ();
		theDeadPlayIndex = new List<string> ();
		theDeathName = new List<string> ();
		//读取数据
		string theDeadFilePath =  Application .persistentDataPath+"/Death.txt";
		string informationRead = "";
		//读取数据
		FileStream aFile = new FileStream (theDeadFilePath, FileMode.OpenOrCreate);
		StreamReader sw = new StreamReader (aFile);
		informationRead = sw.ReadToEnd ();
		sw.Close ();
		sw.Dispose ();
		//分析数据
		string  [] sp = informationRead.Split(';');
		for (int i = 0; i < sp.Length; i++) 
		{
			string[] sp2 = sp [i].Split (',');
			bool open = Convert.ToBoolean (sp2 [2]);
			theDeadIndex.Add (sp2 [0]);
			theDeadPlayIndex.Add (sp2[1]);
			theDeathName.Add (sp2[3]);
			theDeadOpen.Add (open);
		}

	}

	//初始化的工作实际上是在这里执行的，静态的，写死的
	private static void makeInitValues()
	{
		theDeadIndex = new List<string> ();
		theDeadOpen = new List<bool> ();
		theDeadPlayIndex = new List<string> ();
		theDeathName = new List<string> ();
		//第一个
		theDeadIndex.Add ("120008");
		theDeadOpen.Add (false);//默认未激活
		theDeadPlayIndex.Add("1000000");
		theDeathName.Add("夜战黑羽");
		//第二个
		//theDeadIndex.Add ("300008");
		//theDeadOpen.Add (false);//默认未激活
		//theDeadPlayIndex.Add("1100000");
	}


	//保存文件
	//同样地，小项目用","分割，大项目用";"分割
	private static string getSaveString()
	{
		string saveString = "";
		for (int i = 0; i < theDeadIndex.Count; i++) 
		{
			saveString += theDeadIndex[i] + ","+theDeadPlayIndex [i].ToString()+"," + theDeadOpen[i].ToString () +","+theDeathName[i];
			if(i< theDeadIndex.Count -1)
				saveString +=";";
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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
