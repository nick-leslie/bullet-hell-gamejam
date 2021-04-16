using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 moveDire;
    [SerializeField]
    private float moveSpeed;
    public float moveMod;
    [SerializeField]
    public float buildUpTime;
    [SerializeField]
    private float dashDistence;
    [SerializeField]
    private float dashTime;
    private bool dashing;
    Vector2 targetPos;
    [SerializeField]
    private float closeEnough;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float currentSpeed = moveSpeed * moveMod;

        if (dashing)
        {
            rb.velocity = Vector2.zero;
            transform.position = Vector2.Lerp(transform.position, targetPos, dashTime);
            Debug.DrawLine(transform.position, targetPos);
            if (Vector2.Distance(transform.position,targetPos) <= closeEnough)
            {
                dashing = false;
            }
        }
        else
        {
            //rb.velocity = Vector2.Lerp(rb.velocity, currentSpeed * moveDire * Time.deltaTime, buildUpTime);
            //rb.MovePosition(transform.position + currentSpeed * moveDire);
            //Vector2 MoveTarget = transform.position + moveDire * currentSpeed;
            //RaycastHit2D playerCollsionCheck = Physics2D.Raycast(transform.position,moveDire, currentSpeed);
            //Debug.DrawLine(transform.position, MoveTarget);
            //Debug.Log(playerCollsionCheck.collider.name);
            //rb.velocity += (currentSpeed * moveDire);
            //if (playerCollsionCheck.collider == null)
            //{
                //transform.position = MoveTarget;
            //}
            rb.velocity = currentSpeed * moveDire;
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Canceled)
        {
            Vector2 input =  context.ReadValue<Vector2>();
            moveDire = new Vector3(input.x, input.y, 0).normalized;
        }
        else
        {
            moveDire = Vector3.zero;
        }
    }
    public void Dash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (!dashing)
            {
                //transform.Translate(moveDire * Time.deltaTime * dashDistence);
                dashing = true;
                //rb.velocity = Vector2.zero;
                //rb.velocity = moveDire * dashDistence;
                targetPos = new Vector2(transform.position.x + dashDistence * moveDire.x, transform.position.y + dashDistence * moveDire.y);
                RaycastHit2D dashCollisonCheck = Physics2D.Raycast(transform.position, moveDire, dashDistence);
                if (dashCollisonCheck.collider != null)
                {
                    targetPos = dashCollisonCheck.point;
                }
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        dashing = false;
    }
}
