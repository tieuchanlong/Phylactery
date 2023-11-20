using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    private bool _isLevelCompleted = false;

    #region Menu
    private GameObject _levelCompleteMenu;
    private GameObject _gameOverMenu;
    private GameObject _inLevelTutorialMenu;
    private GameObject _pauseMenu;
    private GameObject _saveMenu;
    #endregion

    private GameObject _map;
    private bool _isNewGame = true;
    private bool _gameFirstTime = true;

    public bool IsGameFirstTime
    {
        get
        {
            return _gameFirstTime;
        }
    }

    private Vector3 _playerSpawnPos;

    public bool IsLevelCompleted
    {
        get
        {
            return _isLevelCompleted;
        }
    }

    private bool[] _weaponsUnlocked = { true, false, false };
    private int _savedWeaponSelected = 1;
    private int _savedStoneAmmo = 0;

    public bool IsMovementTutorialUnlocked = false;
    public bool IsHealingCrystalTutorialUnlocked = false;
    public bool IsSlingshotTutorialUnlocked = false;

    private void Awake()
    {
        GameControl[] gameControls = FindObjectsOfType<GameControl>();

        if (gameControls.Length > 1)
        {
            Destroy(gameObject);
        }

        _playerSpawnPos = transform.position;
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.ambientLight = new Color(0.1f, 0.1f, 0.1f);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PhylacteryPlayerMovement player = FindObjectOfType<PhylacteryPlayerMovement>();

        if (player)
        {
            if (_isNewGame || _gameFirstTime)
            {
                PlayerSpawnControl _spawnControl = FindObjectOfType<PlayerSpawnControl>();
                _playerSpawnPos = _spawnControl.transform.position;
                _savedWeaponSelected = 1;
                _savedStoneAmmo = 0;
                IsHealingCrystalTutorialUnlocked = false;
                IsSlingshotTutorialUnlocked = false;
                IsMovementTutorialUnlocked = false;
                _weaponsUnlocked[0] = true;
                _weaponsUnlocked[1] = false;
                _weaponsUnlocked[2] = false;
            }

            player.transform.position = _playerSpawnPos;
            player.WeaponSelected = _savedWeaponSelected;
            player.StoneAmmoCount = _savedStoneAmmo;
        }

        // Find menu
        if (FindObjectOfType<GameOverMenuControl>())
        {
            _gameOverMenu = FindObjectOfType<GameOverMenuControl>().gameObject;
            _gameOverMenu.SetActive(false);
        }

        if (FindObjectOfType<PauseMenuControl>())
        {
            _pauseMenu = FindObjectOfType<PauseMenuControl>().gameObject;
            _pauseMenu.SetActive(false);
        }

        if (FindObjectOfType<SaveGameHUDControl>())
        {
            _saveMenu = FindObjectOfType<SaveGameHUDControl>().gameObject;
            _saveMenu.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void BeginGame()
    {
        _gameFirstTime = false;
//         _map = Instantiate(Resources.Load<GameObject>("Maps/GameJam1Map"));
//         Camera cam = Camera.main;
//         cam.gameObject.SetActive(true);
    }

    public void SetNewGame(bool newGame)
    {
        _isNewGame = newGame;
    }

    public void RestartGame()
    {
        _isLevelCompleted = false;
        Time.timeScale = 1.0f;
    }

    public void SaveGame()
    {
        PhylacteryPlayerMovement player = FindObjectOfType<PhylacteryPlayerMovement>();
        _playerSpawnPos = player.transform.position;
        _isNewGame = false;
        _savedWeaponSelected = player.WeaponSelected;
        _savedStoneAmmo = player.StoneAmmoCount;

        if (_saveMenu)
        {
            _saveMenu.SetActive(true);
        }
    }

    public void ExitLevel()
    {
        _isLevelCompleted = false;
        Time.timeScale = 1.0f;
    }

    public void GameOver()
    {
        _isLevelCompleted = true;

        if (_gameOverMenu)
        {
            _gameOverMenu.SetActive(true);
        }

        if (_inLevelTutorialMenu)
        {
            _inLevelTutorialMenu.SetActive(false);
        }
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

    public void PauseGame()
    {
        _pauseMenu.SetActive(true);
    }

    public bool IsWeaponUnlocked(int weaponIndex)
    {
        return _weaponsUnlocked[weaponIndex - 1];
    }

    public void UnlockWeapon(int weaponIndex)
    {
        _weaponsUnlocked[weaponIndex - 1] = true;
    }
}
