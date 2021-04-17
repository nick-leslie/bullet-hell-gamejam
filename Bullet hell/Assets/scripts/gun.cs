using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    private bool canShoot;
    [Header("universial data")]
    [SerializeField]
    private float delay;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Transform shotPos;
    [Header("shot amount must be odd or else it adds an extra shot")]
    [SerializeField]
    private int amount;
    [SerializeField]
    private int spred;
    [SerializeField]
    private float delayBetweenBurst;
    bool bursting;
    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void shoot()
    {
        StartCoroutine(shootingLogic());
    }
    IEnumerator shootingLogic()
    {
        if (canShoot)
        {
            if (!bursting)
            {
                canShoot = false;
                //TODO make it so it can handle multiple shots
                int startIndex = Mathf.Max(amount / 2);
                bursting = true;
                for (int i = -startIndex; i <= startIndex; i++)
                {
                    if (spred > 0)
                    {
                        Instantiate(projectile, shotPos.transform.position, shotPos.transform.rotation * Quaternion.Euler(0, 0, spred * i));
                        ;
                    }
                    else
                    {
                        Instantiate(projectile, shotPos.transform.position, shotPos.transform.rotation);
                    }

                    yield return new WaitForSecondsRealtime(delayBetweenBurst);
                }
                StartCoroutine("coolDown");
            }
        }
    }
    IEnumerator coolDown()
    {
        yield return new WaitForSeconds(delay);
        bursting = false;
        canShoot = true;
    }
}
