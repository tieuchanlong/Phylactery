using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarControl : MonoBehaviour
{
    private Slider _staminaFillBar;

    [SerializeField]
    private Slider _staminaPrevFillBar;

    [SerializeField]
    private float _staminaBarSpeed = 0.05f;
    private bool _doStaminaBarAnimation = false;
    private bool _startStaminaPrevFillAnimation = false;
    private float _newStaminaPercentage = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        _staminaFillBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_startStaminaPrevFillAnimation)
        {
            // If we rechrage Stamina, then no need to show yellow bar fill animation
            if (_staminaPrevFillBar.value < _staminaFillBar.value)
            {
                _staminaPrevFillBar.value = _staminaFillBar.value;
                _startStaminaPrevFillAnimation = false;
                _doStaminaBarAnimation = false;
                return;
            }

            _staminaPrevFillBar.value -= _staminaBarSpeed * Time.deltaTime;

            if (_staminaPrevFillBar.value <= _staminaFillBar.value)
            {
                _staminaPrevFillBar.value = _staminaFillBar.value;
                _startStaminaPrevFillAnimation = false;
                _doStaminaBarAnimation = false;
            }
        }
    }

    public void UpdateStamina(float staminaPercentageChange)
    {
        _staminaFillBar.value += staminaPercentageChange;

        if (!_doStaminaBarAnimation)
        {
            StartCoroutine(DelayStaminaPrevFillAnimation());
        }
    }

    IEnumerator DelayStaminaPrevFillAnimation()
    {
        _doStaminaBarAnimation = true;
        yield return new WaitForSeconds(1.0f);
        _startStaminaPrevFillAnimation = true;
    }
}
