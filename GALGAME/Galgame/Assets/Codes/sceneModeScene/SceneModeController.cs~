using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneModeController : MonoBehaviour {

	//scene模式的控制单元
	public int sceneGroup = 1;//通CG模式一样，也分为三个组
	//三个组分别是 艾丽斯，菲奥奈，吉克
	public GameObject contant;//声称的button的挂载点也就是父物体
	public GameObject buttonProfabe;//按钮的预设物

	private  List< scenes> SceneNameForGroup1;//艾丽斯
	private  List< scenes> SceneNameForGroup2;//菲奥奈
	private  List< scenes> SceneNameForGroup3;//其他

	 
	//选择分组
	public void changeSceneButton(int index = 0)
	{
		sceneGroup = index;
		sceneModeButton[] buttons = contant.GetComponentsInChildren<sceneModeButton> ();
		for (int i = 0; i < buttons.Length; i++)
			Destroy (buttons[i].gameObject);
		List< scenes> theSelected = getSelectGroup ();
		for (int i = 0; i < theSelected.Count; i++) 
		{
			GameObject theSceneModeButton = GameObject.Instantiate<GameObject> (buttonProfabe);
			sceneModeButton theButton = 	theSceneModeButton.GetComponent <sceneModeButton> ();
			theButton.makeStart (theSelected [i].startIndex,theSelected [i].endIndex, theSelected [i].sceneName , theSelected[i].isOpened);
			theSceneModeButton.transform.SetParent (contant.transform);//排版用grid自行解决（在这里进行设置的配置）
		}
		
	}

	private List< scenes> getSelectGroup()
	{
		List< scenes> selected = new List< scenes> ();
		if (sceneGroup == 1)
			selected  = SceneNameForGroup1;
		if (sceneGroup == 2)
			selected  = SceneNameForGroup2;
		if (sceneGroup == 3)
			selected  = SceneNameForGroup3;
		return selected;
	}

	private void makeStart()
	{
		SceneNameForGroup1 = new List< scenes> ();
		SceneNameForGroup2 = new List< scenes> ();
		SceneNameForGroup3 = new List< scenes> ();
		for(int i = 0 ;i < SceneModeFile.theSceneList1.Count ; i ++)
			SceneNameForGroup1.Add(SceneModeFile.theSceneList1[i]);
		for(int i = 0 ;i < SceneModeFile.theSceneList2.Count ; i ++)
			SceneNameForGroup2.Add(SceneModeFile.theSceneList2[i]);
		for(int i = 0 ;i < SceneModeFile.theSceneList3.Count ; i ++)
			SceneNameForGroup3.Add(SceneModeFile.theSceneList3[i]);
	}
	void Start () 
	{
		SceneModeFile.InitValues ();
		makeStart ();
		changeSceneButton (1);
	}
}


