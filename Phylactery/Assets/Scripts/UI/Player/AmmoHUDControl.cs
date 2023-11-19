using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoHUDControl : MonoBehaviour
{
    [SerializeField]
    private Image _axeImage;

    [SerializeField]
    private Image _slingshotImage;

    [SerializeField]
    private Image _ammoImage;

    [SerializeField]
    private TextMeshProUGUI _ammoCountText;

    [SerializeField]
    private AudioClip _itemPickUpSound;
    private AudioSource _audio;

    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeWeapon(int weaponType)
    {
        switch (weaponType)
        {
            case 1:
                _axeImage.gameObject.SetActive(true);
                _slingshotImage.gameObject.SetActive(false);
                break;
            case 2:
                break;
            case 3:
                _axeImage.gameObject.SetActive(false);
                _slingshotImage.gameObject.SetActive(true);
                break;
        }
    }

    public void ShowAmmoHUD(bool showAmmo)
    {
        _ammoImage.gameObject.SetActive(showAmmo);
        _ammoCountText.gameObject.SetActive(showAmmo);
    }

    public void UpdateAmmoCount(int ammoCount)
    {
        _ammoCountText.text = ammoCount.ToString();
    }

    public void PlayAmmoPickUpSound()
    {
        _audio.clip = _itemPickUpSound;
        _audio.Play();
    }
}
