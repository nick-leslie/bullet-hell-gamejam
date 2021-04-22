using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    [SerializeField]
    private bool canShoot;
    [Header("universial data")]
    [SerializeField]
    private float delay;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Transform[] shotPos;
    [Header("shot amount must be odd or else it adds an extra shot")]
    [SerializeField]
    private int amount;
    [SerializeField]
    private int spred;
    [SerializeField]
    private float delayBetweenBurst;
    bool bursting;
    //-----------------animation and sound shit
    private animationControler Ac;
    // Start is called before the first frame update
    private void OnEnable()
    {
        canShoot = true;
        bursting = false;
    }
    void Start()
    {
        canShoot = true;
        bursting = false;
        Ac = gameObject.GetComponent<animationControler>();
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
                for (int j = 0; j < shotPos.Length; j++)
                {
                    for (int i = -startIndex; i <= startIndex; i++)
                    {
                        if (spred > 0)
                        {
                            Instantiate(projectile, shotPos[j].transform.position, shotPos[j].transform.rotation * Quaternion.Euler(0, 0, spred * i));
                        }
                        else
                        {
                            Instantiate(projectile, shotPos[j].transform.position, shotPos[j].transform.rotation);
                        }

                        yield return new WaitForSecondsRealtime(delayBetweenBurst);
                    }
                }
                if (Ac != null)
                {
                    Ac.StartAnimation("Shoot");
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
        if (Ac != null)
        {
            Ac.EndAnimation("Shoot");
        }
    }
}
