using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenuControl : MonoBehaviour
{
    private Camera _beginLevelCamera;
    private GameControl _gameControl;

    // Start is called before the first frame update
    void Start()
    {
        _beginLevelCamera = FindAnyObjectByType<Camera>();
        _gameControl = FindObjectOfType<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            gameObject.SetActive(false);
            _beginLevelCamera.gameObject.SetActive(false);
            _gameControl.BeginGame();
        }
    }
}
