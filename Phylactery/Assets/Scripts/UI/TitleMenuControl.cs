using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleMenuControl : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _pressKeyText;
    [SerializeField]
    private float _pressKeyTextFadeSpeed = 2.0f;

    [SerializeField]
    private GameObject _newGameBtn;

    [SerializeField]
    private GameObject _continueBtn;

    [SerializeField]
    private GameObject _creditBtn;

    [SerializeField]
    private GameObject _loadingMenu;

    [SerializeField]
    private GameObject _creditMenu;

    private float _pressKeyTextAlpha = 255.0f;
    private float _pressKeyTextFadeSign = -1.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _pressKeyText.alpha = _pressKeyTextAlpha/255.0f;
        _pressKeyTextAlpha += _pressKeyTextFadeSign * _pressKeyTextFadeSpeed;

        if (_pressKeyTextAlpha < 0 || _pressKeyTextAlpha > 255.0f)
        {
            _pressKeyTextFadeSign = -_pressKeyTextFadeSign;
        }

        if (Input.anyKey)
        {
            GameControl gameControl = FindObjectOfType<GameControl>();
            _continueBtn.SetActive(!gameControl.IsGameFirstTime);
            _newGameBtn.SetActive(true);
            _creditBtn.SetActive(true);
            _pressKeyText.gameObject.SetActive(false);
        }
    }

    public void StartNewGame()
    {
        GameControl gameControl = FindObjectOfType<GameControl>();
        gameControl.SetNewGame(true);
        gameControl.BeginGame();
        _loadingMenu.SetActive(true);
        SceneManager.LoadSceneAsync(1);
    }

    public void ContinueGame()
    {
        GameControl gameControl = FindObjectOfType<GameControl>();
        gameControl.SetNewGame(false);
        gameControl.BeginGame();
        _loadingMenu.SetActive(true);
        SceneManager.LoadSceneAsync(1);
    }

    public void OpenCreditMenu()
    {
        _creditMenu.gameObject.SetActive(true);
    }
}
