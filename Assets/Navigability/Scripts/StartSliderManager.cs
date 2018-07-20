using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartSliderManager : MonoBehaviour {
    public Slider slider;

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        slider.value += 0.008f;

        if (slider.value >= 1)
        {
            SceneManager.LoadScene(1);
        }
    }
}
