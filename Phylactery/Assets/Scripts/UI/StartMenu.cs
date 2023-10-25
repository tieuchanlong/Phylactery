using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _inLevelTutorialMenu;
    private bool isInLevelTutorialMenuActive = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }
    
    public void OnStartButton(){
        Time.timeScale = 1;
    }

    public void OnPauseButton(){
        isInLevelTutorialMenuActive = _inLevelTutorialMenu.activeSelf;
        _inLevelTutorialMenu.SetActive(false);
        Time.timeScale = 0;
    }

    public void OnResumeGame()
    {
        _inLevelTutorialMenu.SetActive(isInLevelTutorialMenuActive);
    }
    
    public void OnQuitButton(){
        Application.Quit();
    }
    
}
