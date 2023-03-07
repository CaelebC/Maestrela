using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitioner : MonoBehaviour
{
    public SceneFader sceneFader;
    
    public void GoToScene(string _sceneName)
    {
        sceneFader.FadeTo(_sceneName);
    }
}
