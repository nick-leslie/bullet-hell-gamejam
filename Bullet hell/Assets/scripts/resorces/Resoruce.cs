using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resoruce : MonoBehaviour
{
    [SerializeField]
    private string type;
    [SerializeField]
    private int amount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("geting called");
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Invantory>().addToRecorseCount(type, amount);
            Destroy(gameObject);
        }
    }
}
