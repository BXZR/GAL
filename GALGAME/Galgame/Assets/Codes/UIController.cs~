using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour {

	//这个类专门用来控制说话的时候现实的图和名字

	/*
	 * 文本框的人物图和背景大图某一时刻是唯效果非常复杂，这个需额外的处理
	 * 值得注意的就是整个界面有两个部分组成
	 * 一部分是UI
	 * 还有一部分是纯粹的2D的Sprite的构成的图像
	 * 其中，背景图北京众人物的大图都是sprite
	 * 
	*/

	public GameObject theShowText;//用于显示说话内容的下面的文本框组

	public SpriteRenderer theBackPicture;//背景大图
	public effectSlowIn theForwardPicture;//前置的大图（用来做渐入效果）
	public SpriteRenderer [] bigPeoplePictures;//说话的时候的人物大图

	//public Image theShowPictureInSceen;//用于显示在场景里的大人物图
	public Image theShowPictureInText1;//在文本框显示的图
	public Image theShowPictureInText2;//在文本框显示的图
	//public Text theName;//文本框显示的名字

	private string theBackName = "";//保留的背景图的引用，因为背景图不用每一次都更新
	private string thePeopleSmallPictureName = "people/noOne";//说话的时候人物小图的标记

	private Sprite theNoOnePicture = null;

	public void makeShow(thePlotItem theItem)
	{
		makeBigPicture(theItem);
		makeSmallPicture (theItem);
		makeBackPicture (theItem);
		maketalkBox (theItem);
	 
	}

	//没人说话就关闭掉文本框
	void maketalkBox(thePlotItem  theItem)
	{
		//有一些用来占时间的过长没必要显示说话文本框
		//print("check");
		//这段代码确实就写得挺简洁的，但是如果每一次都setActive = true很显然是一个浪费
		//SetActive(true)，很触发MonoBehaviour.OnEnable()事件，就算对象之前本就是activeSelf==true，事件依然会发生； 
		//SetActive(false)，很触发MonoBehaviour.OnDisable()事件,就算对象之前本就是activeSelf==false，事件依然会发生；
		//对策就是加一个检查
		bool show = !theItem.theTalkInformation.StartsWith ("---");
		if (theShowText.gameObject.activeInHierarchy !=  show)
		{
			theShowText.gameObject.SetActive (show);
		}
		//theShowText.gameObject.SetActive (show);
			
	}

	//说话的时候的人物大图
	List <string > picNames = new List<string> ();
	void makeBigPicture(thePlotItem theItem)
	{
		int count = 0;
		picNames.Clear ();//不用new而使用clear

		for (int i = 0; i < theItem.thePartyplayers.Length; i++) 
		{
			if (theItem.thePartyplayers [i].Equals ("noOne") == false)
				picNames.Add (theItem.thePartyplayers [i]);
		}

		//直接置空人物大图要比加载孔图要快很多
		//希望这种优化能够在手机平台也可以得到很好的应用
		switch (picNames.Count) 
		{
		   //第零种类情况，说话的时候不显示人，这种情况需要把所有的大图都做成"noOne"
		case 0: 
			{
				for (int i = 0; i < bigPeoplePictures.Length; i++) 
				{
					bigPeoplePictures [i].sprite = null;//  makeLoadSprite ("people/noOne");
				}
			}
			break;
			//第一种情况，说话的只有一个人，所以显示的是中间的图
		case 1:
			{
				//------------------这个加载方法就是每一步刷新，好想好写格调高，但是开销真的很大，每一个一个剧本帧---------------------------------------------//
				//for (int i = 0; i < bigPeoplePictures.Length; i++)
				//{
				//	bigPeoplePictures [i].sprite = null;// makeLoadSprite ("people/noOne");
				//}
				//bigPeoplePictures [1].sprite = systemInformations.makeLoadSprite ("people/big/" +picNames [0]+"_B") ;

				//------------------土一些的立即数方法，有针对性的刷新----------------------------------------------------------------------------------------//
				bigPeoplePictures [0].sprite = null;
				//loadWithCheckForBigPicture(bigPeoplePictures [1] , "people/big/" + picNames [0]+"_B" );
				//带有转换效果的换图
				StartCoroutine(changeBigPeoplePicture(bigPeoplePictures [1] ,"people/big/" + picNames [0]+"_B" ) );
				bigPeoplePictures [2].sprite = null;
			}
			break;
			//第二种情况，说话的有两个人所以是两边的图被显示
		case 2:
			{
				//------------------这个加载方法就是每一步刷新，好想好写格调高，但是开销真的很大，每一个一个剧本帧---------------------------------------------//
				//for (int i = 0; i < bigPeoplePictures.Length; i++)
				//{
				//	bigPeoplePictures [i].sprite = null;// makeLoadSprite ("people/noOne");
				//}
				//bigPeoplePictures [0].sprite = systemInformations.makeLoadSprite ( "people/big/" +picNames [0]+"_B");
				//bigPeoplePictures [2].sprite =systemInformations. makeLoadSprite ("people/big/" +picNames [1]+"_B");
				//------------------土一些的立即数方法，有针对性的刷新----------------------------------------------------------------------------------------//
				//loadWithCheckForBigPicture(bigPeoplePictures [0] , "people/big/" + picNames [0]+"_B" );
				StartCoroutine(changeBigPeoplePicture(bigPeoplePictures [0],"people/big/" + picNames [0]+"_B" ) );
				bigPeoplePictures [1].sprite = null;
				//loadWithCheckForBigPicture(bigPeoplePictures [2], "people/big/" + picNames [1]+"_B" );
				StartCoroutine(changeBigPeoplePicture(bigPeoplePictures [2], "people/big/" + picNames [1]+"_B" ) );
			}
			break;
			//第三种情况，说话的有三个人,，所以三张图都显示
		case 3:
			{
				for (int i = 0; i < bigPeoplePictures.Length; i++) 
				{
					//bigPeoplePictures [i].sprite = systemInformations.makeLoadSprite ("people/big/" + picNames [i]+"_B");
					//loadWithCheckForBigPicture(bigPeoplePictures [i] , "people/big/" + picNames [i]+"_B" );
					StartCoroutine(changeBigPeoplePicture(bigPeoplePictures [i], "people/big/" + picNames [i]+"_B" ) );
				}
			}
			break;
		}
       //接下来还可能需要做一些额外的动作
		string [] extra = theItem.extraAction.Split(':');
		if (extra.Length >= 2) 
		{
			int index = System.Convert.ToInt16 (extra [1]);
			bigPeoplePictures [index].gameObject.AddComponent (System.Type.GetType(extra[0]));
		}
	}

	IEnumerator changeBigPeoplePicture(SpriteRenderer thePic, string pictureName)
	{
		if (thePic.sprite == null || systemInformations.showExtraEffectsForAnimation == 0)
		{
			loadWithCheckForBigPicture (thePic, pictureName);
		} 
		else if (thePic.sprite!= null)
		{

			string[] filter = pictureName.Split ('/');
			string[] filter0 = thePic.sprite.name.Split ('/');
			string thePeopleName = filter0[filter0.Length-1].Split ('_') [0];
			string theNewPeopleName =   filter [filter.Length-1].Split ('_') [0];
			//同一个人的不同图片切换会有一个透明度的变化
			//print(pictureName +" -++- "+ thePic.sprite.name);
			//print (thePeopleName +" ---- "+ theNewPeopleName);
			if(thePeopleName == theNewPeopleName && pictureName != thePic.sprite.name)
			{
				Color theColor = Color.white;
				for (int i = 0; i < 3; i++) 
				{
					yield return new WaitForSeconds (0.025f);
					theColor.a -= 0.05f;
					thePic.color = theColor;
				}
				loadWithCheckForBigPicture (thePic, pictureName);
				for (int i = 0; i < 3; i++) 
				{
					yield return new WaitForSeconds (0.025f);
					theColor.a += 0.05f;
					thePic.color = theColor;
				}
			}
			else
			  loadWithCheckForBigPicture (thePic, pictureName);
		}

	}

	//如果只是检查单纯地load人物图的话，其实每一个剧本帧都是在不断重复加载 systemInformations.makeLoadSprite
	//这很显然有大量的浪费存在，所以在加载之前做一下检查比较好
	private void loadWithCheckForBigPicture(SpriteRenderer theImage  , string SpriteNameNew)
	{
		//剧本帧同样的人物大图就没有要再一次加载了
		if (theImage.sprite == null || theImage.sprite.name != SpriteNameNew)
		{
			//if (theImage.sprite != null) 
			//{
			//	print ("SpriteNameNew = " + SpriteNameNew + "    theImage.sprite.name = " + theImage.sprite.name);
			//	print ("load bigPicture");
			//}
		  theImage.sprite = systemInformations.makeLoadSprite (SpriteNameNew);
		}
	}
	private void loadWithCheckForSmallPicture(Image theImage  , string SpriteNameNew)
	{
		//剧本帧同样的人物大图就没有要再一次加载了
		if (theImage!= null && theImage .sprite!=null &&  theImage .sprite .name != SpriteNameNew)
		{
			//print ("SpriteNameNew = " + SpriteNameNew + "    theImage.sprite.name = " +  theImage .sprite );
		    //print ("load smallPicture");
			theImage .sprite  = systemInformations.makeLoadSprite (SpriteNameNew);
		}
	}


	//用这个方法来决定说话的时候的小图用哪一个（左边还是右边）
	private string theSpeakerNameNow = "--";
	private Image  theShowPictureInTextNow ;
	//记录当前说话的人的名字
	//实际上只有在改变说话人的时候才应该左右换头像
	//要不然一个人说话来回换头像成何体统
	Image getPart(string nameIn)
	{
		if (nameIn == theSpeakerNameNow)
			return theShowPictureInTextNow;

		theSpeakerNameNow = nameIn;

		//theShowPictureInText2.gameObject.SetActive (false);
		//减少上面setActive操作的次数
		//UI不同 ，这里不可以被重置为为null
		theShowPictureInText1.sprite = theNoOnePicture;
		theShowPictureInText2.sprite = theNoOnePicture;
	
		//这只是当前顶包的策略（随机），最好的方法就是用剧本控制
		int seed= Random.Range(1,4);//(int包前不包后 所以会是0,1,2,3 )
		if (seed >= 2) 
		{
			theShowPictureInTextNow = theShowPictureInText2;
			return theShowPictureInText2;
		}

		theShowPictureInTextNow = theShowPictureInText1;
		return theShowPictureInText1;
	}

	//说话的时候的小图
	void makeSmallPicture(thePlotItem theItem)
	{
		if (string.IsNullOrEmpty (theItem.theSpeekerName) || theItem.theSpeekerName .Equals("Caim"))
		{
			//不显示
			getPart(theItem.theSpeekerName).sprite = theNoOnePicture;
			thePeopleSmallPictureName = "noOne";
		} 
		else 
		{
			if (theItem.theSpeekerName.Equals (thePeopleSmallPictureName) == false)
			{
				string textureName = "people/small/" + theItem.theSpeekerName;
				//getPart(theItem.theSpeekerName).sprite = systemInformations.makeLoadSprite (textureName);
				loadWithCheckForSmallPicture(getPart(theItem.theSpeekerName), textureName);
				thePeopleSmallPictureName = theItem.theSpeekerName;
			}
		}
	}

	//背景图
	void makeBackPicture(thePlotItem theItem)
	{
		if ( string .IsNullOrEmpty(theItem.theBackPictureName)) 
		{
			//显示默认背景图，没有不行啊
			//这也是显示出来的第一张的图了
			theBackPicture.sprite = systemInformations.makeLoadSprite ("backPicture/home");
			theBackName = "home";
		} 
		else 
		{
			//只有指定换图的时候才会换图
			if ( string .IsNullOrEmpty (theItem.theBackPictureName) == false && theItem.theBackPictureName.Equals (theBackName) == false  )
			{
				//print ("back picture change");
				theBackName = theItem.theBackPictureName;

				if (theBackName != "changeBack")
				{
					//print ("change the back");
					 
					//CG 控制
					CGModeFile.CGActive (theBackName+"_Button");//激活这个CG
					theBackPicture.sprite =systemInformations. makeLoadSprite ("backPicture/" + theBackName);
				} 
				else 
				{
					//这个转换是一个非常细节的转换，需要保证转换的时间要比下一个剧本帧的转换时间短，要不然会出现图片转换会出现问题
					Invoke ("waitChangeInvoke", theItem.waitTimeForAutoSkip * 0.5f / Time.timeScale);
					if(theForwardPicture && theForwardPicture.gameObject .activeInHierarchy)
					    theForwardPicture.makeChange (theItem.waitTimeForAutoSkip*1.6f);
				}
			}

		}
	}

	//延时转换2（在这里不太推荐协程，似乎对事件的控制有一点不妙）
	private void waitChangeInvoke()
	{
		theBackPicture.sprite = systemInformations.makeLoadSprite ("backPicture/changeBack");

	}

 
	void Start ()
	{
		//UI小图与任务大图不同，人物大图是Sprite == null的时候不显示
		//但是UI小图是Image如果 == null 这里就会显示一张白图片
		//这当然是不行的，因此需要比较频繁地置空图片
		//所以将这一透明图保存起来各种复用
		theNoOnePicture =  systemInformations. makeLoadSprite ("people/noOne");
		Image ST = getPart("----------初始化----------");//做初始化，强制让两张图都变成透明
	}
}
