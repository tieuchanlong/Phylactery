using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteMenuControl : MonoBehaviour
{
    private GameControl _gameControl;

    // Start is called before the first frame update
    void Start()
    {
        _gameControl = FindObjectOfType<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReplayLevel()
    {
        _gameControl.ClearGame();
        SceneManager.LoadScene(1);
    }

    public void QuitLevel()
    {

    }
}
