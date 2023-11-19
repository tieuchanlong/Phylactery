using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSoundControl : MonoBehaviour
{
    [SerializeField]
    private AudioClip _footStep1Sound;
    [SerializeField]
    private AudioClip _footStep2Sound;
    private bool _startFootStep = false;

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

    public void PlayFootstepSound(bool running)
    {
        if (_startFootStep)
        {
            return;
        }

        if (running)
        {
            StartCoroutine(PlayRunFootStep());
        }
        else
        {
            StartCoroutine(PlayWalkFootStep());
        }
    }

    IEnumerator PlayWalkFootStep()
    {
        _startFootStep = true;
        _audio.clip = _footStep1Sound;
        _audio.Play();
        yield return new WaitForSeconds(0.4f);
        _audio.clip = _footStep2Sound;
        _audio.Play();
        yield return new WaitForSeconds(0.3f);
        _startFootStep = false;
    }

    IEnumerator PlayRunFootStep()
    {
        _startFootStep = true;
        _audio.clip = _footStep1Sound;
        _audio.Play();
        yield return new WaitForSeconds(0.2f);
        _audio.clip = _footStep2Sound;
        _audio.Play();
        yield return new WaitForSeconds(0.1f);
        _startFootStep = false;
    }
}
