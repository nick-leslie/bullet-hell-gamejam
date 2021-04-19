using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
public class UImaniger : MonoBehaviour
{
    private GameObject HealthCanvus;
    [SerializeField]
    private GameObject HealthPrefb;
    private GameObject[] hearts;
    [SerializeField]
    private Transform startPos;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Sprite[] spriteAnimations;
    [SerializeField]
    private float TimeBetweenSpriteChage;
    private GameObject player;
    [Header("Shop UI stuff")]
    [SerializeField]
    private GameObject shopUI;
    private bool shopActive = false;
    PauseManiger pm;
    [Header("The number stuff")]
    TimeEvent timeingEvent;
    [SerializeField]
    private TMP_Text THEBIGNUMBER;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pm = GameObject.FindGameObjectWithTag("game maniger").GetComponent<PauseManiger>();
       // hearts = new GameObject[player.GetComponent<healthManiger>().getMaxHeath];
       // HealthCanvus = GameObject.FindGameObjectWithTag("PlayerUI");
       // for (int i = 0; i < hearts.Length; i++)
        //{
            //hearts[i] = Instantiate(HealthPrefb, (startPos.position + (offset * (i))) / HealthCanvus.GetComponent<Canvas>().scaleFactor, startPos.rotation);
            //hearts[i].GetComponent<RectTransform>().SetParent(HealthCanvus.transform);
        //}
        timeingEvent = GameObject.FindGameObjectWithTag("timeManiger").GetComponent<TimeEvent>();
        timeingEvent.onSecondpassed += THENUMBER;
    }
    public void OpenShop(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            changeShopState();
        }
    }
    public void changeShopState()
    {
        if (shopActive == false)
        {
            shopUI.SetActive(true);
            shopActive = true;
            pm.PauseWithoutUI();
        }
        else
        {
            shopUI.SetActive(false);
            shopActive = false;
            pm.unPause();
        }
    }
    IEnumerator HealthAniamtion(float timeDelay, int index, int dire)
    {
        if (dire == 1)
        {
            for (int i = 0; i < spriteAnimations.Length; i++)
            {
                yield return new WaitForSecondsRealtime(timeDelay);
                hearts[index].GetComponent<Image>().sprite = spriteAnimations[i];
            }
        }
        else
        {
            for (int i = spriteAnimations.Length - 1; i >= 0; i--)
            {
                yield return new WaitForSecondsRealtime(timeDelay);
                hearts[index].GetComponent<Image>().sprite = spriteAnimations[i];
            }
        }
    }
    //update the big count down
    void THENUMBER(int theNumber)
    {
        //Debug.Log(THEBIGNUMBER.GetComponent<TextMeshPro>().text);
        THEBIGNUMBER.text = theNumber.ToString();
    }
}
