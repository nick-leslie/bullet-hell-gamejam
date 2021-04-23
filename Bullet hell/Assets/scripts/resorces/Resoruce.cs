using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resoruce : MonoBehaviour
{
    [SerializeField]
    private string type;
    [SerializeField]
    private int amount;
    
    public void Pickup(GameObject caller)
    {
       caller.GetComponent<Invantory>().addToRecorseCount(type, amount);
       Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (type == "Money")
            {
                collision.gameObject.GetComponent<Invantory>().addToRecorseCount(type, amount);
                Destroy(gameObject);
            } else if(type == "Health")
            {
                collision.gameObject.GetComponent<healthManiger>().Heal(amount);
                Destroy(gameObject);
            }
        }
    }
}
