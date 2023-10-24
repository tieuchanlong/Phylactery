using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightControl : MonoBehaviour
{
    private UnityEngine.Rendering.Universal.Light2D _lightComp;

    [SerializeField]
    private float _flashLightLifeTime;
    [SerializeField]
    private float _flashLightMaxIntensity;
    [SerializeField]
    private float _flashLightMinIntensity;

    [SerializeField]
    private float _flashLightRechargeIntensity;

    // Start is called before the first frame update
    void Start()
    {
        _lightComp = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        _lightComp.intensity = _flashLightMaxIntensity;
        _flashLightMinIntensity = Mathf.Max(_flashLightMinIntensity, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Dim();
    }

    private void Dim()
    {
        _lightComp.intensity -= Time.deltaTime * (_flashLightMaxIntensity - _flashLightMinIntensity) / _flashLightLifeTime;

        if (_lightComp.intensity <= _flashLightMinIntensity)
        {
            _lightComp.intensity = _flashLightMinIntensity;
        }
    }

    public void Recharge()
    {
        _lightComp.intensity += _flashLightRechargeIntensity;

        if (_lightComp.intensity >= _flashLightMaxIntensity)
        {
            _lightComp.intensity = _flashLightMaxIntensity;
        }
    }
}
