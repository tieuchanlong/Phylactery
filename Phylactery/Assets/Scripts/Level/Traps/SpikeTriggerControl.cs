using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTriggerControl : MonoBehaviour
{
    [SerializeField]
    private List<SpikeControl> _spikes;
    [SerializeField]
    private List<GameObject> _spikeGrounds;

    [SerializeField]
    private float _spikesAppearDelay = 2.0f;

    private AudioSource _audio;

    private bool _isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerSpike()
    {
        if (!_isTriggered)
        {
            _isTriggered = true;
            _audio.Play();
            StartCoroutine(DelayShowSpikes());
        }
    }

    IEnumerator DelayShowSpikes()
    {
        yield return new WaitForSeconds(_spikesAppearDelay);

        foreach (GameObject spikeGround in _spikeGrounds)
        {
            spikeGround.SetActive(false);
        }

        foreach (SpikeControl spike in _spikes)
        {
            spike.gameObject.SetActive(true);
        }
    }
}
