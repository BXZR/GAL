using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class theCreaterShowerMove : MonoBehaviour {

	//等待一段时间之后进行移动
	private float moveLengthMax =0f;//自适应移动长度
	private float movedNow = 0f;//当前移动的长度，如果超过max就停止
	private bool isStarted = false;//不是最开始就移动的，至少所有初始效果都完成
	private bool isStartedForSave = false ;//一开始整体也是开始的
	public float speed = 50f;//速度，注意UI的缩放和一般的东西不一样，所以这里的速度要比较大
	private Vector3 startPosition ;//记录初始坐标
	void Start () 
	{
		moveLengthMax = this.GetComponent<RectTransform> ().sizeDelta.y + Screen.height;
		Invoke ("moveStart" , 1);//延迟1秒钟
	}

	void moveStart()
	{
		isStarted = true;
		isStartedForSave = true;
		startPosition = this.transform.position;
	}


	private void move()
	{
		if (isStarted == false)
			return;
		float moveStep = Time.deltaTime * speed;
		this.transform.Translate (new Vector3(0,1,0) * moveStep);
		movedNow += moveStep;
		if (movedNow >= moveLengthMax)
			isStarted = false;
	}

	private void  makeEnd()
	{
		if (Input.anyKeyDown) 
		{
			if (isStartedForSave && isStarted) 
			{
				isStarted = false;
				this.transform.position = startPosition + new Vector3 (0, moveLengthMax, 0);
			} 
			else if (isStartedForSave && isStarted == false) 
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene ("start");
			}
		}
	}
	void Update ()
	{
		move ();
		makeEnd ();
	}
}
