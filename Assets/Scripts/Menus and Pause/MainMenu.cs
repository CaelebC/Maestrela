using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string sceneToLoad = "LevelSelect";
    public SceneFader sceneFader;
    
    public void Play()
    {
        sceneFader.FadeTo(sceneToLoad);
    }

    public void Options()
    {
        Debug.Log("Options Menu");
    }

    public void Quit()
    {
        Debug.Log("Exiting");
    }
}
