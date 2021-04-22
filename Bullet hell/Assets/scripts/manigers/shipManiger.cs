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
    [SerializeField]
    private int totalLeftNeeded;
    private int totalNeeded;
    [SerializeField]
    private Sprite[] ships;
    private int currentShip;
    public GameObject home;
    private int checkpoint;
    [SerializeField]
    private float closeDistence;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInvatory = player.GetComponent<Invantory>();
        texts = new GameObject[playerInvatory.recorseType.Length];
        currentShip = ships.Length;
        for(int i=0;i<playerInvatory.recorseType.Length-1;i++)
        {
            shipInvatory[playerInvatory.recorseType[i]] = amountNeeded;
            totalLeftNeeded += amountNeeded;
            texts[i] = Instantiate(resorceTextPrefab, (startPos.position - (offset * i)) / shipCanvas.GetComponent<Canvas>().scaleFactor, startPos.rotation);
            texts[i].transform.SetParent(shipCanvas.transform);
            texts[i].gameObject.GetComponent<TMP_Text>().text = playerInvatory.recorseType[i] + ": " + shipInvatory[playerInvatory.recorseType[i]];
        }
        totalNeeded = totalLeftNeeded;
        shipCanvas.SetActive(false);
        home.GetComponent<Room>().ThingsInRoom.Add(gameObject);
        checkpoint = totalNeeded;  
    }
    private void Update()
    {
        if(Vector2.Distance(player.transform.position,gameObject.transform.position) < closeDistence)
        {
            shipCanvas.SetActive(true);
        } else
        {
            shipCanvas.SetActive(false);
        }
    }
    public void updateAmount()
    {
        int zeroCount = 0;
        for (int i = 0; i < playerInvatory.recorseType.Length-1; i++)
        {
            int playerAmount = playerInvatory.Recorces[playerInvatory.recorseType[i]];
            //add in zeroing out protection
            if (shipInvatory[playerInvatory.recorseType[i]] - playerAmount >= 0)
            {
                shipInvatory[playerInvatory.recorseType[i]] -= playerAmount;
                totalLeftNeeded -= playerAmount;
            } else {
                shipInvatory[playerInvatory.recorseType[i]] = playerAmount;
            }
            if(shipInvatory[playerInvatory.recorseType[i]] <=0)
            {
                zeroCount += 1;
            }
            playerInvatory.Recorces[playerInvatory.recorseType[i]] -= playerAmount;
            texts[i].GetComponent<TMP_Text>().text = playerInvatory.recorseType[i] + ": " + shipInvatory[playerInvatory.recorseType[i]];
            player.GetComponent<UImaniger>().updateResorceCount(i);
        }
        //un tested sprite updateing code
        if(totalLeftNeeded < totalNeeded/ships.Length*currentShip-1)
        {
            currentShip -= 1;
            checkpoint = totalLeftNeeded;
            gameObject.GetComponent<SpriteRenderer>().sprite = ships[currentShip-1];
        }
        if (zeroCount >= playerInvatory.recorseType.Length-1)
        {
            Debug.Log("we win these");
            //win condition
        }

    }
    public void DealDammage(int dammage)
    {
        int indexOfChange = Random.Range(0, playerInvatory.recorseType.Length - 1);
        if (totalLeftNeeded + dammage < checkpoint)
        {
            shipInvatory[playerInvatory.recorseType[indexOfChange]] += dammage;
            totalLeftNeeded += dammage;
        }
        texts[indexOfChange].GetComponent<TMP_Text>().text = playerInvatory.recorseType[indexOfChange] + ": " + shipInvatory[playerInvatory.recorseType[indexOfChange]];
    }
}
