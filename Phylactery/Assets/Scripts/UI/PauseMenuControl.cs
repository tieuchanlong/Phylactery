using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuControl : MonoBehaviour
{
    [SerializeField]
    private Button _resumeBtn;

    [SerializeField]
    private Button _exitBtn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 0.0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
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
