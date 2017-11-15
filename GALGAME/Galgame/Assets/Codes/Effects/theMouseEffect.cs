using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theMouseEffect : MonoBehaviour {

	private ParticleSystem theEffectForMouse = null;

	void Start () {
		GameObject theEffect=  GameObject.Instantiate<GameObject>( Resources.Load<GameObject> ("Effects/mouseMove"));
		theEffectForMouse = theEffect.GetComponent <ParticleSystem> ();
		if (theEffectForMouse == null)
			Destroy (this);
		else
			theEffectForMouse.Stop ();
	} 
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButton (0)) 
		{
			if(theEffectForMouse.isPlaying == false)
			theEffectForMouse.Play ();
			theEffectForMouse.transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 1.0f));
		}
		if (Input.GetMouseButtonUp (0))
		  theEffectForMouse.Stop ();
	}
}
