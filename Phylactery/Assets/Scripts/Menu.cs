using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlayButton(){
        //SceneManager.LoadScene();
    }

    public void OnQuitButton(){
        Application.Quit();
    }

    public void OnPauseButton(){
        Time.timeScale = 0;
    }
    
    public void OnResumeButton(){
        Time.timeScale = 1;
    }

}
