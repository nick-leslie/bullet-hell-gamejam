using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hearts = new GameObject[player.GetComponent<healthManiger>().getMaxHeath];
        HealthCanvus = GameObject.FindGameObjectWithTag("PlayerUI");
        //this should work but if not shit
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = Instantiate(HealthPrefb, (startPos.position + (offset * (i))) / HealthCanvus.GetComponent<Canvas>().scaleFactor, startPos.rotation);
            hearts[i].GetComponent<RectTransform>().SetParent(HealthCanvus.transform);
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
}
