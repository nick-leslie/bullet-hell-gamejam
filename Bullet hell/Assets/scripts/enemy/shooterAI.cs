using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooterAI : MonoBehaviour
{
    private AIbrain brain;
    [SerializeField]
    private float PathFindDistence;
    private Vector2 point;
    [SerializeField]
    private GameObject Home;
    // Start is called before the first frame update
    void Start()
    {
        brain = gameObject.GetComponent<AIbrain>();
        StartPathfinding();
    }
    // Update is called once per frame
    void Update()
    {
        if (brain.atTarget==true)
        {
            StartPathfinding();
        }
    }
    private void StartPathfinding()
    {
        //ray cast
        point = RandomInRange();
        brain.changeTargetPos(point);
    }
    private void checkForCloseColiders()
    {
        //send out a circle that checks close by
    }
    private Vector2 RandomInHome()
    {
        return new Vector2(
            Random.Range(Home.GetComponent<Collider2D>().bounds.min.x, Home.GetComponent<Collider2D>().bounds.max.x),
            Random.Range(Home.GetComponent<Collider2D>().bounds.min.y, Home.GetComponent<Collider2D>().bounds.max.y)
        );
    }
    private Vector2 RandomInRange()
    {
        Vector2 targetPoint = new Vector2(
                Random.Range(-PathFindDistence, PathFindDistence) + transform.position.x,
                Random.Range(-PathFindDistence, PathFindDistence) + transform.position.y);
        int amount = 0;
        while (inBounds(targetPoint) == false)
        {
           targetPoint = new Vector2(
           Random.Range(-PathFindDistence, PathFindDistence) + transform.position.x,
           Random.Range(-PathFindDistence, PathFindDistence) + transform.position.y);
            amount++;
            if(amount > 10)
            {
                targetPoint = Home.transform.position;
            }
        }
        return targetPoint;
    }
    private bool inBounds(Vector2 targetPoint)
    {
        BoxCollider2D homeZone = Home.gameObject.GetComponent<BoxCollider2D>();
        if (targetPoint.x < homeZone.bounds.max.x && targetPoint.y < homeZone.bounds.max.y && targetPoint.x > homeZone.bounds.min.x && targetPoint.y > homeZone.bounds.min.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
