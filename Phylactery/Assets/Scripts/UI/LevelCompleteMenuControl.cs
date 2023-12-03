using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteMenuControl : MonoBehaviour
{
    [SerializeField]
    private GameObject _tutorialMenu;

    [SerializeField]
    private GameObject _loadingMenu;

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

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void ReplayLevel()
    {
        _gameControl.ClearGame();

        if (_tutorialMenu)
        {
            _tutorialMenu.SetActive(true);
        }
        gameObject.SetActive(false);
    }

    public void QuitLevel()
    {
        Time.timeScale = 1;
        _loadingMenu.SetActive(true);
        SceneManager.LoadScene(0);
    }
}
