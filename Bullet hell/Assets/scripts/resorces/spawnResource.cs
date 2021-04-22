using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnResource : MonoBehaviour
{
    TimeEvent timeingEvents;
    [SerializeField]
    private GameObject recorese;
    [SerializeField]
    private Transform spawnPos;
    [SerializeField]
    private float spred;
    [SerializeField]
    private float SPAWNAMOUNT;
    // Start is called before the first frame update
    void Start()
    {
        timeingEvents = GameObject.FindGameObjectWithTag("timeManiger").GetComponent<TimeEvent>();
        timeingEvents.onSecondpassed += spawn;
    }
    void spawn(int time)
    {
        if(time ==0)
        {
            for (int i = 0; i < SPAWNAMOUNT; i++)
            {
                GameObject newRescorce = Instantiate(recorese, spawnPos.transform.position + new Vector3(Random.Range(-spred, spred), Random.Range(-spred, spred), 0), spawnPos.transform.rotation * Quaternion.Euler(0, 0, Random.Range(-spred, spred)));
            }
            //newRescorce.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-startVelocity, startVelocity), Random.Range(-startVelocity, startVelocity));

        }
    }
    private void OnDisable()
    {
        timeingEvents.onSecondpassed -= spawn;
    }
    private void OnDestroy()
    {
        timeingEvents.onSecondpassed -= spawn;
    }
}
