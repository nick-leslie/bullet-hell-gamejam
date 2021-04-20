using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class GunManiger : MonoBehaviour
{
    [Serializable]
    public struct Gun
    {
        public string GunName;
        public bool Aquired;
        public bool isGun;
        public GameObject gunObject;  
    }
    public Gun[] guns;
    playerShootingManiger Sm;
    // Start is called before the first frame update
    void Start()
    {
        Sm = gameObject.GetComponent<playerShootingManiger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SwapGun(InputAction.CallbackContext context)
    {
        int keyPressed = Mathf.RoundToInt(context.ReadValue<float>());
        if (context.ReadValue<float>() != 0)
        {
            if(guns[keyPressed-1].Aquired==true)
            {
                //we do be swaping wepons doh
                Sm.currentGun.SetActive(false);
                guns[keyPressed - 1].gunObject.SetActive(true);
                Sm.currentGun = guns[keyPressed - 1].gunObject;
                if(guns[keyPressed - 1].isGun)
                {
                    Sm.canShoot = true;
                    Sm.canMelee = false;
                }  else
                {
                    Sm.canShoot = false;
                    Sm.canMelee = true;
                }
            }
        }
    }
    void ChangeAquiredState(string name)
    {
        for(int i=0;i<guns.Length;i++)
        {
            if(guns[i].GunName == name)
            {
                guns[i].Aquired = true;
                break;
            }
        }
    }
}
