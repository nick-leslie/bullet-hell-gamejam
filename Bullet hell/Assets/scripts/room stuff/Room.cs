using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<GameObject> ThingsInRoom = new List<GameObject>();
    public int MaxAmountOfEnemysForRoom;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Camera.main.GetComponent<CameraControler>().MoveCamera(gameObject.transform);
        }
        ThingsInRoom.Add(collision.gameObject);
        if (collision.gameObject.CompareTag("Player"))
        {
            List<GameObject> enemys = ThingsInRoom.FindAll((GameObject obj) => obj.tag == "enemy");
            for (int e = 0; e < enemys.Count; e++)
            {
                enemys[e].GetComponent<AIbrain>().SwapTarget(collision.gameObject);
            }
        } else if(collision.gameObject.CompareTag("turret"))
        {
            collision.gameObject.GetComponent<turretBrain>().AssignHome(gameObject);
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
