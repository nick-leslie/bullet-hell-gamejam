using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
public class animationControler : MonoBehaviour
{
    [SerializeField]
    private string AnimationOnInputString;
    [Header("forgen object")]
    public bool fogenAnimation;
    public GameObject forgenObject;
    private string forgenAnimationString;
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
    public void AnimationOnInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            StartAnimation(AnimationOnInputString);
        } else if(context.phase == InputActionPhase.Canceled)
        {
            EndAnimation(AnimationOnInputString);
        }
    }
    public void EnableForgenObject()
    {
        forgenObject.SetActive(true);
    }
    public void DisableAfterUse()
    {
        gameObject.SetActive(false);
    }
    public void startForgenAnimation(string forgenInput)
    {
        if (forgenObject != null)
        {
            forgenObject.GetComponent<Animator>().SetBool(forgenInput, true);
        }
    }
    public void stopForgenAnimation(string forgenInput)
    {
        if (forgenObject != null)
        {
            forgenObject.GetComponent<Animator>().SetBool(forgenInput, false);
        }
    }
}
