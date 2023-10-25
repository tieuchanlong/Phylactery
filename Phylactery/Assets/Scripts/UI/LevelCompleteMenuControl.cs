using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteMenuControl : MonoBehaviour
{
    [SerializeField]
    private GameObject _tutorialMenu;

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
        _tutorialMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void QuitLevel()
    {
        SceneManager.LoadScene(0);
    }
}
