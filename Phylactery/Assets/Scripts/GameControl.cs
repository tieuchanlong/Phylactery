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

    [SerializeField]
    private GameObject _inLevelTutorialMenu;

    private GameObject _map;

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
        _map = Instantiate(Resources.Load<GameObject>("Maps/GameJam1Map"));
        Camera cam = Camera.main;
        cam.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        _isLevelCompleted = true;
        _gameOverMenu.SetActive(true);
        _inLevelTutorialMenu.SetActive(false);
    }

    public void EndLevel()
    {
        _isLevelCompleted = true;
        _levelCompleteMenu.SetActive(true);
        _inLevelTutorialMenu.SetActive(false);
    }

    public void ClearGame()
    {
        _isLevelCompleted = false;
        Destroy(_map);
    }
}
