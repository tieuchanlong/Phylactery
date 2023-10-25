using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InLevelTutorialMenuControl : MonoBehaviour
{
    private TextMeshProUGUI _tutorialText;
    private int _tutorialStep = 0;

    // Start is called before the first frame update
    void Start()
    {
        _tutorialText = GetComponentInChildren<TextMeshProUGUI>();
        _tutorialText.text = "Press A, D to move left and right";
    }

    // Update is called once per frame
    void Update()
    {
        if (_tutorialStep == 0)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                _tutorialStep++;
                _tutorialText.text = "Press Space to Jump";
            }
        }
        else if (_tutorialStep == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _tutorialStep++;
                _tutorialText.text = "Press F when light nearly goes out to recharge, but it will create sound";
            }
        }
        else if (_tutorialStep == 2)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
