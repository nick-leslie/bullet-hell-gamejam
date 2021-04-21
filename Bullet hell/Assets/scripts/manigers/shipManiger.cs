using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class shipManiger : MonoBehaviour
{
    [SerializeField]
    private int amountNeeded;
    private GameObject player;
    private Invantory playerInvatory;
    public Dictionary<string, int> shipInvatory = new Dictionary<string, int>();
    [SerializeField]
    private GameObject shipCanvas;
    [SerializeField]
    private Transform startPos;
    [SerializeField]
    private GameObject resorceTextPrefab;
    private GameObject[] texts;
    [SerializeField]
    private Vector3 offset;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInvatory = player.GetComponent<Invantory>();
        texts = new GameObject[playerInvatory.recorseType.Length];
        for(int i=0;i<playerInvatory.recorseType.Length;i++)
        {
            shipInvatory[playerInvatory.recorseType[i]] = amountNeeded;
            texts[i] = Instantiate(resorceTextPrefab, (startPos.position - (offset * i)) / shipCanvas.GetComponent<Canvas>().scaleFactor, startPos.rotation);
            texts[i].transform.SetParent(shipCanvas.transform);
            texts[i].gameObject.GetComponent<TMP_Text>().text = playerInvatory.recorseType[i] + ":" + shipInvatory[playerInvatory.recorseType[i]];
        }
    }
    public void updateAmount(string recourceName)
    {
        for (int i = 0; i < playerInvatory.recorseType.Length; i++)
        {
            if (recourceName == playerInvatory.recorseType[i])
            {
                shipInvatory[playerInvatory.recorseType[i]] += 1;
                break;
            }
        }
        // update UI
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //show UI
    }
}
