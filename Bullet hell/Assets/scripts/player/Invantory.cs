using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invantory : MonoBehaviour
{
    [Header("resorces")]
    public string[] recorseType;
    [SerializeField]
    private UImaniger uiMainiger;

    public Dictionary<string, int> Recorces = new Dictionary<string, int>();
    private void Start()
    {
        for(int i=0;i<recorseType.Length;i++)
        {
            Recorces[recorseType[i]] = 0;
        }
        uiMainiger = gameObject.GetComponent<UImaniger>();
    }
    public void addToRecorseCount(string recorse,int amount)
    {
        for (int i = 0; i < recorseType.Length; i++)
        {
            if (recorse == recorseType[i])
            {
                Recorces[recorse] += amount;
                uiMainiger.updateResorceCount(i);
                break;
            }
        }
    }
}
