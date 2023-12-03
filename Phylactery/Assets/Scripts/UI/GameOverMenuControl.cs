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

    public void RestartGame()
    {
        GameControl gameControl = FindObjectOfType<GameControl>();
        gameControl.RestartGame();
        _loadingMenu.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        // Exit to title
        GameControl gameControl = FindObjectOfType<GameControl>();
        gameControl.ExitLevel();
        _loadingMenu.SetActive(true);
        SceneManager.LoadScene(0);
    }
}
