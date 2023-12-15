using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenuControl : MonoBehaviour
{
    [SerializeField]
    private Button _restartBtn;

    [SerializeField]
    private Button _exitBtn;

    [SerializeField]
    private GameObject _loadingMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Time.timeScale = 0.0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        GameControl gameControl = FindObjectOfType<GameControl>();
        gameControl.RestartGame();
        _loadingMenu.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Time.timeScale = 1.0f;
        // Exit to title
        GameControl gameControl = FindObjectOfType<GameControl>();
        gameControl.ExitLevel();
        _loadingMenu.SetActive(true);
        SceneManager.LoadScene(0);
    }
}
