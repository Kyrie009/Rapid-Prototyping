using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Timer : GameBehaviour
{
    float currentTime;
    bool isTiming;

    void Start()
    {
        StartTimer();
    }

    void Update()
    {
        if (isTiming)
        {
            currentTime += Time.deltaTime;
            
        }
    }

    /// <summary>
    /// Start Timer from zero and increment in real time seconds
    /// </summary>
    public void StartTimer()
    {
        isTiming = true;
        currentTime = 0;
    }

    /// <summary>
    /// Will pause timer with intention to resume
    /// </summary>
    public void PauseTimer()
    {
        isTiming = false;
    }

    /// <summary>
    /// Stops the timer from timing
    /// </summary>
    public void StopTimer()
    {
        isTiming = false;
    }

    /// <summary>
    /// resumes the time
    /// </summary>
    public void ResumeTimer()
    {
        isTiming = true;
    }

    /// <summary>
    /// Get the current timer
    /// </summary>
    /// <returns></returns>
    public float GetTime()
    {
        return currentTime;
    }

    public bool IsTiming()
    {
        return isTiming;
    }
}
