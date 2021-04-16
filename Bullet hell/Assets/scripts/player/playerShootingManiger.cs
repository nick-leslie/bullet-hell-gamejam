using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class playerShootingManiger : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector2 screenPointRaw;
    [SerializeField]
    private Transform cursor;
    private bool facingRight;
    public bool shootingHeld;
    [SerializeField]
    private GameObject currentGun;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ChangeGunRotation();
        playerDirection();
    }
    public void shoot(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started || context.phase == InputActionPhase.Performed)
        {
            shootingHeld = true;
        } else
        {
            shootingHeld = false;
        }
    }
    void ChangeGunRotation()
    {
        screenPointRaw = Input.mousePosition;
        screenPoint = new Vector3(screenPointRaw.x, screenPointRaw.y, Camera.main.nearClipPlane);
        Vector3 camPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, 10));
        Vector3 pos = new Vector3(camPos.x, camPos.y, 0);
        cursor.position = pos;
        Quaternion rotation = Quaternion.LookRotation
            (cursor.transform.position - currentGun.transform.position, transform.TransformDirection(Vector3.up));
        currentGun.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
    }
    void playerDirection()
    {
        // using mousePosition and player's transform (on orthographic camera view)
        var delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (delta.x >= 0 && !facingRight)
        { // mouse is on right side of player
            transform.localScale = new Vector3(1, 1, 1); // or activate look right some other way
            facingRight = true;
        }
        else if (delta.x < 0 && facingRight)
        { // mouse is on left side
            transform.localScale = new Vector3(-1, 1, 1); // activate looking left
            facingRight = false;
        }
    }
}
