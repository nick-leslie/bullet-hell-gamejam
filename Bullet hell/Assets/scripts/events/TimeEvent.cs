using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEvent : MonoBehaviour {


    public delegate void secondPassedDelegate(int time);
    public event secondPassedDelegate onSecondpassed;

    public void Start()
    {
        StartCoroutine(TimePassed());
    }
    public IEnumerator TimePassed()
    {
        int timeFrom10=10;
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            onSecondpassed?.Invoke(timeFrom10); // this calls the event if there are subscribers to it
            timeFrom10--;
            if (timeFrom10 < 0)
            {
                timeFrom10 = 10;
            }
        }
    }
}
