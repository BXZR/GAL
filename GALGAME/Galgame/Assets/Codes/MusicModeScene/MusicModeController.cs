using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicModeController : MonoBehaviour {

	//暂时没有想到有什么好的方法来做动态加载，所以就是用了很死的写法
	//这个地方我可以优化，甚至优化空间很大

	//下面保存的内容写得很死不是很推荐
    //保存音乐名称
	public string[] musicNames;
	//不同音乐有不同的图
	public string[] pictureNames;
	//用于挂在父物体的theContantPanel引用
	//用引用赋值的方法来做可以节约大量的时间
	private  MusicController theMusicController;
	public Image theShowImage;

	public Transform theContantPanel;
	//展示按钮预设物
	public GameObject theButtonProfab;
    //显示音乐名字的Text
	public Text theMusicNameText;
	//保留一个引用作为初始值
	private musicModeButton theMusicFirst = null;
	//进行初始化
	//世界上这个脚本的功能最重要的就是初始化
	public  void makeStart()
	{
		theMusicController =this.GetComponent <AudioGetter>().GetMusicController();
		for (int i = 0; i < musicNames.Length; i++)
		{
			GameObject theButton = GameObject.Instantiate (theButtonProfab);
			musicModeButton MB = theButton.GetComponent<musicModeButton> ();
			MB .makeStart (musicNames[i] , pictureNames[i] , theMusicController,theShowImage,theMusicNameText);
			theButton.gameObject.transform.SetParent (theContantPanel.transform);
			if (theMusicFirst == null)
				theMusicFirst = MB;
		}
		if (theMusicFirst != null)
			theMusicFirst.playSound ();
	}


	void Start () 
	{
		makeStart ();
	}
}
