using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhylacteryTutorialMenuControl : MonoBehaviour
{
    [SerializeField]
    private GameObject _nextTutorialScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 0.0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;

        if (_nextTutorialScreen)
        {
            _nextTutorialScreen.SetActive(true);
        }
    }
}
