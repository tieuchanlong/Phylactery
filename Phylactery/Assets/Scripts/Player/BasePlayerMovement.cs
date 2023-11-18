using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerMovement : MonoBehaviour
{
    [SerializeField]
    protected float _speed = 0.0f;
    private Rigidbody2D _body;

    private Animator _playerAnimator;
    private AudioSource _playerAudioSource;
    protected GameControl _gameControl;

    [SerializeField]
    protected float _maxHP;
    protected float _currentHP;

    [SerializeField]
    protected bool _isDamaged = false;
    protected float _damageInviciblePeriod = 0.5f;
    protected float _currentDamageInviciblePeriod = 0.0f;

    public float MaxHP
    {
        get
        {
            return _maxHP;
        }
    }

    protected bool _isDead = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _playerAudioSource = GetComponent<AudioSource>();
        _gameControl = FindObjectOfType<GameControl>();
        _playerAnimator = GetComponent<Animator>();
        _currentHP = _maxHP;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_gameControl.IsLevelCompleted || _isDead)
        {
            return;
        }

        // press D to move right
        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }

        // press A to move left
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }

        // press W to move up
        if (Input.GetKey(KeyCode.W))
        {
            MoveUp();
        }

        // press A to move left
        if (Input.GetKey(KeyCode.S))
        {
            MoveDown();
        }

        // WHen damage, have iframe period
        if (_isDamaged)
        {
            _currentDamageInviciblePeriod += Time.deltaTime;

            if (_currentDamageInviciblePeriod >= _damageInviciblePeriod)
            {
                _isDamaged = false;
                _currentDamageInviciblePeriod = 0;
            }
        }
    }

    // Move to the right every frame *per second*
    public virtual void MoveRight()
    {
        transform.position +=
            new Vector3(_speed, 0.0f, 0.0f) * Time.deltaTime;
        //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    public virtual void MoveLeft()
    {
        transform.position +=
            new Vector3(-_speed, 0.0f, 0.0f) * Time.deltaTime;
        //transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    public virtual void MoveUp()
    {
        transform.position +=
            new Vector3(0.0f, _speed, 0.0f) * Time.deltaTime;
    }

    public virtual void MoveDown()
    {
        transform.position +=
            new Vector3(0.0f, -_speed, 0.0f) * Time.deltaTime;
    }

    public virtual void Die()
    {
        _isDead = true;
    }

    public virtual void TakeDamage(float damageAmount)
    {
        if (!_isDamaged)
        {
            _currentHP -= damageAmount;
            _isDamaged = true;

            if (_currentHP <= 0)
            {
                _isDead = true;
            }
        }
    }

    public virtual void AddHP(float hpAmount)
    {
        if (!_isDamaged)
        {
            _currentHP += hpAmount;

            if (_currentHP > _maxHP)
            {
                _currentHP = _maxHP;
            }
        }
    }
}
