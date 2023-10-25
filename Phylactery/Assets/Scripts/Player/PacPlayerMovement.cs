using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacPlayerMovement : MonoBehaviour
{
    public float speed = 0.0f;
    private Rigidbody2D body;

    private bool _rechargeFlashlightPressed = false;
    private FlashlightControl _flashlight;
    private AudioSource _playerAudioSource;
    private GameControl _gameControl;

    private Animator _playerAnimator;
    private bool _isLanding = false;
    private float _landTimeCount = 0.0f;
    [SerializeField]
    private float MaxLandingTime = 2.0f;

    private bool _isDead = false;
    //private SceneHandler scene;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        _playerAudioSource = GetComponent<AudioSource>();
        _flashlight = GetComponentInChildren<FlashlightControl>();
        _gameControl = FindObjectOfType<GameControl>();
        _playerAnimator = GetComponent<Animator>();
        // access component
    }

    // Update is called once per frame
    void Update()
    {
        GameControl gameControl = FindAnyObjectByType<GameControl>();

        if (gameControl.IsLevelCompleted || _isDead)
        {
            return;
        }

        _playerAnimator.SetBool("IsRunning", false);

        if (Input.GetKeyDown(KeyCode.Space) && body.velocity.y == 0.0f)
        {
            Jump();
        }
        CheckLanding();

        // press D to move right
        if (Input.GetKey(KeyCode.D)) {
            MoveRight();
        }

        // press A to move left
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }

        RechargeFlashlight();
    }

    // Move to the right every frame *per second*
    void MoveRight() {   
            transform.position +=
                new Vector3(speed, 0.0f, 0.0f) * Time.deltaTime;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        if (body.velocity.y == 0)
        {
            _playerAnimator.SetBool("IsRunning", true);
        }
    }

    void MoveLeft() {   
            transform.position +=
                new Vector3(-speed, 0.0f, 0.0f) * Time.deltaTime;
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        if (body.velocity.y == 0)
        {
            _playerAnimator.SetBool("IsRunning", true);
        }
    }

    void MoveUp() {   
            transform.position +=
                new Vector3(0.0f, speed, 0.0f) * Time.deltaTime;
    }

    void MoveDown() {   
            transform.position +=
                new Vector3(0.0f, -speed, 0.0f) * Time.deltaTime;
    }

    void Jump()
    {
        _playerAnimator.SetBool("IsJumping", true);
        body.AddForce(new Vector2(0.0f, 500.0f));
    }

    public void CheckLanding()
    {
        if (body.velocity.y < 0)
        {
            _isLanding = true;
            _landTimeCount += Time.deltaTime;

            _playerAnimator.SetBool("IsJumping", false);
            _playerAnimator.SetBool("IsLanding", true);
        }

        if (body.velocity.y == 0)
        {
            if (_isLanding)
            {
                if (_landTimeCount >= MaxLandingTime)
                {
                    _playerAudioSource.Play();
                    // Spawn loud sound
                    GameObject loudSound = Resources.Load<GameObject>("Borrowed/Prefabs/LoudSound");
                    Instantiate(loudSound, transform.position, Quaternion.identity);
                }

                _isLanding = false;
                _landTimeCount = 0.0f;
            }

            _playerAnimator.SetBool("IsLanding", false);
        }
    }

    private void RechargeFlashlight()
    {
        if (Input.GetKeyDown(KeyCode.F) && !_rechargeFlashlightPressed)
        {
            _rechargeFlashlightPressed = true;
            _flashlight.Recharge();

            // Spawn loud sound
            GameObject loudSound = Resources.Load<GameObject>("Borrowed/Prefabs/LoudSound");
            Instantiate(loudSound, transform.position, Quaternion.identity);
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            _rechargeFlashlightPressed = false;
        }
    }

    public void Die()
    {
        _isDead = true;
        _playerAudioSource.Play();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "EndLevelTrigger")
        {
            _gameControl.EndLevel();
        }
    }
}