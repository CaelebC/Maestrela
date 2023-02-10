using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "Chapter1_FirstSemester";

    public SceneFader sceneFader;
    
    // Start is called before the first frame update
    public void Play()
    {
        sceneFader.FadeTo(levelToLoad);
        // SceneManager.LoadScene(levelToLoad);
    }

    public void Options()
    {
        Debug.Log("Options Menu");
    }

    public void Quit()
    {
        Debug.Log("Exiting");
        Application.Quit();
    }
}
