using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GVREscape : MonoBehaviour
{
#if UNITY_ANDROID
    void LateUpdate()
    {
        // Exit app if the gvr x button in upper left of screen is tapped.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
#endif
}
