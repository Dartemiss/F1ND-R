using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamaTimer : MonoBehaviour
{

    public float currentTimeRemaining = 10f;
    public float totalTimeRemaining = 10f;
    public bool isActive = false;

    // Update is called once per frame
    void Update()
    {
        if(isActive && currentTimeRemaining > 0f)
        {
            currentTimeRemaining -= Time.deltaTime;
        }
    
    }

    public void StartTimer(float time)
    {
        totalTimeRemaining = time;
        currentTimeRemaining = time;
        isActive = true;
    }

    public void StopTimer()
    {
        isActive = false;
        currentTimeRemaining = 0f;
    }

    public void PauseTimer()
    {
        isActive = false;
    }

    public void ResumeTimer()
    {
        isActive = true;
    }

    public bool HasTimedOut()
    {
        if(currentTimeRemaining <= 0f)
        {
            return true;
        }

        return false;
    }

    //Reset the timer
    public void Reset()
    {
        currentTimeRemaining = totalTimeRemaining;
        isActive = true;
    }

    public void SetTotalTime(float time)
    {
        totalTimeRemaining = time;
    }
}
