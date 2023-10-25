using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerControl : MonoBehaviour
{
    private bool _rechargeFlashlightPressed = false;
    private FlashlightControl _flashlight;

    // Start is called before the first frame update
    void Start()
    {
        _flashlight = GetComponentInChildren<FlashlightControl>();
    }

    // Update is called once per frame
    void Update()
    {
        RechargeFlashlight();
    }

    private void RechargeFlashlight()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_rechargeFlashlightPressed)
        {
            _rechargeFlashlightPressed = true;
            _flashlight.Recharge();

            // Spawn loud sound
            GameObject loudSound = Resources.Load<GameObject>("Borrowed/Prefabs/LoudSound");
            Instantiate(loudSound, transform.position, Quaternion.identity);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _rechargeFlashlightPressed = false;
        }
    }
}
