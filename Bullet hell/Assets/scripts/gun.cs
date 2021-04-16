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
    [SerializeField]
    private int amount;
    [SerializeField]
    private int spred;
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
        if (canShoot)
        {
            canShoot = false;
            Instantiate(projectile, shotPos.transform.position, shotPos.transform.rotation);
            StartCoroutine("coolDown");
        }
    }
    IEnumerator coolDown()
    {
        yield return new WaitForSecondsRealtime(delay);
        canShoot = true;
    }
}
