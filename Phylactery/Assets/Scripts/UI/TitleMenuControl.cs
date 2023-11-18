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
            _pressKeyText.gameObject.SetActive(false);
        }
    }

    public void StartNewGame()
    {
        GameControl gameControl = FindObjectOfType<GameControl>();
        gameControl.SetNewGame(true);
        gameControl.BeginGame();
        SceneManager.LoadScene(2);
    }

    public void ContinueGame()
    {
        GameControl gameControl = FindObjectOfType<GameControl>();
        gameControl.SetNewGame(false);
        gameControl.BeginGame();
        SceneManager.LoadScene(2);
    }
}
