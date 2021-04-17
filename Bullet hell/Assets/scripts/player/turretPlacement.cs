using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class turretPlacement : MonoBehaviour
{
    public List<GameObject> PlacementQ;
    public GameObject placementLoc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlacementQ.Count > 0)
        {
            placementLoc.GetComponent<SpriteRenderer>().sprite = PlacementQ[0].GetComponent<SpriteRenderer>().sprite;
        } else
        {
            placementLoc.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
    public void Place(InputAction.CallbackContext context)
    {
        if (PlacementQ.Count > 0)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                Instantiate(PlacementQ[0], placementLoc.transform.position, placementLoc.transform.rotation);
                PlacementQ.RemoveAt(0);
            }
        }
    }
    public void addToQ(GameObject toBeAdded)
    {
        PlacementQ.Add(toBeAdded);
    }
}
