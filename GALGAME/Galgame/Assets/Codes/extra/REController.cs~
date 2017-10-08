using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REController : MonoBehaviour {

	//这个脚本强制就改分辨率
	void Start () {
		Screen.SetResolution(1024, 600, true, 60);
		this.GetComponent <Camera > ().aspect = 1024f / 600f;
		Destroy (this.gameObject.GetComponent (this.GetType ()));
	}
	
 
}
