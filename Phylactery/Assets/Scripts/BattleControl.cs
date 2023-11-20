using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControl : MonoBehaviour
{
    [SerializeField]
    private AudioClip _ambientMusic;

    [SerializeField]
    private AudioClip _combatMusic;

    private AudioSource _audio;
    private bool _playingCombatMusic = true;
    private GameControl _gameControl;
    private PhylacteryPlayerMovement _player;

    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        // Make sure the battle trigger is near player
        _player = FindObjectOfType<PhylacteryPlayerMovement>();
        _gameControl = FindObjectOfType<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAmbience(bool _isInBattle, BattleTriggerControl trigger)
    {
        if (_gameControl.IsLevelCompleted || _player.IsDead)
        {
            _audio.Stop();
            return;
        }

        if (Vector3.Distance(_player.transform.position, trigger.transform.position) > 10.0f)
        {
            return;
        }

        if (_isInBattle != _playingCombatMusic)
        {
            _playingCombatMusic = _isInBattle;

            if (_playingCombatMusic)
            {
                _audio.clip = _combatMusic;
            }
            else
            {
                _audio.clip = _ambientMusic;
            }

            _audio.Play();
        }
    }
}
