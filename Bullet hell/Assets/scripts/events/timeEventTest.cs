using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeEventTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TimeEvent timeingEvents = GetComponent<TimeEvent>();
        timeingEvents.onSecondpassed += printTest;
    }
    void printTest(int time)
    {
    }
}
