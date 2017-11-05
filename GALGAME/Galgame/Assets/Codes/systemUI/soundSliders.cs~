using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundSliders : MonoBehaviour , configUser {

	public AudioSource theAudioSource;//这种slider是用来控制各种声音的大小
	public ConfigItems theConfigType = ConfigItems.BGMVolume;
	//为了与配置文件想配并且不增加代码的重复程度
	//只好使用枚举的标记
	//但是这样也需要承担风险，例如slider选择了font这种人工的错误需要注意
	private Slider theSlider;

	void Awake ()
	{  
		theSlider = this.GetComponent <Slider> ();
	}
	public  void makeSoundValue()
	{
		theAudioSource.volume = theSlider.value;
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
