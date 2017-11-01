using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textSpeedSlider : MonoBehaviour {

	//这个脚本专门控制各种speed的slider
	//原理就是控制各种systeminformation的数值
	private Slider theSlider;
	//注意每一个slider的外部面板都是需要设置的
	public ConfigItems theConfigType = ConfigItems.AutoTextSpeed;
	void Awake ()
	{  
		theSlider = this.GetComponent <Slider> ();
		//初始化的时候尝试读取配置文件，没有文件默认返回1
		theSlider.value = configController.getConfigItem (theConfigType);
	}
	//控制剧本等待时间
	public  void  makePlotTimeWait()
	{
		systemInformations.plotTimeWait = 0.35f - 0.1f * theSlider.value; 
		//每一次变化都会修改配置文件
		configController.setConfigItem (theConfigType ,theSlider.value.ToString("f2") );
	}

	//控制文本出现时间
	public  void  makeTextShowSpeed()
	{
		systemInformations.textShowTime  = 0.2f - 0.1f * theSlider.value; 
		//每一次变化都会修改配置文件
		configController.setConfigItem (theConfigType ,theSlider.value.ToString("f2") );
	}
	//关闭设置面板的时候才会进行一次IO
	void OnDisable()
	{
		configController.flashConfig ();
	}
}
