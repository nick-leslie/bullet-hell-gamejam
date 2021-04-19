using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField]
    public float movmentSpeed;
    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private float distence;
    [SerializeField]
    private string[] tagsToHit;
    [SerializeField]
    private int dammage;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("die", lifeTime);  
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, transform.right, distence);
        if (hitinfo.collider != null)
        {
            for(int i=0; i<tagsToHit.Length;i++)
            {
                if(hitinfo.collider.CompareTag(tagsToHit[i]))
                {
                    healthManiger hm = hitinfo.collider.gameObject.GetComponent<healthManiger>();
                    if (hm != null)
                    {
                        if (!hitinfo.collider.CompareTag("Player"))
                        {
                            hm.DealDamage(dammage);
                        }
                    }
                    die();
                }
            }
        }
        transform.Translate(Vector2.right * movmentSpeed * Time.deltaTime);
    }
    void die()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < tagsToHit.Length; i++)
        {
            if (collision.gameObject.CompareTag(tagsToHit[i]))
            {
                healthManiger hm = collision.gameObject.GetComponent<healthManiger>();
                if (hm != null)
                {
                    hm.DealDamage(dammage);
                }
                die();
            }
        }
    }
}
