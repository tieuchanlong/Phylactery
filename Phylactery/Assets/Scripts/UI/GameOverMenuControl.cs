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
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        // Exit to title
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
