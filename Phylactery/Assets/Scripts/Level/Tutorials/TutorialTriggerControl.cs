using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggerControl : MonoBehaviour
{
    [SerializeField]
    private GameObject _tutorialMenu;

    protected GameControl _gameControl;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _gameControl = FindObjectOfType<GameControl>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (IsUnlocked())
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual bool IsUnlocked()
    {
        return false;
    }

    public virtual void ActivateTutorial()
    {
        if (!IsUnlocked())
        {
            _tutorialMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
