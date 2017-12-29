using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class systemInformations : MonoBehaviour {

	//当前进入到的死亡到场剧本剧情下标
	//同时也是是否进入到了死亡道场的标记
	public static int deadPlotIndex = -99; 

	public  static bool loadMemory = false;//是否加载存档
	public static int indexForLoad = -99;//存档的编号
	public static bool canControll = true;//在渐入转场的时候鼠标是不可以有效果的
	public static int showExtraEffectsForAnimation = 1;//舞台效果需要不需要 0 不需要 1 需要
	public static int readTextColorChange = 1;//已读文本颜色是否会变化 0 不需要 1 需要
	public static Color getTextColor (int plotItemID)//返回文本框的颜色
	{
		if (readTextColorChange == 0)
			return Color.white;

		return plotIDRead.Contains (plotItemID) ? Color.yellow : Color.white;
	}
	//think 是旁白，不显示人名的时候用
	private static string [] proName = { "alice" ,"fione" ,"jik"  , "Caim"   ,"mert",   "aozi", "noOne" , "get" , "darkPlume" ,"NPC"} ;
	private static string [] showName = {"艾莉斯","菲奥奈","吉克" , "凯伊姆" ,"梅尔特", "奥兹",  ""     , "戈尔" , "黑羽" , "路人"};
	private static Dictionary <string , string > playerNames = null;
	//使用数组顺序查找似乎没有字典快
	public static void makePlayerNamesStartIfNull()
	{
		if (playerNames == null) 
		{
			playerNames = new Dictionary<string, string> ();
			for (int i = 0; i < proName.Length; i++)
			{
				playerNames.Add (proName[i] , showName[i]);
			}
		}
	}

	//好感度系统的好感度百分比
	//从左到右分别是艾莉斯，菲奥奈，吉克
	//也就是说只有这三个任务是可以被攻略的
	//好感度100%可以考虑弄一个H彩蛋
	//好感度是需要跟随存档的，因为不知道什么选择就修改好感度了
	//每一个人都有默认的初始好感度
	//需要注意与下面flash的数值保持同步
	public static float [] lovePercent = { 0.30f,0.25f,0.15f};
	//剧本总共数量，这个还是人为设定吧，花多次I/O的时间做这件事情有一点不值得
	private  static float  plotOverAll = 0;//这需要一个准确的数字
	//为了防止因为没有顺序执行在造成除零异常，在这里初始值为1，多一点就多一点
	//用这个List来保存所有已经读过的plotItem的ID，用List的count来记录剧本完成数量
	//这个List会被保存到一个特殊的文件，所有存档共有
	private static List<int> plotIDRead = new List<int> ();

	//主音频控制单元，这个控制单元将从start场景开始不消失，所有的音频管理懂从这里开始
	//注意这里必须判断是否为空，如果为空，就是用本场景中自己的音频控制单元
	public static MusicController theMainMusicController = null;

	//加载所有剧本数行数的方法（开销是个问题，在犹豫到底用不用）
	public static void makeOlotOverAllCount()
	{
		Object[] plots = Resources.LoadAll("thePlots" , typeof(TextAsset));
		foreach (TextAsset thePlot in plots) 
		{
			string[] theInformation = ((TextAsset)thePlot).text.Split ('\n');
			for (int j = 0; j < theInformation.Length; j++) 
			{
				string checkString = theInformation [j].Trim ();
				if (checkString.StartsWith ("//") || string .IsNullOrEmpty (checkString))
					continue;
				
				plotOverAll++;//每一行一个剧本剧情
		     //测试输出全剧本长度
		     //print ("thePlotAllCount = "+ plotOverAll);
			}
		}
	}

	//累加剧本完成度的方法
	public static void addPlotOverPercent(thePlotItem theItem)
	{
		if (!plotIDRead.Contains (theItem.ThePlotItemID))
			plotIDRead.Add (theItem.ThePlotItemID);
	}
	public static float getPlotOverPercent()
	{
		return plotIDRead.Count / plotOverAll;
	}

	//需要找一个特殊的文件保存已经读过的所有的ID
	public static void SaveTheOverPlot()
	{
		string path = Application.persistentDataPath + "/PlotOver.txt";
		//print ("save to "+ path);
		string informationSave = "";
		for (int i = 0; i < plotIDRead.Count; i++) 
		{
			informationSave  += plotIDRead[i];
			if(i < plotIDRead.Count -1)
				informationSave += "\n";
		}
		CreateFile(informationSave , path);

	}

	//加载已读plotItem的方法
	public static void LoadPlotOver()
	{
		//print ("load over plot");
		string FilePath =  Application .persistentDataPath+"/PlotOver.txt";
		if (File.Exists (FilePath) == false)
		{
			plotIDRead = new List<int> ();
		}
		else 
		{
			string informationRead = "";
			//读取数据
		    FileStream aFile = new FileStream (FilePath , FileMode.OpenOrCreate);
			StreamReader sw = new StreamReader (aFile);
			informationRead = sw.ReadToEnd ();
			sw.Close ();
			sw.Dispose ();
			//分析数据
			plotIDRead = new List<int> ();
			string [] reads = informationRead.Split('\n');
			for (int i = 0; i < reads.Length; i++) 
			{
				plotIDRead.Add (System.Convert.ToInt32(reads[i]));
			}
		}
	}

	public static string getShowNameWithProName(string pro)
	{
		//这种方法是顺序查找，或许可以用字典哈希查找来提速
		//但是这种方法予以保留
		//for (int i = 0; i < proName.Length; i++)
		//{
		//	if (proName [i].Equals (pro))
		//		return showName [i];
		//}
		//return "???";

		//看上去更快一点的字典查找
		makePlayerNamesStartIfNull();
		return playerNames[pro];
	}

	//----------------------system panel的相关记录逻辑---------------------------//
	public static  bool isAutoWait = true;//剧本到时间自动跳转
	public static bool isAutoNowSave = true; //是否自动跳转的副本
	public static bool isThePanelShows = false;//存档面板是不是显示了
	public static bool isChildPanelShows = false;//有一个字panel用来存储一些不是很常用的按钮
	public static bool isSaving = true;//true表示存档，false表示读档（面板相同，用这个标记来区分功能）

	//----------------------快进控制--------------------------//
	//控制时间速度
	private static float speedModeTimeScale= 17.0f;
	private static float normalModeTimeScale = 1.0f;

	private static  bool isSkiping = false;//是否在进行快进
	public static bool ISSkiping { get {return isSkiping;} }

	public static string theBackMusicNameNow = "并没有音乐";//为了防止额外的多次加载，做一个小小的判定

	//重置快进标记-----------------------------------------------------------------
	public static void flash()
	{
		isAutoWait = true;//剧本到时间自动跳转
		isThePanelShows = false;//存档面板是不是显示了
		isChildPanelShows = false;//有一个字panel用来存储一些不是很常用的按钮
		isSaving = true;//true表示存档，false表示读档（面板相同，用这个标记来区分功能）
		canControll = true;
		Time.timeScale = normalModeTimeScale;
		isSkiping = false;
		deadPlotIndex = -99;
		theBackMusicNameNow = "并没有音乐";
		lovePercent = new  float []  { 0.30f,0.25f,0.15f};
	}
	//全局唯一的控制是否快进的方法
	public  static void skipControll()
	{
		if (isSkiping == false) 
		{
			isSkiping = true;
			Time.timeScale = speedModeTimeScale;
		}
		else 
		{
			isSkiping = false;
			Time.timeScale = normalModeTimeScale;
		}
	}

	//每一次按照说话的长度判断是持续时间(这个数值要比textController的theFlashWaitTime要长才可以)
	public static float plotTimeWait = 0.35f;//剧本中每一个字的时间长度
	//将全局变量赋值成初始值
	public static float textShowTime = 0.2f;//文本显示的时候每一个字的弹出速度

	//----------------------已经阅读的内容最近几条内容的记录---------------------------//
	public  static Queue<string> theReadText = new Queue<string> ();
	public  static Queue<string> theReadTextSpeak = new Queue<string> ();
	private static  int theReadLength = 25;//最多记录25条最新的已读内容
	//记录最新一条已经读到的内容
	public static void saveRead (string information , string speakName = "")
	{
		//如果数据量不多，直接插入就好
		if (theReadText.Count < theReadLength) 
		{
			theReadText.Enqueue(information );
			theReadTextSpeak.Enqueue(speakName);
			//print ("TheCountNow = " + theReadText.Count );
		}
		else
		{
			//print("Delete : " + theReadText.Dequeue());
			theReadText.Dequeue ();
			theReadText.Enqueue(information);

			theReadTextSpeak.Dequeue ();
			theReadTextSpeak.Enqueue(speakName);
			//print ("TheCountNow = " + theReadText.Count );

			//print ("---------------------");
			//print (getReadText());
			//print ("---------------------");
		}
	}


	//有关场景回放
	public static int SceneStartIndex = -1;
	public static int SceneEndIndex = -1;
	public static  bool isScene = false;//是否正在回忆模式
	//这个标记在每一次开始场景中会重置为false
	public static void makeFlashForScene()
	{
		isScene = false;
		SceneStartIndex = -1;
		SceneEndIndex = -1;
	}
	public static void makeScene(int start , int end)
	{
		SceneStartIndex = start;
		SceneEndIndex = end;
		isScene = true;
	}


	//文档读写问题还是应该一个雷一个，这块的代码暂时先不做重复
	//因为不同的文件和类又可能有不同的逻辑和处理
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

	public static Sprite makeLoadSprite(string textureName)
	{
		//textureName = "people/noOne";
		Texture2D theTextureIn = Resources.Load <Texture2D> (textureName);
		Sprite theSprite =  Sprite .Create(theTextureIn,new Rect (0,0,theTextureIn.width,theTextureIn.height),new Vector2 (0,0));
		theSprite.name = textureName;
		return theSprite;
	}


}
