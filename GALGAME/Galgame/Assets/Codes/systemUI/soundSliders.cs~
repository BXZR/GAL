using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundSliders : MonoBehaviour {

	public AudioSource theAudioSource;//这种slider是用来控制各种声音的大小
	private Slider theSlider;
	void Start ()
	{
		theSlider = this.GetComponent <Slider> ();
	}
	public  void makeSoundValue()
	{
		theAudioSource.volume = theSlider.value;
	}
}
