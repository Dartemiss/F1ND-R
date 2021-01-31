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

        string timeInMintuesString = timeInMinutes.ToString();
        if (timeInMinutes < 10) 
        {
            timeInMintuesString= "0" + timeInMintuesString;
        }

        string timeInSecondsString = timeInSeconds.ToString();
        if (timeInSeconds < 10)
        {
            timeInSecondsString = "0" + timeInSecondsString;
        }

        timeText.text = timeInMintuesString + ":" + timeInSecondsString;
        if (time < 60 && time >= 30)
        {
            timeText.GetComponent<Animator>().SetTrigger("Hurry");
        }
        else if (time < 30)
        {
            timeText.GetComponent<Animator>().SetTrigger("HURRYUP");
        }
    }
}
