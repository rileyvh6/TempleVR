using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

   

    public Text timerText;
    public float totalTime=300;
   


    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
    float t = totalTime - Time.time;
        string minutes = (totalTime / 60).ToString();
        string seconds = (t % 60).ToString("f0");

        timerText.text=minutes + ":" + seconds;

        if (t<=0f)
        {
            LevelManager.CompletedPuzzle();
            this.enabled = false;
        }

    }

   
}
