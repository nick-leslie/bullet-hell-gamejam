using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AIbrain : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    public GameObject target;
    private Vector3 targetPos;
    public bool atTarget;
    public GameObject home;
    private void Start()
    {
        
    }
    private void Update()
    {
       move();
    }
    public void move()
    {
        if(Vector2.Distance(transform.position,targetPos) > 0.2)
        {
            transform.position = Vector2.MoveTowards(transform.position,targetPos,Speed*Time.deltaTime);
        } else
        {
            atTarget = true;
        }
    }
    public void SwapTarget(GameObject newTarget)
    {
        //add check for if target is within the same room
        target = newTarget;
    }
    public void changeTargetPos(Vector3 newtargetPos)
    {
        targetPos = newtargetPos;
        atTarget = false;
    }
    public void TargetTransform(Transform newTarget)
    {
        targetPos = newTarget.position;
    }
    public void FindTarget()
    {
        Room HomeInfo = home.GetComponent<Room>();
        GameObject potentalPlayer = HomeInfo.ThingsInRoom.Find((GameObject obj) => obj.tag == "Player");
        if (potentalPlayer != null)
        {
            target = potentalPlayer;
        } else
        {
            List<GameObject> turrets = HomeInfo.ThingsInRoom.FindAll((GameObject obj) => obj.tag == "turret");
            if(turrets.Count > 0)
            {
                int turretTarget = UnityEngine.Random.Range(0, turrets.Count);
                target = turrets[turretTarget];
            }
        }
    }
}
