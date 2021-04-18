using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<GameObject> ThingsInRoom = new List<GameObject>();
    public int MaxAmountOfEnemysForRoom;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ThingsInRoom.Add(collision.gameObject);
        if (collision.gameObject.CompareTag("Player"))
        {
            List<GameObject> enemys = ThingsInRoom.FindAll((GameObject obj) => obj.tag == "enemy");
            for (int e = 0; e < enemys.Count; e++)
            {
                enemys[e].GetComponent<AIbrain>().SwapTarget(collision.gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ThingsInRoom.Remove(collision.gameObject);
        if (collision.gameObject.CompareTag("Player"))
        {
            List<GameObject> enemys = ThingsInRoom.FindAll((GameObject obj) => obj.tag == "enemy");
            for (int e = 0; e < enemys.Count; e++)
            {
                enemys[e].GetComponent<AIbrain>().FindTarget();
            }
        }
    }
}
