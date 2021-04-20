using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invantory : MonoBehaviour
{
    [Header("resorces")]
    [SerializeField]
    public string[] recorseType;

    public Dictionary<string, int> Recorces = new Dictionary<string, int>();
    private void Start()
    {
        for(int i=0;i<recorseType.Length;i++)
        {
            Recorces[recorseType[i]] = 0;
        }
        Recorces["Money"] = 69;
    }
    public void addToRecorseCount(string recorse,int amount)
    {
        for (int i = 0; i < recorseType.Length; i++)
        {
            if (recorse == recorseType[i])
            {
                Recorces[recorse] += amount;
                break;
            }
        }
    }
}
