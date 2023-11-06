using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoachmenWagonWheelControl : MonoBehaviour
{
    [SerializeField]
    private float _projectTileSpeed = 1.0f;

    [SerializeField]
    private Vector3 _projectTileDirection;

    [SerializeField]
    private float _stopDist;

    [SerializeField]
    private float _attackRange = 2.0f;

    private Vector3 _destination;
    private int travelPhase = 0;

    public Vector3 Destination
    {
        set
        {
            _destination = value;
        }
    }

    public float ProjectTileSpeed
    {
        set
        {
            _projectTileSpeed = value;
        }
    }

    public Vector3 ProjectTileDirection
    {
        set
        {
            _projectTileDirection = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Travel();
    }

    public virtual void Travel()
    {
        transform.position += _projectTileSpeed * Time.deltaTime * _projectTileDirection;

        if (Vector3.Distance(transform.position, _destination) <= _stopDist)
        {
            if (travelPhase == 0)
            {
                _destination = GetComponentInParent<CoachmenControl>().transform.position;
                _projectTileDirection = -_projectTileDirection;
                travelPhase = 1;
                return;
            }

            if (travelPhase == 1)
            {
                travelPhase = 0;
                gameObject.SetActive(false);
            }
        }
    }
}
