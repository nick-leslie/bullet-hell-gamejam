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
    private GameObject cursor;
    private bool facingRight;
    public bool shootingHeld;
    public GameObject currentGun;
    public bool canShoot=true;
    public bool canMelee = false;
    public Vector2 range;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ChangeGunRotation();
        playerDirection();
        if(shootingHeld)
        {
            if (canShoot)
            {
                currentGun.GetComponent<gun>().shoot();
                cursor.GetComponent<animationControler>().StartAnimation("shoot");
            }
        }
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
    public void Melee(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Melee meleeSystem = currentGun.GetComponent<Melee>();
            if (meleeSystem != null)
            {
                if (canMelee)
                {
                    meleeSystem.melee();
                }
            }
        }
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Collider2D[] entitys = Physics2D.OverlapBoxAll(currentGun.transform.position, range, 0);
            for (int i = 0; i < entitys.Length; i++)
            {
                if (entitys[i].CompareTag("pickup"))
                {
                    if (entitys[i].gameObject.GetComponent<Resoruce>() != null)
                    {
                        entitys[i].gameObject.GetComponent<Resoruce>().Pickup(gameObject);
                    }
                    //TODO MAKE IT SO IT CAN ITERFACE WITH SHIP
                }
            }
        }
    }
    void ChangeGunRotation()
    {
        screenPointRaw = Input.mousePosition;
        screenPoint = new Vector3(screenPointRaw.x, screenPointRaw.y, Camera.main.nearClipPlane);
        Vector3 camPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, 10));
        Vector3 pos = new Vector3(camPos.x, camPos.y, 0);
        cursor.transform.position = pos;
        Quaternion rotation = Quaternion.LookRotation
            (cursor.transform.transform.position - currentGun.transform.position, transform.TransformDirection(Vector3.up));
        Quaternion finalRot = new Quaternion(0, 0, rotation.z, rotation.w);
        currentGun.transform.rotation = finalRot;


        if (!facingRight)
        {
            currentGun.transform.GetChild(0).localRotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            currentGun.transform.GetChild(0).localRotation = new Quaternion(0, 0, 0, 0);
        }

    }
    void playerDirection()
    {
        // using mousePosition and player's transform (on orthographic camera view)
        var delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (delta.x >= 0 && !facingRight)
        { // mouse is on right side of player
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // or activate look right some other way
            facingRight = true;
        }
        else if (delta.x < 0 && facingRight)
        { // mouse is on left side
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z); // activate looking left
            facingRight = false;
        }
    }
}
