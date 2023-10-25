using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    private bool _isLevelCompleted = false;

    [SerializeField]
    private GameObject _levelCompleteMenu;

    [SerializeField]
    private GameObject _gameOverMenu;

    public bool IsLevelCompleted
    {
        get
        {
            return _isLevelCompleted;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.ambientLight = new Color(0.1f, 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginGame()
    {
        Instantiate(Resources.Load<GameObject>("Maps/GameJam1Map"));
    }

    public void GameOver()
    {
        _isLevelCompleted = true;
        _gameOverMenu.SetActive(true);
    }

    public void EndLevel()
    {
        _isLevelCompleted = true;
        _levelCompleteMenu.SetActive(true);
    }

    public void ClearGame()
    {
        foreach (LoudNoiseLocation loudNoise in LoudNoiseLocation.LoudNoiseLocations)
        {
            Destroy(loudNoise);
        }
        LoudNoiseLocation.LoudNoiseLocations.Clear();
    }
}
