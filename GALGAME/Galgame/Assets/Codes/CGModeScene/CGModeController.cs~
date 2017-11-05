using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CGModeController : MonoBehaviour {
	//这个类控制CG模式的按钮生成以及基础的button的初始化

	//CG 分为三组 艾丽斯 菲奥奈 其他
	//每一组的CG最多12张
	public string[] CGNameForGroup1;//艾丽斯
	public string[] CGNameForGroup2;//菲奥奈
	public string[] CGNameForGroup3;//其他

	public int selectedGroup = 1;//当前选择的grop
	public GameObject contant;//声称的button的挂载点也就是父物体
	public GameObject buttonProfabe;//按钮的预设物
	public Image theBigPicture;//显示的大图
	//制作在面板中选择的按钮
	//图片来自于forButton的小图
	//也可以通过传入index使得当前的CGgroup改变
	public void  makeCGSelectButton(int index = -1)
	{
		if (index >= 1)
			selectedGroup = index; 
		
		makeClear ();
		//选择分组
		string[] theSelectOne =  selectGroup();
		makeButtons(theSelectOne);



	}



	//建立预设物与设置信息
	void makeButtons(string[] theSelectOne)
	{
		for (int i = 0; i < theSelectOne.Length; i++) 
		{
			GameObject theButton = GameObject.Instantiate <GameObject>( buttonProfabe);
			theButton.transform.SetParent(contant.transform);
			//按钮上面加载的都是小图
			theButton.GetComponent <Button>().image.sprite = makeLoadSprite("backPictureForButton/"+theSelectOne[i]);
			CGModeButton  theCGButton= theButton.gameObject.GetComponent <CGModeButton> ();
			theCGButton.makeStart (theSelectOne[i], theBigPicture , this);
		}

	}

	//选择分组简单工厂模式
	string [] selectGroup()
	{

		string[] selected = {};
		switch (selectedGroup) 
		{
		case 1:
			selected = CGNameForGroup1;
			break;
		case 2:
			selected = CGNameForGroup2;
			break;
		case 3:
			selected = CGNameForGroup3;
			break;
		}

		return selected;
	}

	void  makeClear()
	{
		//如果上一次有生成内容就清空
		CGModeButton[] buttons = contant.GetComponentsInChildren<CGModeButton> ();
		for (int i = 0; i < buttons.Length; i++)
		{
			Destroy (buttons[i].gameObject);
		}
	}
	//加载图片
	public  Sprite makeLoadSprite(string textureName)
	{
		//textureName = "people/noOne";
		Texture2D theTextureIn = Resources.Load <Texture2D> (textureName);
		return Sprite .Create(theTextureIn,new Rect (0,0,theTextureIn.width,theTextureIn.height),new Vector2 (0,0));
	}



	void Start () 
	{
		makeCGSelectButton (3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
