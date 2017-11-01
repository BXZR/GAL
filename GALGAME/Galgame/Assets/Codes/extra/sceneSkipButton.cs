using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneSkipButton : MonoBehaviour {
	//最单纯的场景跳转按钮，单独列出来防止耦合

	public string aimsceneName = "start";
	public void MoveToScene()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene (aimsceneName);

	}
}
