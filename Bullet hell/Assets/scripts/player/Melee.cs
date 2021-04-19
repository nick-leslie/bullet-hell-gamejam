using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Melee : MonoBehaviour
{
    [SerializeField]
    private bool onCoolDown;
    [SerializeField]
    private Vector2 range;
    [SerializeField]
    public int dammage;
    public Transform InteractPoint;
    [SerializeField]
    private string[] thingsToHit;
    // Start is called before the first frame update
    void Start()
    {
        onCoolDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void melee()
    {
        if (onCoolDown == false)
        {
            Collider2D[] entitys = Physics2D.OverlapBoxAll(InteractPoint.position, range, 0);
            for (int i = 0; i < entitys.Length; i++)
            {
                for (int j = 0; j < thingsToHit.Length; j++)
                {
                    if (entitys[i].CompareTag(thingsToHit[j]) == true)
                    {
                        if (entitys[i].gameObject.GetComponent<healthManiger>() != null)
                        {
                            entitys[i].gameObject.GetComponent<healthManiger>().DealDamage(dammage);
                        }
                    }
                }
            }
            //melee animation
            //onCoolDown = true;
        }
    }
    public void StopCoolDown()
    {
        onCoolDown = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(InteractPoint.position, range);
    }
}
