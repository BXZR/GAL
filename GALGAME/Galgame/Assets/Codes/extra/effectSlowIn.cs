using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//可选效果，渐变进入，这是一个非常细节的效果
public class effectSlowIn : MonoBehaviour {

	Image theImage;
	float timer = 30f;//等待的最长时间，默认值是30秒

	public void  makeChange(float timerIn = 30)
	{
		systemInformations.canControll = false;
		timer = timerIn/2;

		StartCoroutine ("StartTime");
	}

	IEnumerator StartTime()
	{
		//yield return new WaitForSeconds(timer);//淡入延迟时间
		theImage.CrossFadeAlpha(1, timer, true);//淡入执行参数，1=显示 0.5f=时间 true=start
		yield return new WaitForSeconds(timer); //淡出延迟时间
		theImage.CrossFadeAlpha(0,timer,true);//淡出执行参数，0=透明 0.5f=时间 true=start
		yield return new WaitForSeconds(timer); //淡出延迟时间
		systemInformations .canControll = true;
	}
	void Start ()
	{
		//每一次开启场景的时候都有一个淡出（必要）
		//如果没有这个淡出，slowInChangePanel就会一直显示，这与其他的设置有一点矛盾
		//这在这里也只能是一个权宜之计
		theImage = this.GetComponent <Image > ();
		theImage.CrossFadeAlpha(0,0.05f,true);//淡出执行参数，0=透明 0.5f=时间 true=start
	}
}
