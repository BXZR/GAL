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

	void Start () 
	{
		
	}


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
		bool show = theItem.theTalkInformation.StartsWith ("---");
		theShowText.gameObject.SetActive (!show);
			
	}

	//说话的时候的人物大图
	void makeBigPicture(thePlotItem theItem)
	{
		int count = 0;
		List <string > picNames = new List<string> ();

		for (int i = 0; i < theItem.thePartyplayers.Length; i++) 
		{
			if (theItem.thePartyplayers [i].Equals ("noOne") == false)
				picNames.Add (theItem.thePartyplayers [i]);
		}
		switch (picNames.Count) 
		{
		   //第零种类情况，说话的时候不显示人，这种情况需要把所有的大图都做成"noOne"
		case 0: 
			{
				for (int i = 0; i < bigPeoplePictures.Length; i++) {
					bigPeoplePictures [i].sprite =  makeLoadSprite ("people/noOne");

				}
			}
			break;
			//第一种情况，说话的只有一个人，所以显示的是中间的图
		case 1:
			{
				for (int i = 0; i < bigPeoplePictures.Length; i++) {
					bigPeoplePictures [i].sprite = makeLoadSprite ("people/noOne");
				}
				bigPeoplePictures [1].sprite = makeLoadSprite ("people/big/" +picNames [0]+"_B") ;
			}
			break;
			//第二种情况，说话的有两个人所以是两边的图被显示
		case 2:
			{
				for (int i = 0; i < bigPeoplePictures.Length; i++) {
					bigPeoplePictures [i].sprite = makeLoadSprite ("people/noOne");
				}
				bigPeoplePictures [0].sprite = makeLoadSprite ( "people/big/" +picNames [0]+"_B");
				bigPeoplePictures [2].sprite = makeLoadSprite ("people/big/" +picNames [1]+"_B");
			}
			break;
			//第三种情况，说话的有三个人,，所以三张图都显示
		case 3:
			{
				for (int i = 0; i < bigPeoplePictures.Length; i++) {
					bigPeoplePictures [i].sprite = makeLoadSprite ("people/big/" + picNames [i]+"_B");
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

	//用这个方法来决定说话的时候的小图用哪一个（左边还是右边）
	Image getPart()
	{
		//这只是当前顶包的策略（随机），最好的方法就是用剧本控制
		int seed= Random.Range(1,4);//(int包前不包后 所以会是0,1,2,3 )
		if (seed >= 2) 
		{
			theShowPictureInText1.gameObject.SetActive (false);
			theShowPictureInText2.gameObject.SetActive (true);
			return theShowPictureInText2;
		}
		theShowPictureInText2.gameObject.SetActive (false);
		theShowPictureInText1.gameObject.SetActive (true);
		return theShowPictureInText1;
	}

	//说话的时候的小图
	void makeSmallPicture(thePlotItem theItem)
	{
		if (string.IsNullOrEmpty (theItem.theSpeekerName) || theItem.theSpeekerName .Equals("Caim"))
		{
			//不显示
			getPart().sprite = makeLoadSprite ("people/noOne");
			thePeopleSmallPictureName = "noOne";
		} 
		else 
		{
			if (theItem.theSpeekerName.Equals (thePeopleSmallPictureName) == false)
			{
				string textureName = "people/small/" + theItem.theSpeekerName;
				getPart().sprite = makeLoadSprite (textureName);
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
			theBackPicture.sprite = makeLoadSprite ("backPicture/home");
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
					theBackPicture.sprite = makeLoadSprite ("backPicture/" + theBackName);
				} 
				else 
				{
					//这个转换是一个非常细节的转换，需要保证转换的时间要比下一个剧本帧的转换时间短，要不然会出现图片转换会出现问题
					Invoke ("waitChangeInvoke", theItem.waitTimeForAutoSkip * 0.5f / Time.timeScale);
					theForwardPicture.makeChange (theItem.waitTimeForAutoSkip*1.6f);
				}
			}

		}
	}

	//延时转换2（在这里不太推荐协程，似乎对事件的控制有一点不妙）
	private void waitChangeInvoke()
	{
		theBackPicture.sprite = makeLoadSprite ("backPicture/changeBack");

	}

	public Sprite makeLoadSprite(string textureName)
	{
		//textureName = "people/noOne";
		Texture2D theTextureIn = Resources.Load <Texture2D> (textureName);
		return Sprite .Create(theTextureIn,new Rect (0,0,theTextureIn.width,theTextureIn.height),new Vector2 (0,0));
	}
}
