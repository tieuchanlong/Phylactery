using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleControl : MonoBehaviour
{
    [SerializeField]
    private float _interactTriggerTime = 1.0f;

    [SerializeField]
    private bool _canInteractMultipleTimes = false;

    [SerializeField]
    private float _delayBetweenInteraction = 0.1f;

    private float _currentInteractionTime = 0.0f;
    private bool _finishInteracting = false;
    private bool _delayInteraction = false;
    private float _currentDelayInteraction = 0.0f;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!_finishInteracting)
        {
            if (_delayInteraction)
            {
                _currentDelayInteraction += Time.deltaTime;

                if (_currentDelayInteraction >= _delayBetweenInteraction)
                {
                    _currentDelayInteraction = 0.0f;
                    _delayInteraction = false;
                }

                return;
            }

            if (Input.GetKey(KeyCode.E))
            {
                Interact();
            }
            else
            {
                StopInteract();
            }
        }
    }

    protected virtual void Interact()
    {
        _currentInteractionTime += Time.deltaTime;

        if (_currentInteractionTime >= _interactTriggerTime)
        {
            _finishInteracting = !_canInteractMultipleTimes;
            FinishInteraction();
        }
    }

    protected virtual void StopInteract()
    {
        _currentInteractionTime = 0;
    }

    protected virtual void FinishInteraction()
    {
        _delayInteraction = true;
        _currentDelayInteraction = 0.0f;

        // Play animation and give effects/results
    }
}
