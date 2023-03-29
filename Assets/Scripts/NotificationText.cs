using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationText : MonoBehaviour
{
    [SerializeField] float notifDuration = 1.5f;
    [SerializeField] float msgDuration = 3.5f;

    [SerializeField] [TextArea(3, 10)] string higherTierEnemyIncomingMsg;
    [SerializeField] [TextArea(3, 10)] string lastWaveIncomingMsg;
    
    [Header("Unity Setup Fields")]
    [SerializeField] TextMeshProUGUI textObject;
    [SerializeField] Animator TextAnim;


    void Awake() 
    {
        LoadoutManager.GiveNotif += ShowNotif;
        Node.OnBuildError += ShowNotif;
        WaveSpawner.OnNewWave += ShowMsg;
    }

    void OnDestroy()
    {
        LoadoutManager.GiveNotif -= ShowNotif;
        Node.OnBuildError -= ShowNotif;
        WaveSpawner.OnNewWave -= ShowMsg;
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

    void ShowMsg(int _waveIndex)
    {
        if (_waveIndex == 5 || _waveIndex == 12)
        {
            textObject.text = higherTierEnemyIncomingMsg;
            StartCoroutine(FadingMsg());
        }
        if (_waveIndex == 18)
        {
            textObject.text = lastWaveIncomingMsg;
            StartCoroutine(FadingMsg());            
        }
    }
    
    IEnumerator FadingMsg()
    {
        TextAnim.SetBool("TextFade", true);
        TextAnim.SetBool("TextFadeOut", false);

        yield return new WaitForSeconds(msgDuration);

        TextAnim.SetBool("TextFade", false);
        TextAnim.SetBool("TextFadeOut", true);
    }

}
