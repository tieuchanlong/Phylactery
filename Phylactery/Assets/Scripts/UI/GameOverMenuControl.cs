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
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        // Exit to title
        GameControl gameControl = FindObjectOfType<GameControl>();
        gameControl.ExitLevel();
        SceneManager.LoadScene(0);
    }
}
