using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

//这个枚举保存每一个配置文件的项目，外部调用的时候使用枚举，不用字符串
//这样看上去安全一点
using System.IO;
using System;

public enum ConfigItems { Font,BGMVolume,SouneVolume,SpeakSoundVolume,AutoTextSpeed,TextSpeed , TextBack}
public class configController : MonoBehaviour  {

	//系统设置的保存和重新配置管理单元

    /*
     * 这段代码在PC上是可以用的但是在安卓端会不可用于是决定用IO来做兼容一下
	[System.Runtime.InteropServices.DllImport("kernel32")]
	private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
	[System.Runtime.InteropServices.DllImport("kernel32")]
	private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);

	private string theIniPath = "";
	//分而治之，每一个相应的模块读取自己部分的item返回，在初始化的时候做一次改变
	//这个类仅仅提供方法，但是不提供集中控制
	//每一个受到控制文件约束的模块有自己的判断逻辑
	//游戏第一次的时候如果没有配置文件就创建一个
	public static  void createConfigFileIfNull()
	{
		
		string theIniPath =  Application .persistentDataPath+"/GalConfig.ini";
		print (theIniPath);
		if (File.Exists (theIniPath) == false) 
		{
			//配置文件的所有项目的值都是数字，方便整合
			WritePrivateProfileString ("Config", ConfigItems.Font.ToString (), "1", theIniPath);
			WritePrivateProfileString ("Config", ConfigItems.BGMVolume.ToString (), "1", theIniPath);
			WritePrivateProfileString ("Config", ConfigItems.SouneVolume.ToString (), "1", theIniPath);
			WritePrivateProfileString ("Config", ConfigItems.SpeakSoundVolume.ToString (), "1", theIniPath);
			WritePrivateProfileString ("Config", ConfigItems.AutoTextSpeed.ToString (), "0", theIniPath);
			WritePrivateProfileString ("Config", ConfigItems.TextSpeed.ToString (), "0", theIniPath);
		}
	}




	//检查配置文件是否存在
	public static string chekcConfigFile()
	{
		string theIniPath =  Application .persistentDataPath+"/GalConfig.ini";
		if (File.Exists (theIniPath))
			return  "配置文件存在";
		return  "配置文件不存在";
	}
	//根据key返回配置文件的相关item做判断
	public static float getConfigItem(ConfigItems item)
	{
		string key = item.ToString ();
		string theIniPath =  Application .persistentDataPath+"/GalConfig.ini";
		if (File.Exists (theIniPath)) 
		{
			StringBuilder informationGet = new StringBuilder (255);
			GetPrivateProfileString ("Config", key, "", informationGet, 255, theIniPath);
			return (float )Convert.ToDouble( informationGet.ToString ());
		}
		return 1f;
	}

	//存入相关配置信息
	public static void setConfigItem(ConfigItems itemKey , string item)
	{
		string key = itemKey.ToString ();
		string theIniPath =  Application .persistentDataPath+"/GalConfig.ini";
		if (File.Exists (theIniPath))
		{
			WritePrivateProfileString ("Config", key, item, theIniPath);
		}
	}

	void demo()
	{
		StringBuilder demos = new StringBuilder (255);
		WritePrivateProfileString ("section", "name", "theDemo", @"D:\theDemoini.ini");
		GetPrivateProfileString ("section" , "name" ,"" ,demos,255 , @"D:\theDemoini.ini");
		print (demos);
	}

	*/

	//保存config的字典
	private static List<string> configKey = new List<string>();
	private static List<string> configValue = new List<string>();
	//配置文件转字符串
	private static string configsToString ()
	{
		//加入默认的配置
		//文本格式： 大项目之间用;分割，小项目之间用,分割
		//之所以不用\n是因为有一些文件对\n的支持不是很好
		string information = "";
		for(int i= 0 ; i< configKey.Count ; i++)
		{
			information += configKey [i] + "," + configValue [i];
			if (i < configKey.Count - 1)
				information += ";";
				
		}
		return information;
	}

	//配置文件初始化 
	//同时也是初始文件设定
	//如果需要增加config就在这里增加条目
	private static  void  makeConfig()
	{
		//配置文件的所有项目的值都是数字，方便整合
		configKey .Add (ConfigItems.Font.ToString () );
		configValue.Add ("1");
		configKey .Add (ConfigItems.BGMVolume.ToString () );
		configValue.Add ("1");
		configKey .Add (ConfigItems.SouneVolume.ToString () );
		configValue.Add ("1");
		configKey .Add (ConfigItems.SpeakSoundVolume.ToString ());
		configValue.Add ("1");
		configKey .Add (ConfigItems.AutoTextSpeed.ToString () );
		configValue.Add ("0");
		configKey .Add (ConfigItems.TextSpeed.ToString ());
		configValue.Add ("0");
		configKey .Add (ConfigItems.TextBack.ToString ());
		configValue.Add ("0");
	}

