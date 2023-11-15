using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasketControl : EnemyFactory
{
    [SerializeField]
    private float _triggerDistance = 2.0f;

    [SerializeField]
    private GameObject _castketOpen;

    private bool _isTriggered = false;

    private PhylacteryPlayerMovement _player;
    private AudioSource _audio;

    // Start is called before the first frame update
    protected override void Start()
    {
        _player = FindObjectOfType<PhylacteryPlayerMovement>();
        _audio = GetComponent<AudioSource>();
        base.Start();
        GetComponent<SpriteRenderer>().enabled = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!_isTriggered)
        {
            if (Vector3.Distance(_player.transform.position, transform.position) <= _triggerDistance)
            {
                _audio.Play();
                _isTriggered = true;
                _castketOpen.SetActive(true);
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        base.Update();
    }

    protected override void SpawnEnemy()
    {
        if (_isTriggered)
        {
            base.SpawnEnemy();
        }
    }
}
