using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadSceneController : MonoBehaviour {

	//过渡场景使用脚本，功能略显集成

	public string nextSceneName = "gal_1";
	private float timer = 1f;//渐入渐出的时间间隔
	public Image theShowImage ;//UI 上面显示的Image
	public Sprite[] Sprites;//在这个场景中我不想用太多加载的功能，所以直接用的是立即数
	//public float allTimeUse = 0f;//渐入渐出的总时间（测试用）
	private bool isLoading = false;//为了防止多次调用做的标记
	public Slider theProcessSlider ;//加载进度slider

	IEnumerator startLoading()//异步加载新的场景
	{
		yield return new WaitForSeconds(1);//等一段时间
		//这是为了显示那个“一梦浮灯”
		theProcessSlider.gameObject.SetActive (true);
		//SceneManager貌似是新版的场景管理
		//异步加载场景
		AsyncOperation acop = SceneManager.LoadSceneAsync(nextSceneName);
		//因为太快了所以需要加一点缓冲
		acop.allowSceneActivation = false;
		theProcessSlider.value = 0;
		float value = 0f;

		//等待40帧大约0.8秒
		for (int i = 0; i < 100; i++)
		{
			value++;
			//theProcessBar.value =  value /120;
			theProcessSlider.value = value/125f;
			yield return new WaitForEndOfFrame ();
		}

		theProcessSlider.value = value+0.2f*acop.progress;

		acop.allowSceneActivation = true;

		yield return acop;

	}

	IEnumerator ShowInFront()
	{
		theProcessSlider.gameObject.SetActive (false);
		theShowImage.CrossFadeAlpha(0,timer,true);//淡出执行参数，0=透明 0.5f=时间 true=start
		yield return new WaitForSeconds(timer); //淡出延迟时间

		int i =0;
		for( ;i < Sprites .Length-1; i++)
		{
			theShowImage.overrideSprite = Sprites [i];
			theShowImage.CrossFadeAlpha(1, timer, true);//淡入执行参数，1=显示 0.5f=时间 true=start
		    yield return new WaitForSeconds(timer*2); //淡出延迟时间
			theShowImage.CrossFadeAlpha(0,timer,true);//淡出执行参数，0=透明 0.5f=时间 true=start
			yield return new WaitForSeconds(timer); //淡出延迟时间

		}

		//异步加载场景
		isLoading = true;

		StartCoroutine (startLoading());
		theShowImage.overrideSprite = Sprites [i];
		theShowImage.CrossFadeAlpha(1, timer, true);//淡入执行参数，1=显示 0.5f=时间 true=start
	}


	// Use this for initialization
	void Start () {
		//异步加载真正的gal场景
		//同时播放片头感言
		StartCoroutine(ShowInFront());
	}
	
	// Update is called once per frame
	void Update () 
	{
		//allTimeUse += Time.deltaTime;
		if (Input.anyKeyDown &&  isLoading == false) 
		{
			isLoading = true;
			StopAllCoroutines ();//需要加入这句话，推断可能是因为淡入淡出的底层实现是协程
			StopCoroutine (ShowInFront());
			StartCoroutine (startLoading());
			theShowImage.overrideSprite = Sprites [Sprites .Length -1];
			theShowImage.CrossFadeAlpha(1, 0.2f, true);//淡入执行参数，1=显示 0.5f=时间 true=start
		}	
	}
}
