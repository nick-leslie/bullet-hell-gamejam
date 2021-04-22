using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretBrain : MonoBehaviour
{
    public GameObject target;
    private Room home;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            AsignTarget();
        }
    }
    public void AsignTarget() 
    {
        GameObject[] enemys = home.ThingsInRoom.FindAll((GameObject obj) => obj.tag == "enemy").ToArray();
        int enemyTarget = Random.Range(0, enemys.Length);
        target = enemys[enemyTarget];
    }
    public void AssignHome(GameObject potentalHome)
    {
        home = potentalHome.GetComponent<Room>();
    }
}
