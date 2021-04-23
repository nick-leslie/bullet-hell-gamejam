using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpEnemy : MonoBehaviour
{
    [SerializeField]
    private AIbrain brain;
    [Header("General")]
    [SerializeField]
    private float delay;
    [Header("explosion things")]
    public bool isExploder;
    [SerializeField]
    private float explotionRadius;
    [SerializeField]
    private int dammage;
    [Header("tracking stuff")]
    [SerializeField]
    private bool trackingTarget;
    [SerializeField]
    private bool facingRight;
    [SerializeField]
    private GameObject shotPoint;
    [SerializeField]
    private GameObject explotion;
    // Start is called before the first frame update
    void Start()
    {
        brain = gameObject.GetComponent<AIbrain>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (brain.target != null)
        {
            brain.TargetTransform(brain.target.transform);
        }
        if(brain.atTarget==true)
        {
            combat();
        }
        if (brain.target != null)
        {
            enemyDirection();
            trackTarget();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.CompareTag("Player"))
        {
            if (isExploder)
            {
                Explode();
            }
        }
    }
    public void combat()
    {
        if(!isExploder)
        {
            Melee meleeWoker = gameObject.GetComponent<Melee>();
            if(meleeWoker != null)
            {
                if (meleeWoker.onCoolDown == false)
                {
                    StartCoroutine(meleeAttack(meleeWoker));
                }
            }
        } else
        {
            Explode();
        }
    }
    public void Explode()
    {
        StartCoroutine(explosion());
    }
    private IEnumerator meleeAttack(Melee meleeWorker)
    {
        yield return new WaitForSeconds(delay/2);
        meleeWorker.melee();
        yield return new WaitForSeconds(delay);
        meleeWorker.StopCoolDown();
    }
    private IEnumerator explosion()
    {
        //add a bit of a delay
        //use overlap shere here to do thing and stuff
        yield return new WaitForSeconds(delay);
        Collider2D[] entitys = Physics2D.OverlapCircleAll(transform.position, explotionRadius);
        for (int i = 0; i < entitys.Length; i++)
        {
            healthManiger hm = entitys[i].GetComponent<healthManiger>();
            if (hm != null)
            {
                hm.DealDamage(dammage);
            }
        }
        Instantiate(explotion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    private void trackTarget()
    {
        if (trackingTarget)
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
            if (trackingTarget)
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
            if (trackingTarget)
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
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explotionRadius);
    }
}
