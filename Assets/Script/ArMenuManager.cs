using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArMenuManager : MonoBehaviour
{
    public string mainScene;

    void Update()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
    }
  
  
    public void GoToMain()
    {
        SceneManager.LoadScene(mainScene);
    }
}
