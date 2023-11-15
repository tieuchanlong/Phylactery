using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveGameHUDControl : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _savingText;

    [SerializeField]
    private float _fadingSpeed = 0.01f;

    [SerializeField]
    private float _fadingPeriod = 2.0f;

    private float _fadingSign = -1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _savingText.alpha += _fadingSign * _fadingSpeed * Time.deltaTime;

        if (_savingText.alpha < 0.0f || _savingText.alpha > 1.0f)
        {
            if (_savingText.alpha < 0.0f)
            {
                _savingText.alpha = 0.0f;
            }

            if (_savingText.alpha > 1.0f)
            {
                _savingText.alpha = 1.0f;
            }

            _fadingSign = -_fadingSign;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(ShowSavingText());
    }

    IEnumerator ShowSavingText()
    {

        yield return new WaitForSeconds(_fadingPeriod);
        gameObject.SetActive(false);
    }
}
