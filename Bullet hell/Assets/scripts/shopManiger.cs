using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class shopManiger : MonoBehaviour
{
    [Serializable]
    public struct Item
    {
        public int price;
        public string name;
        public bool isGun;
        public GameObject turret;
    }
    [SerializeField]
    public Item storeItem;
    private GameObject player;
    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Buy()
    {
        //here is where we check for price TODO 
        Invantory invan = player.GetComponent<Invantory>();
        if (invan != null)
        {
            Debug.Log(invan.Recorces["Money"]);
            if (storeItem.price <= invan.Recorces["Money"])
            {
                invan.Recorces["Money"] -= storeItem.price;
                if (storeItem.isGun == true)
                {
                    player.GetComponent<GunManiger>().ChangeAquiredState(storeItem.name);
                }
                else
                {
                    player.GetComponent<turretPlacement>().addToQ(storeItem.turret);
                }
            }
        }
    }
}
