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
    [SerializeField]
    private float offset;
    [SerializeField]
    private float closeDistence;
    [SerializeField]
    private float backPetalveration;
    private GameObject player;
    [SerializeField]
    private GameObject shotPoint;
    private bool facingRight;


    // TODO MAKE I SO THAT IT LOOKS AT TARGET NOT JUST PLAYER



    // Start is called before the first frame update
    void Start()
    {
        brain = gameObject.GetComponent<AIbrain>();
        StartPathfinding();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if (brain.atTarget==true)
        {
            
            StartPathfinding();
        }
        gunTrackPlayer();
        checkForCloseColiders();
        enemyDirection();
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
        Collider2D[] closeBy = Physics2D.OverlapCircleAll(transform.position, closeDistence);
        for(int i=0;i<closeBy.Length;i++)
        {
            if(closeBy[i].CompareTag("Player"))
            {
                point = pointAwayFromPoint(closeBy[i].transform.position);
                brain.changeTargetPos(point);
            }
        }
    }
    private Vector2 pointAwayFromPoint(Vector3 AwayPoint)
    {
        Vector2 DirecltyAway = transform.position - AwayPoint;
        Vector2 newTarget = new Vector2(DirecltyAway.x + Random.Range(-backPetalveration, backPetalveration), DirecltyAway.y + Random.Range(-backPetalveration, backPetalveration));
        return newTarget;
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
        if (targetPoint.x < homeZone.bounds.max.x - offset && targetPoint.y < homeZone.bounds.max.y - offset && targetPoint.x > homeZone.bounds.min.x + offset && targetPoint.y > homeZone.bounds.min.y + offset)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void shoot()
    {
        gameObject.GetComponent<gun>().shoot();
    }
    private void gunTrackPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation
            (shotPoint.transform.position - transform.transform.position, transform.TransformDirection(Vector3.up));
        Quaternion finalRot = new Quaternion(0, 0, rotation.z, rotation.w);
        shotPoint.transform.rotation = finalRot;
        if (!facingRight)
        {
            shotPoint.transform.localRotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            shotPoint.transform.localRotation = new Quaternion(0, 0, 0, 0);
        }
    }
    void enemyDirection()
    {
        // using mousePosition and player's transform (on orthographic camera view)
        var delta = player.transform.position - transform.position;
        if (delta.x >= 0 && !facingRight)
        { // mouse is on right side of player
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // or activate look right some other way
            facingRight = true;
        }
        else if (delta.x < 0 && facingRight)
        { // mouse is on left side
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z); // activate looking left
            facingRight = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, closeDistence);
    }
}
