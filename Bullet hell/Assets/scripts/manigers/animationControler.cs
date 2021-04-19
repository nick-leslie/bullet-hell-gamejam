using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class animationControler : MonoBehaviour
{
    public void DoTrigger(string trigger)
    {
        gameObject.GetComponent<Animator>().SetTrigger(trigger);
    }
    public void StopTrigger(string triggrer)
    {
        gameObject.GetComponent<Animator>().ResetTrigger(triggrer);
    }
    public void StartAnimation(string name)
    {
        gameObject.GetComponent<Animator>().SetBool(name, true);
    }
    public void EndAnimation(string name)
    {
        gameObject.GetComponent<Animator>().SetBool(name, false);
    }
    public void setBool(string name,bool state)
    {
        gameObject.GetComponent<Animator>().SetBool(name,state);
    }
}