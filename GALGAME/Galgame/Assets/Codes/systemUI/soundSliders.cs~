using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundSliders : MonoBehaviour {

	public AudioSource theAudioSource;//这种slider是用来控制各种声音的大小
	public ConfigItems theConfigType = ConfigItems.BGMVolume;
	//为了与配置文件想配并且不增加代码的重复程度
	//只好使用枚举的标记
	//但是这样也需要承担风险，例如slider选择了font这种人工的错误需要注意
	private Slider theSlider;

	void Awake ()
	{  
		theSlider = this.GetComponent <Slider> ();
		//初始化的时候尝试读取配置文件，没有文件默认返回1
		theSlider.value = configController.getConfigItem (theConfigType);
	}
	public  void makeSoundValue()
	{
		theAudioSource.volume = theSlider.value;
		//每一次变化都会修改配置文件
		configController.setConfigItem (theConfigType ,theSlider.value.ToString("f2") );
	}

	//关闭设置面板的时候才会进行一次IO
	void OnDisable()
	{
		configController.flashConfig ();
	}
}
