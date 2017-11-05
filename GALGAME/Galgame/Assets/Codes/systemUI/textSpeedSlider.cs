using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textSpeedSlider : MonoBehaviour,configUser {

	//这个脚本专门控制各种speed的slider
	//原理就是控制各种systeminformation的数值
	private Slider theSlider;
	//注意每一个slider的外部面板都是需要设置的
	public ConfigItems theConfigType = ConfigItems.AutoTextSpeed;
 

	void Awake ()
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
 

	//------------------------------------------------------------------------------------------------//
	//用来统一外部调用的方法（来自于接口抽象）
	public void saveToConfig ()
	{
		//每一次变化都会修改配置文件
		configController.setConfigItem (theConfigType ,theSlider.value.ToString("f2") );
	}

	public void  loadFromConfig ()
	{
		theSlider.value = configController.getConfigItem (theConfigType);
	}

 

}
