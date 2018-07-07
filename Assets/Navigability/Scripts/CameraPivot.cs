using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    public static CameraPivot Instance;
    public Transform head;
    // Use this for initialization
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRotation(Vector3 rot)
    {
        float newY = rot.y;
        float oldY = head.localEulerAngles.y;
        rot.y = newY - oldY;
        transform.localEulerAngles = rot;
    }


}