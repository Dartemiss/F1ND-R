using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerDisplayManager : MonoBehaviour
{
    public Text timeText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTime(float time)
    {
        int timeInMinutes = (int)time / 60;
        int timeInSeconds = (int)time % 60;

        if (timeInMinutes < 10)
        {
            timeText.text = "0" + timeInMinutes + ":" + timeInSeconds;
        }
        else
        {
            timeText.text = timeInMinutes + ":" + timeInSeconds;
        }
    }
}
