using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarControl : MonoBehaviour
{
    private Slider _hpFillBar;

    [SerializeField]
    private Slider _hpPrevFillBar;

    [SerializeField]
    private float _hpBarSpeed = 0.05f;
    private bool _doHPBarAnimation = false;
    private bool _startHPPrevFillAnimation = false;
    private float _newHPPercentage = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        _hpFillBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_startHPPrevFillAnimation)
        {
            // If we rechrage HP, then no need to show yellow bar fill animation
            if (_hpPrevFillBar.value < _hpFillBar.value)
            {
                _hpPrevFillBar.value = _hpFillBar.value;
                _startHPPrevFillAnimation = false;
                _doHPBarAnimation = false;
                return;
            }

            _hpPrevFillBar.value -= _hpBarSpeed * Time.deltaTime;

            if (_hpPrevFillBar.value <= _hpFillBar.value)
            {
                _hpPrevFillBar.value = _hpFillBar.value;
                _startHPPrevFillAnimation = false;
                _doHPBarAnimation = false;
            }
        }
    }

    public void UpdateHP(float hpPercentageChange)
    {
        _hpFillBar.value += hpPercentageChange;
        
        if (!_doHPBarAnimation)
        {
            StartCoroutine(DelayHPPrevFillAnimation());
        }
    }

    IEnumerator DelayHPPrevFillAnimation()
    {
        _doHPBarAnimation = true;
        yield return new WaitForSeconds(1.0f);
        _startHPPrevFillAnimation = true;
    }
}
