using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioTiggerZone : MonoBehaviour
{
    [SerializeField]
    private int index;
    private GameObject audioManiger;
    private void Start()
    {
        audioManiger = GameObject.FindGameObjectWithTag("audioManiger");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            audioManiger.GetComponent<audioManiger>().PlayBackgroundSong(index);
        }
    }
}
