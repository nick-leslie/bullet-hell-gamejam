using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretAim : MonoBehaviour
{
    [SerializeField]
    private GameObject shotPoint;
    [SerializeField]
    private bool facingRight;
    [SerializeField]
    private bool GunTrackTargert;
    private turretBrain brain;
    private gun turretGun;
    // Start is called before the first frame update
    void Start()
    {
        brain = gameObject.GetComponent<turretBrain>();
        Debug.Log(brain);
        turretGun = gameObject.GetComponent<gun>();
    }

    // Update is called once per frame
    void Update()
    {
        if(brain.target != null)
        {
            trackTarget();
            enemyDirection();
            turretGun.shoot();
        }
    }
    private void trackTarget()
    {
        if (GunTrackTargert)
        {
            Vector3 dir = brain.target.transform.position - shotPoint.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            shotPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            if (dir.x >= 0 && !facingRight)
            {

                shotPoint.transform.localScale = new Vector3(shotPoint.transform.localScale.x * -1, shotPoint.transform.localScale.y, shotPoint.transform.localScale.z); // activate looking left
            }
            else if (dir.x < 0 && facingRight)
            {
                shotPoint.transform.localScale = new Vector3(Mathf.Abs(shotPoint.transform.localScale.x), shotPoint.transform.localScale.y, shotPoint.transform.localScale.z); // or activate look right some other way
            }
        }
    }
    void enemyDirection()
    {
        // using mousePosition and player's transform (on orthographic camera view)
        var delta = brain.target.transform.position - transform.position;
        if (delta.x >= 0 && !facingRight)
        {
            if (GunTrackTargert)
            {
                // mouse is on right side of player
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // or activate look right some other way
                facingRight = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                facingRight = true;
            }
        }
        else if (delta.x < 0 && facingRight)
        {
            if (GunTrackTargert)
            {
                // mouse is on left side
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z); // activate looking left
                facingRight = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                facingRight = false;
            }
        }
    }
}
