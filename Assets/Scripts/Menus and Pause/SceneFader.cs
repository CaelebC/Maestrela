using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;
    public float fadeTime = 0.5f;

    void Start() 
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string _sceneName)
    {
        StartCoroutine(FadeOut(_sceneName));
    }

    IEnumerator FadeIn()
    {
        float time = fadeTime;

        while (time > 0)
        {
            time -= Time.deltaTime;
            float a = curve.Evaluate(time);
            img.color = new Color (0f, 0f, 0f, a);
            yield return 0;
        }
    }

    IEnumerator FadeOut(string _sceneName)
    {
        float time = 0f;

        while (time < fadeTime)
        {
            time += Time.deltaTime;
            float a = curve.Evaluate(time);
            img.color = new Color (0f, 0f, 0f, a);
            yield return 0;
        }

        SceneManager.LoadScene(_sceneName);
    }
}
