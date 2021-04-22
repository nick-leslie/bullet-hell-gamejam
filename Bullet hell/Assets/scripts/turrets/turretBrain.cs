using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretBrain : MonoBehaviour
{
    public GameObject target;
    [SerializeField]
    private Room home;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (home != null)
        {
            if (target == null)
            {
                AssignTarget();
            }
        }
    }
    public void AssignTarget() 
    {
        GameObject[] enemys = home.ThingsInRoom.FindAll((GameObject obj) => obj.tag == "enemy").ToArray();
        int enemyTarget = Random.Range(0, enemys.Length);
        if (enemys.Length > 0)
        {
            target = enemys[enemyTarget];
        }
    }
    public void AssignHome(GameObject potentalHome)
    {
        home = potentalHome.GetComponent<Room>();
    }
}
