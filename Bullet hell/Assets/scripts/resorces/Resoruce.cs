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
}
