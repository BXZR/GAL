using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class systemInformations : MonoBehaviour {

	public  static bool loadMemory = false;//是否加载存档
	public static int indexForLoad = -99;//存档的编号
	public static bool canControll = true;//在渐入转场的时候鼠标是不可以有效果的
	//think 是旁白，不显示人名的时候用
	private static string [] proName = {"alice","fione","jik","Caim" ,"mert","fione", "aozi","noOne" , "get"} ;
	private static string [] showName = {"艾莉斯","菲奥奈","吉克" , "凯伊姆" ,"梅尔特", "菲奥奈", "奥兹","" , "戈尔"};
	//好感度系统的好感度百分比
	//从左到右分别是艾莉斯，菲奥奈，吉克
	//也就是说只有这三个任务是可以被攻略的
	//好感度100%可以考虑弄一个H彩蛋
	//好感度是需要跟随存档的，因为不知道什么选择就修改好感度了
	//每一个人都有默认的初始好感度
	//需要注意与下面flash的数值保持同步
	public static float [] lovePercent = { 0.30f,0.25f,0.15f};

	//剧本完成度
	public static float  plotOverPercent = 0;
	//剧本完成数量(单线剧本+1 ， 多线剧本需要除以分支数量 )
	//剧本总共数量，这个还是人为设定吧，花多次I/O的时间做这件事情有一点不值得
	private  static float  plotOverAll = 1;//这需要一个准确的数字
	//为了防止因为没有顺序执行在造成除零异常，在这里初始值为1，多一点就多一点

	//加载所有剧本数行数的方法（开销是个问题，在犹豫到底用不用）
	public static void makeOlotOverAllCount()
	{
		Object[] plots = Resources.LoadAll("thePlots" , typeof(TextAsset));
		foreach (TextAsset thePlot in plots) 
		{
			string[] theInformation = ((TextAsset)thePlot).text.Split ('\n');
			for (int j = 0; j < theInformation.Length; j++) 
			{
				if (theInformation [j].StartsWith ("//") || string.IsNullOrEmpty (theInformation [j]))
					continue;
				plotOverAll++;//每一行一个剧本剧情
			}
		}
		print ("thePlotAllCount = "+ plotOverAll);
	}

	//累加剧本完成度的方法
	public static void addPlotOverPercent(thePlotItem theItem)
	{
		plotOverPercent +=  1f/(theItem.getChilds ().Count * plotOverAll);
		if (plotOverPercent > 1f)
			plotOverPercent = 1f;
	}


	public static string getShowNameWithProName(string pro)
	{
		for (int i = 0; i < proName.Length; i++)
		{
			if (proName [i].Equals (pro))
				return showName [i];
		}
		return "???";
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
	//重置快进标记
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
		plotOverPercent = 0;
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

	//----------------------已经阅读的内容记录---------------------------//
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
	//当前进入到的死亡到场剧本剧情下标
	//同时也是是否进入到了死亡道场的标记
	public static int deadPlotIndex = -99; 

}
