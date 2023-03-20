using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationText : MonoBehaviour
{
    [SerializeField] float notifDuration = 1.5f;
    
    [Header("Unity Setup Fields")]
    [SerializeField] TextMeshProUGUI textObject;
    [SerializeField] Animator TextAnim;


    void Awake() 
    {
        LoadoutManager.GiveNotif += ShowNotif;
        Node.OnBuildError += ShowNotif;
    }

    void OnDestroy()
    {
        LoadoutManager.GiveNotif -= ShowNotif;
        Node.OnBuildError -= ShowNotif;
    }

    void ShowNotif(string _notifContent)
    {
        
        textObject.text = _notifContent;
        StartCoroutine(Fading());
    }

    IEnumerator Fading()
    {
        TextAnim.SetBool("TextFade", true);
        TextAnim.SetBool("TextFadeOut", false);

        yield return new WaitForSeconds(notifDuration);

        TextAnim.SetBool("TextFade", false);
        TextAnim.SetBool("TextFadeOut", true);
    }

}
