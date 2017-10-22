using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textSpeedSlider : MonoBehaviour {

	//这个脚本专门控制各种speed的slider
	//原理就是控制各种systeminformation的数值
	private Slider theSlider;
	void Start ()
	{
		theSlider = this.GetComponent <Slider> ();
	}
	//控制剧本等待时间
	public  void  makePlotTimeWait()
	{
		systemInformations.plotTimeWait = 0.35f - 0.1f * theSlider.value; 
	}

	//控制文本出现时间
	public  void  makeTextShowSpeed()
	{
		systemInformations.textShowTime  = 0.2f - 0.1f * theSlider.value; 
	}
}
