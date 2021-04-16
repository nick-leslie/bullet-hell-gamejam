using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistol : MonoBehaviour
{
    [SerializeField]
    private float delay;
    [SerializeField]
    private GameObject projectile;
    private bool canShoot;
    [SerializeField]
    private Transform shotPos;
    private playerShootingManiger shotingManiger;
    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        shotingManiger = transform.parent.gameObject.GetComponent<playerShootingManiger>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shotingManiger.shootingHeld == true)
        {
            shoot();
        }
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
