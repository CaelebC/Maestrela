using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public SceneFader sceneFader;
    
    public void GoToLevel(string _sceneName)
    {
        sceneFader.FadeTo(_sceneName);
    }
}
