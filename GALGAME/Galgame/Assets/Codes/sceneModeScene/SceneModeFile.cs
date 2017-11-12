using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//场景控制模块要比CG控制模块更加复杂一点...
using System.IO;
using System;


public class SceneModeFile : MonoBehaviour {

	 //静态控制scene的管理单元，与CG的有相似之处
	//配置文件的类中存储，因为在最开始的时候是不可能进入到这个场景的，这就有矛盾。
	//现在想到的方法姑且是在内存中静态保存一次，而且只在必要的时候进行又滚稳健的吃后话操作
	public static List<scenes> theSceneList1 ;//直接用list保存
	public static List<scenes> theSceneList2 ;//直接用list保存
	public static List<scenes> theSceneList3 ;//直接用list保存
	//之所以用这样的套路是为了减少一点分配用的计算量

	private static bool isStarted = false;


	public static void activeScene(int endIndex)
	{
		//防止初始化出现问题
		if (isStarted == false)
			InitValues ();
		bool isActiveOne = false;
		//用尾巴标记进行激活
		//有一些不用激活的在初始化的时候直接标记为true就可以了
		for (int i = 0; i < theSceneList1.Count; i++) 
		{
			if (theSceneList1 [i].endIndex == endIndex) 
			{
				isActiveOne = true;
				theSceneList1 [i].isOpened = true;
			}
		}
		for (int i = 0; i < theSceneList2.Count; i++) 
		{
			if (theSceneList2 [i].endIndex == endIndex) 
			{
				isActiveOne = true;
				theSceneList2 [i].isOpened = true;
			}
		}
		for (int i = 0; i < theSceneList3.Count; i++) 
		{
			if (theSceneList3 [i].endIndex == endIndex) 
			{
				isActiveOne = true;
				theSceneList3 [i].isOpened = true;
			}
		}
		//如果有激活就保存到文件里面
		if(isActiveOne)
		saveSceneFile();
	}

	public static void InitValues()
	{
		string theSceneFilePath =  Application .persistentDataPath+"/Scene.txt";
		if (File.Exists (theSceneFilePath) == false) 
		{
			//没有文件就创建文件
			makeInitValues();
			saveSceneFile ();//建立文件
		}
		else
		{
			loadFromFile ();
			//否则就加载文件
		}
		isStarted = true;
	}

	//保存于创建
	//加上了标记防止互斥事件
	private static bool isSaving=false;
	public static void saveSceneFile()
	{
		if (isSaving == false) 
		{
			isSaving = true;
			string path = Application.persistentDataPath + "/Scene.txt";
			string information = getSaveString ();
			//print (" save   " + information);
			CreateFile (information, path);
			isSaving = false;
		}
	}


	//从存档里面加载出来相关内容
	private static  void loadFromFile()
	{
		//读取数据
		string theSceneFilePath =  Application .persistentDataPath+"/Scene.txt";
		print (theSceneFilePath);
		string informationRead = "";
		//读取数据
		FileStream aFile = new FileStream ( theSceneFilePath , FileMode.OpenOrCreate);
		StreamReader sw = new StreamReader (aFile);
		informationRead = sw.ReadToEnd ();
		sw.Close ();
		sw.Dispose ();
		//分析数据
		//同样地，大项目用“;”,小项目用','
		string [] theGroups = informationRead.Split (';');

		theSceneList1 = new List<scenes> ();
		theSceneList2 = new List<scenes> ();
		theSceneList3 = new List<scenes> ();
		//这种写法的功能应该与CGFile模块是一样的
		//功能一样，代码也好看一点，但是做出了额外的计算
		//很难权衡啊，所以分别用了
		for(int item = 0 ; item < theGroups.Length ; item ++)
		{
			string[] SP = theGroups [item].Split (',');
			for (int i = 0; i < SP.Length; i += 5) 
			{
				scenes theScene = new scenes (SP [i], Convert.ToInt32 (SP [i + 1]), Convert.ToInt32 (SP [i + 2]), Convert.ToInt32 (SP [i + 3]), Convert.ToBoolean (SP [i + 4]));
					if(item == 0)
					theSceneList1.Add (theScene);
				    else if(item == 1)
					theSceneList2.Add (theScene);
				   else if(item == 2)
					theSceneList3.Add (theScene);
			}
		}
	}

	//初始化立即数
	private  static void makeInitValues()
	{
		theSceneList1 = new List<scenes> ();
		theSceneList2 = new List<scenes> ();
		theSceneList3 = new List<scenes> ();
		//100012,100036这种编号要与plot剧本文件相对应
		//第一组
		theSceneList1.Add (new scenes("日常邪恶" ,100012,100036 , 1,false));
		theSceneList1.Add (new scenes("夜半医疗" ,200000,200026 , 1,false));
		theSceneList1.Add (new scenes("独白" ,200027,200044 , 1,false));
		//第二组
		theSceneList2.Add (new scenes("能吃是福" ,100077,100091, 2,false));
		theSceneList2.Add (new scenes("新的任务" ,200045,200089, 2,false));

		//第三组
		theSceneList3.Add (new scenes("近朱者赤" ,100046, 100065 , 3,false));
		theSceneList3.Add (new scenes("吉克与梅尔特" ,100110, 100118 , 3,false));

	}
	//生成存储的字符串
	private static  string getSaveString()
	{
		string information = "";
		for (int i = 0; i < theSceneList1.Count; i++) 
		{
			information += theSceneList1 [i].makeString ();
			if (i < theSceneList1.Count - 1)
				information += ",";
		}
		information +=";";
		for (int i = 0; i < theSceneList2.Count; i++) 
		{
			information += theSceneList2 [i].makeString ();
			if (i < theSceneList2.Count - 1)
				information += ",";
		}
		information +=";";
		for (int i = 0; i < theSceneList3.Count; i++) 
		{
			information += theSceneList3 [i].makeString ();
			if (i < theSceneList3.Count - 1)
				information += ",";
		}
		return information;
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


//记录scene信息的类
public class scenes
{
	public int startIndex = 0;
	public int endIndex = 0;
	public int groupIndex = 1;//当然也是要分组的
	public bool isOpened = false;
	public string sceneName = "";

	public scenes (string sceneNameIn ,int startIndexIn , int endIndexIn , int groupIndexIn , bool isOpenIn)
	{
		sceneName = sceneNameIn;
		startIndex = startIndexIn;
		endIndex = endIndexIn;
		groupIndex = groupIndexIn;
		isOpened = isOpenIn;
	}
	public string makeString()
	{
		return sceneName + "," + startIndex + "," + endIndex + "," + groupIndex + "," + isOpened;
	}
}
