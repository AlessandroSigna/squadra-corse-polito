using UnityEngine;
using System.Collections;

public class CameraModeManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        bool splitScreenFlag = MenuSettings.Instance.cardboardMode;
        GetComponent<Camera>().enabled = !splitScreenFlag;
	}
	
}