	public static  void createConfigFileIfNull()
	{
		
		string theIniPath =  Application .persistentDataPath+"/GalConfig.txt";
		//print (theIniPath);
		if (File.Exists (theIniPath) == false) 
		{
			makeConfig ();
			string informationSave = configsToString ();
			FileStream aFile = new FileStream(theIniPath , FileMode.OpenOrCreate);
			StreamWriter sw = new StreamWriter(aFile);
			sw.Write(informationSave);
			sw.Close();
			sw.Dispose();

		}
	}
		
	//检查配置文件是否存在
	public static string chekcConfigFile()
	{
		string theIniPath =  Application .persistentDataPath+"/GalConfig.txt";
		if (File.Exists (theIniPath))
			return  "已读取配置文件";
		return  "配置文件不存在";
	}

	private static bool isLoadedFromFile = false;
	//为了防止IO冲突，其实整体从文件读取配置信息只有一次
	//读取到的配置信息读取到两个list中访问
	public static  void loadConfig()
	{
		if (isLoadedFromFile == false) 
		{
			isLoadedFromFile = true;

			configKey = new List<string> ();
			configValue = new List<string> ();
			string theIniPath =  Application .persistentDataPath+"/GalConfig.txt";
			if (File.Exists (theIniPath)) 
			{
				string informationRead = "";
				//读取数据
				FileStream aFile = new FileStream (theIniPath, FileMode.OpenOrCreate);
				StreamReader sw = new StreamReader (aFile);
				informationRead = sw.ReadToEnd ();
				sw.Close ();
				sw.Dispose ();
				//分析数据
				string searchedItem = "";
				string[] items = informationRead.Split (';');
				for (int i = 0; i < items.Length; i++) {
					string[] keyValue = items [i].Split (',');
					configKey.Add (keyValue [0]);
					configValue.Add (keyValue [1]);
				}
			}
			else
			{
				//没有配置文件就新建一个
				createConfigFileIfNull ();
			}
		}
	}


	//根据key返回配置文件的相关item做判断
	public static float getConfigItem(ConfigItems item)
	{
		loadConfig ();
		string key = item.ToString ();
		string getString = "";
		for (int i = 0; i < configKey.Count; i++)
		{
			if (configKey [i] == key)
			{
				getString = configValue [i];
			}
		}

		if (string.IsNullOrEmpty (getString) == false)
			return (float)Convert.ToDouble (getString);
		
		return 1f;
	}

	//存入相关配置信息
	public static void setConfigItem(ConfigItems itemKey , string item)
	{
		string key = itemKey.ToString ();
		string theIniPath =  Application .persistentDataPath+"/GalConfig.txt";
		if (!File.Exists (theIniPath))
		{
			//没有配置文件就新建一个
			createConfigFileIfNull ();
		}
		int index = 0;
		for (int i = 0; i < configKey.Count; i++) 
		{
			if (configKey [i] == key) 
			{
				index = i;
				break;
			}
		}
		configValue [index] = item;

		//不推荐动态flash，IO操作爆炸，尤其是slider
		//flashConfig ();
	}

	private static  bool isFlashing = false;
	public static  void flashConfigPrivate()
	{
		//加入了gate所以其实即使被多次调用也仅仅是调用了一次而已
		if (isFlashing == false) 
		{
			isFlashing = true;
			string theIniPath = Application.persistentDataPath + "/GalConfig.txt";
			string informationSave = configsToString ();
			FileStream aFile = new FileStream (theIniPath, FileMode.Create);
			StreamWriter sw = new StreamWriter (aFile);
			sw.Write (informationSave);
			sw.Close ();
			sw.Dispose ();
			isFlashing = false;
			//print ("ERE");
		}
	}

//----------------------------------------------------------------------------------------------------------//
	//下面是真正自身调用的方法
	public GameObject Configontainer;//，包含所有被配置文件控制的UI等的容器SystemPanel_2
	//初始化调用集合
	void Awake ()
	{  
		loadConfig ();
	}

	//用来统一外部调用的方法（来自于接口抽象）
	public void saveToConfigC ()
	{
		configUser[] theConfigUsers = Configontainer.GetComponentsInChildren<configUser> ();
		foreach (configUser J in theConfigUsers)
			J.saveToConfig ();
		//从内存写入到文件中
		flashConfigPrivate ();
		informationPanel.showInformation ("系统设置保存成功");
	}

	public void  loadFromConfigC ()
	{
		configUser[] theConfigUsers = Configontainer.GetComponentsInChildren<configUser> ();
		//print ("theConfigUsers = "+theConfigUsers.Length);
		foreach (configUser J in theConfigUsers)
			J.loadFromConfig ();
		informationPanel.showInformation ("系统设置加载成功");
	}

 

	//注意，这里save是在内存中的保存，而flash是在文件中的保存
	//flash调用的次数一定要少

	//void Update ()
	//{
		//if (Input.GetKeyDown (KeyCode.Return))
		//	createConfigFileIfNull ();
	//}

		
}
