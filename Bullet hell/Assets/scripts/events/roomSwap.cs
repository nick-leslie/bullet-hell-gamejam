using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomSwap : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("grandma");
        if (collision.CompareTag("Player"))
        {
            Camera.main.GetComponent<CameraControler>().MoveCamera(gameObject.transform);
        }
    }
}
