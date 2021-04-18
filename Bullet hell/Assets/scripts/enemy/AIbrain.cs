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
}
