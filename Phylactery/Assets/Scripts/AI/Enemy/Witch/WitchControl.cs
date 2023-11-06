using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchControl : EnemyControl
{
    private TeleportLocationControl _teleportLocationControl;
    private bool _executeAttackAction = false;
    private bool _executeScreamAction = false;
    private bool _executeTeleportAction = false;
    private bool _isAttackAnimationPlaying = false;
    private bool _isScreamAnimationPlaying = false;
    private bool _isTeleportAnimationPlaying = false;
    private AudioSource _witchAudioSource;
    private Animator _witchAnimator;
    private GameControl _gameControl;
    public bool FindNewRandomSpot = false;

    [SerializeField]
    private float MinSoundDist = 1.0f;
    [SerializeField]
    private float MaxSoundDist = 30.0f;

    protected override void Start()
    {
        base.Start();
        _aiRootNode = new WitchNode(this, null);
        _witchAudioSource = GetComponent<AudioSource>();
        _witchAnimator = GetComponent<Animator>();
        _gameControl = FindObjectOfType<GameControl>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        CheckSoundDistance();
    }

    private void CheckSoundDistance()
    {
        float playerDist = Vector3.Distance(_player.transform.position, transform.position);

        if (_gameControl.IsLevelCompleted)
        {
            _witchAudioSource.volume = 0.0f;
            return;
        }

        if (playerDist < MinSoundDist)
        {
            _witchAudioSource.volume = 1.0f;
        }
        else if (playerDist > MaxSoundDist)
        {
            _witchAudioSource.volume = 0.0f;
        }
        else
        {
            _witchAudioSource.volume = 1.0f - ((playerDist - MinSoundDist) / (MaxSoundDist - MinSoundDist));
        }
    }

    public bool TeleportToRandomSpot()
    {
        if (_isTeleportAnimationPlaying)
        {
            return false;
        }

        if (_executeTeleportAction && !_isTeleportAnimationPlaying)
        {
            FindNewRandomSpot = false;
            _executeTeleportAction = false;
            return true;
        }

        if (!_destination || ReachedDestination())
        {
            TeleportLocationControl newTeleportLocation = TeleportLocationControl.TeleportPoints[Random.Range(0, TeleportLocationControl.TeleportPoints.Count)];

            if (newTeleportLocation == null || newTeleportLocation == _teleportLocationControl)
            {
                FindNewRandomSpot = false;
                return false;
            }

            StartCoroutine(PlayTeleportAnimation());
            FindNewRandomSpot = true;
            transform.position = new Vector3(newTeleportLocation.transform.position.x, newTeleportLocation.transform.position.y, newTeleportLocation.transform.position.z);
            _teleportLocationControl = newTeleportLocation;
        }
        return true;
    }

    public bool FindRandomSpot()
    {
        if (TeleportLocationControl.TeleportPoints.Count == 0)
        {
            return false;
        }

        if (!_destination || ReachedDestination())
        {
            TeleportLocationControl newTeleportLocation = TeleportLocationControl.TeleportPoints[Random.Range(0, TeleportLocationControl.TeleportPoints.Count)];

            if (newTeleportLocation == _teleportLocationControl)
            {
                return false;
            }

            _teleportLocationControl = newTeleportLocation;
            _destination = _teleportLocationControl;
        }
        return true;
    }

    public bool FindLoudSound()
    {
        if (LoudNoiseLocation.LoudNoiseLocations.Count == 0)
        {
            return false;
        }

        LoudNoiseLocation newLoudNoiseLocation = LoudNoiseLocation.LoudNoiseLocations[LoudNoiseLocation.LoudNoiseLocations.Count - 1];
        
        if (_destination == null || _destination != newLoudNoiseLocation)
        {
            if (_destination != null && _destination.DestType == DestinationLocationControl.DestinationType.LOUD_NOISE && _destination != newLoudNoiseLocation)
            {
                ((LoudNoiseLocation)_destination).MarkedForPathfinding = false;
            }

            newLoudNoiseLocation.MarkedForPathfinding = true;
            _destination = newLoudNoiseLocation;
        }

        return true;
    }

    public bool DoAttackAction()
    {
        if (!_executeAttackAction)
        {
            _executeAttackAction = true;
            // Play attack animation
            Debug.Log("Play attack animation");
            StartCoroutine(PlayAttackAnimation());
        }

        return _executeAttackAction && !_isAttackAnimationPlaying;
    }

    public bool DoScreamAction()
    {
        if (!_executeScreamAction)
        {
            _executeScreamAction = true;
            // Play scream animation
            StartCoroutine(PlayScreamAnimation());
        }

        return _executeScreamAction && !_isScreamAnimationPlaying;
    }

    IEnumerator PlayAttackAnimation()
    {
        _isAttackAnimationPlaying = true;
        _player.Die();
        // Play the animation
        _witchAnimator.SetBool("IsAttacking", true);

        yield return new WaitForSeconds(1);

        _gameControl.GameOver();
        _witchAudioSource.volume = 0.0f;
        _isAttackAnimationPlaying = false;
        _witchAnimator.SetBool("IsAttacking", false);
    }

    IEnumerator PlayScreamAnimation()
    {
        _isScreamAnimationPlaying = true;
        // Play the animation
        //_witchAudioSource.loop = false;
        AudioClip audio = Resources.Load<AudioClip>("Borrowed/Sounds/WitchScream");
        _witchAudioSource.clip = audio;
        _witchAudioSource.Play();
        _witchAnimator.SetBool("IsScreaming", true);

        yield return new WaitForSeconds(3);

        _isScreamAnimationPlaying = false;
        //_witchAudioSource.loop = true;
        //audio = Resources.Load<AudioClip>("Borrowed/Sounds/WitchGrowl");
        //_witchAudioSource.clip = audio;
        //_witchAudioSource.Play();
        _witchAnimator.SetBool("IsScreaming", false);
    }

    IEnumerator PlayTeleportAnimation()
    {
        _executeTeleportAction = true;
        _isTeleportAnimationPlaying = true;
        // Play the animation
        _witchAnimator.SetBool("IsTeleporting", true);

        yield return new WaitForSeconds(1);

        _isTeleportAnimationPlaying = false;
        _witchAnimator.SetBool("IsTeleporting", false);
    }

    public void FinishInvestigateNoise()
    {
        if (_destination)
        {
            LoudNoiseLocation.LoudNoiseLocations.Remove((LoudNoiseLocation)_destination);
            Destroy(_destination);
        }
    }

    public void ResetState()
    {
        _executeAttackAction = false;
        _executeScreamAction = false;
        _witchAudioSource.loop = true;
        AudioClip audio = Resources.Load<AudioClip>("Borrowed/Sounds/WitchCry");
        _witchAudioSource.clip = audio;
        _witchAudioSource.Play();
    }

    public override void MoveToDestination(bool running = false, bool usePathfinding = false)
    {
        if (!usePathfinding)
        {
            // Check and rotate AI image
            Vector3 distanceVec = _destination.transform.position - transform.position;

            if (distanceVec.x < 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            distanceVec = new Vector3(distanceVec.x, distanceVec.y, 0.0f); // We don't want the AI to move in depth and mess up each layer depth
            transform.position += _walkingSpeed * Time.deltaTime * distanceVec.normalized;
        }
    }

    public override bool IsInPlayerRange()
    {
        Vector2 playerPos = _player.transform.position;
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);

        if (Vector2.Distance(playerPos, enemyPos) > _playerDetectionDistance)
        {
            return false;
        }

        return true;
    }
}
