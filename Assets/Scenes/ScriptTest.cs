using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptTest : MonoBehaviour {
    

	void Update() {
        transform.localRotation = Camera.main.transform.rotation;
	}
    
}