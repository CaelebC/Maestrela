using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;

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
        float time = 1f;

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

        while (time < 1f)
        {
            time += Time.deltaTime;
            float a = curve.Evaluate(time);
            img.color = new Color (0f, 0f, 0f, a);
            yield return 0;
        }

        SceneManager.LoadScene(_sceneName);
    }
}
