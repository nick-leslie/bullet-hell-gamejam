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
    // Start is called before the first frame update
    void Start()
    {
        brain = gameObject.GetComponent<turretBrain>();
    }

    // Update is called once per frame
    void Update()
    {
        trackTarget();
        //enemyDirection();
    }
    private void trackTarget()
    {
        if (GunTrackTargert)
        {
            Vector3 dir = brain.target.transform.position - shotPoint.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
}
