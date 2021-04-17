using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class healthManiger : MonoBehaviour
{
    //TODO ADD UI CODE
    [SerializeField]
    private int MaxHealth;
    public int getMaxHeath 
    { 
        get { return MaxHealth; }    
    }
    [SerializeField]
    private int health;
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            if (value < MaxHealth && value > 0)
            {
                health = value;
            }
        }
    }
    private void Start()
    {
        health = MaxHealth;
    }
    private Transform respawnPoint;
    public void changeSpawn(Transform newSpawn)
    {
        respawnPoint = newSpawn;
    }
    public void DealDamage(int dammage)
    {
        if (health - dammage >= 0)
        {
            health -= dammage;
        }
        if (health <= 0)
        {
            death();
        }
    }
    public void Heal(int amount)
    {
        if (health + amount <= MaxHealth)
        {
            health += amount;
        }
        else
        {
            health = MaxHealth;
        }
    }
    public void death()
    {
        // here is where we would do death animation
        //TODO add code to wait for death animation
        if (gameObject.CompareTag("Player"))
        {
            //gameObject.transform.position = respawnPoint.position;
            health = MaxHealth;
        } else
        {
            Destroy(gameObject);
        }
    }
}
