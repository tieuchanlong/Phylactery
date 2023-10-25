using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
    protected BaseNode _aiRootNode;

    [SerializeField]
    protected float _walkingSpeed;

    [SerializeField]
    protected float _rushSpeed;

    [SerializeField]
    protected float _hp;

    public float HP
    {
        get
        {
            return _hp;
        }
    }

    #region Pathfinding variables
    protected DestinationLocationControl _destination;

    [SerializeField]
    protected float _stopDistance;
    #endregion

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Update AI Root node actions
        if (_aiRootNode != null)
        {
            GameControl gameControl = FindAnyObjectByType<GameControl>();

            if (!gameControl.IsLevelCompleted)
            {
                _aiRootNode.Update(Time.deltaTime);
            }
        }
    }

    public virtual void MoveToDestination(bool usePathfinding = false)
    {
        if (!usePathfinding)
        {
            // Check and rotate AI image
            Vector3 distanceVec = _destination.transform.position - transform.position;

            if (distanceVec.x < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            distanceVec = new Vector3(distanceVec.x, distanceVec.y, 0.0f); // We don't want the AI to move in depth and mess up each layer depth
            transform.position += _walkingSpeed * Time.deltaTime * distanceVec.normalized;
        }
    }

    public virtual bool ReachedDestination()
    {
        Vector3 distanceVec = _destination.transform.position - transform.position;
        distanceVec = new Vector3(distanceVec.x, distanceVec.y, 0.0f); // We don't want the AI to move in depth and mess up each layer depth

        return (distanceVec.magnitude <= _stopDistance);
    }
}
