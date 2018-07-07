using UnityEngine;
using System.Collections;

public class ScreenOrientationManager : MonoBehaviour
{
    
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    void OnDestroy()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
}
