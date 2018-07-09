using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptTest : MonoBehaviour {
	public float distanceFromCamera;
	public float height;

	void Update() {
        //transform.localRotation = Camera.main.transform.rotation;
		//transform.position = (Camera.main.transform.forward * distanceFromCamera);
		transform.rotation = Quaternion.identity;
	}
    
}