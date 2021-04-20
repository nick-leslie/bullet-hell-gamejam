using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class healthManiger : MonoBehaviour
{
    //TODO ADD UI CODE
    [SerializeField]
    private int MaxHealth;
    public bool Invincibal = false;


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
    [SerializeField]
    private GameObject DeathUI;
    private PauseManiger PM;
    [Header("enemy garbage")]
    [SerializeField]
    private GameObject[] dropList;
    [SerializeField]
    [Range(0, 100)]
    private int DropChance;
    private void Start()
    {
        health = MaxHealth;
        PM = GameObject.FindGameObjectWithTag("game maniger").GetComponent<PauseManiger>();
    }
    private Transform respawnPoint;
    public void changeSpawn(Transform newSpawn)
    {
        respawnPoint = newSpawn;
    }
    public void DealDamage(int dammage)
    {
        if (Invincibal == false)
        {
            if (gameObject.GetComponent<animationControler>() != null)
            {
                gameObject.GetComponent<animationControler>().StartAnimation("hurt");
            }
            if(gameObject.CompareTag("Player"))
            {
                gameObject.GetComponent<UImaniger>().DoHealthAnimation(health-1, 1);
            }
            if (health - dammage >= 0)
            {
                health -= dammage;
                
            }
            if (health <= 0)
            {
                death();
            }
        }
    }
    public void Heal(int amount)
    {
        if (health + amount <= MaxHealth)
        {
            health += amount;
            if (gameObject.CompareTag("Player"))
            {
                gameObject.GetComponent<UImaniger>().DoHealthAnimation(health-1, -1);
            }
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
            //health = MaxHealth;
            //Destroy(gameObject);
            if (DeathUI != null)
            {
                PM.PauseWithoutUI();
                DeathUI.SetActive(true);
            }
        } else
        {
            RandomDrop();
            Destroy(gameObject);
        }
    }
    void RandomDrop()
    {
        if (dropList.Length > 0)
        {
            //random drop code
            int chanse = Mathf.RoundToInt(Random.Range(0, 100));
            if (chanse > DropChance)
            {
                int randomDrop = Mathf.RoundToInt(Random.Range(0, dropList.Length));
                Instantiate(dropList[randomDrop], gameObject.transform.position, gameObject.transform.rotation);
            }
        }
    }
}
