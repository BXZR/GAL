using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class extraHFile : MonoBehaviour {

	//第一次好感度超过100%就会永久激活H特典
	//获得文件中的好感度
	//每一个人的H都是作为特典存在的，并且只会有一次
    //其实构造很简单，在一个文件里面存bool数值就可以

	//-----------------------------------有关特典-----------------------------------------------//
	//好感度满了就可以激活特典，这个就是标记
	public static bool  [] loveOpend = { false ,false ,false };
	//特典按钮的背景图片
	public static string  [] PlotName   = { "医者入世","侠侣传说","激情四射"};
	//特典按钮的背景图片
	public static string  [] buttonPictureName  = { "Extra/ExtraAll_1","Extra/ExtraAll_2","Extra/ExtraAll_3"};
	//特典剧本ID
	public static int  [] HPlotID = { 2000000,2100000,2200000};

	//加载特典标记
	public static  void loadHExtra()
	{
		//读取数据
		string theExtraFilePath = Application.persistentDataPath + "/Extra.txt";
		string informationRead = "";
		//读取数据
		FileStream aFile = new FileStream ( theExtraFilePath , FileMode.OpenOrCreate);
		StreamReader sw = new StreamReader (aFile);
		informationRead = sw.ReadToEnd ();
		sw.Close ();
		sw.Dispose ();
		//分析数据
		string [] theSplited = informationRead.Split(';');
		for (int i = 0; i < theSplited.Length; i++) 
		{
			loveOpend [i]= Convert.ToBoolean (theSplited[i]);
		}
	}

	//保存特典标记
	public static void saveHExtra()
	{
		string information = "";
		for (int i = 0; i < loveOpend.Length; i++) 
		{
			information += loveOpend [i];
			if(i < loveOpend.Length -1)
				information+= ";";
		}
		string theExtraFilePath = Application.persistentDataPath + "/Extra.txt";
		CreateFile(information , theExtraFilePath);
	}

	//初始化
	public static void makeAllStart()
	{
		string theExtraFilePath = Application.persistentDataPath + "/Extra.txt";
		if (File.Exists (theExtraFilePath))
			loadHExtra ();
		else
			saveHExtra ();
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
