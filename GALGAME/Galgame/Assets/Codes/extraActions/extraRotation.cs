using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏人物大图的额外动作
public class extraRotation : MonoBehaviour {
	//游戏人物大图的额外旋转动作

	private Vector3 pos;
	private Quaternion qua;
	// Use this for initialization
	void Start () 
	{
		pos = this.transform.position;
		qua = this.transform.rotation;
		Destroy (this , 0.5f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.Rotate (new Vector3 (0,0,1) * -90f*Time .deltaTime);
	}
	void OnDestroy()
	{
		this.transform.position = pos;
		this.transform.rotation = qua;
	}
}
