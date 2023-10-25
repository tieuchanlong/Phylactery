using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationLocationControl : MonoBehaviour
{
    public enum DestinationType
    {
        NONE,
        TELEPORT_POINT,
        LOUD_NOISE
    }

    protected DestinationType _destinationType;
    public DestinationType DestType
    {
        get
        {
            return _destinationType;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
