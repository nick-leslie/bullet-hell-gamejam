using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class enemySpawnManiger : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> rooms = new List<GameObject>();
    TimeEvent timeingEvents;
    [Range(1, 100)]
    [SerializeField]
    private int SpawnChance;
    [SerializeField]
    private GameObject[] easyEnemys;
    [SerializeField]
    private GameObject[] MidEnemys;
    [SerializeField]
    private GameObject[] HardEnemys;
    public int CurrentDificulty;
    // Start is called before the first frame update
    void Start()
    {
        timeingEvents = GameObject.FindGameObjectWithTag("timeManiger").GetComponent<TimeEvent>();
        timeingEvents.onSecondpassed += spawn;
        rooms = GameObject.FindGameObjectsWithTag("room").ToList();
    }
    void spawn(int seconds)
    {
        if(seconds<=0)
        {
            for (int i = 0; i < rooms.Count; i++) {
                Room CurrentRoom = rooms[i].GetComponent<Room>();
                int enemyAmount = CurrentRoom.ThingsInRoom.FindAll((GameObject obj) => obj.tag == "enemy").Count;
                Debug.Log(enemyAmount);
                if (enemyAmount < CurrentRoom.MaxAmountOfEnemysForRoom)
                {
                    int chance = Mathf.RoundToInt(Random.Range(1, 100));
                    if(SpawnChance < chance)
                    {
                        int amount = Random.Range(1, (CurrentRoom.MaxAmountOfEnemysForRoom - enemyAmount)+1);
                        for(int e=0;e<amount;e++)
                        {
                            int SpawnDifculty = Mathf.RoundToInt(Random.Range(1, CurrentDificulty));
                            int enemyIndex = 0;
                            switch (SpawnDifculty)
                            {
                                case 1:
                                    enemyIndex = Random.Range(0, easyEnemys.Length);
                                    enemyFactory(easyEnemys[enemyIndex],rooms[i]);
                                    break;
                                case 2:
                                    enemyIndex = Random.Range(0, MidEnemys.Length);
                                    enemyFactory(MidEnemys[enemyIndex],rooms[i]);
                                    break;
                                case 3:
                                    enemyIndex = Random.Range(0, HardEnemys.Length);
                                    enemyFactory(HardEnemys[enemyIndex],rooms[i]);
                                    break;
                            }
                        }
                    }
                } else
                {
                    Debug.Log("fallout on enemy amount");
                }
            }
        }
    }
    private Vector2 RandomInRoom(GameObject room)
    {
        return new Vector2(
            Random.Range(room.GetComponent<Collider2D>().bounds.min.x, room.GetComponent<Collider2D>().bounds.max.x),
            Random.Range(room.GetComponent<Collider2D>().bounds.min.y, room.GetComponent<Collider2D>().bounds.max.y)
        );
    }
    void enemyFactory(GameObject enemyPrefab,GameObject room)
    {
       GameObject enemy = Instantiate(enemyPrefab, RandomInRoom(room), room.transform.rotation);
       enemy.GetComponent<AIbrain>().home = room;
       enemy.GetComponent<AIbrain>().FindTarget();
    }
}
