using UnityEngine;
using System.Collections;

public class TimerScript : MonoBehaviour
{
    public float targetTime = 60.0f;

    void Update()
    {

        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }

    }

    public void StartTimer(float seconds)
    {
        targetTime = seconds;
    }

    void timerEnded()
    {
        //do your stuff here.
    }
}