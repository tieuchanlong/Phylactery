using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchControl : EnemyControl
{
    private TeleportLocationControl _teleportLocationControl;

    protected override void Start()
    {
        base.Start();
        _aiRootNode = new WitchNode(this, null);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public bool TeleportToRandomSpot()
    {
        if (TeleportLocationControl.TeleportPoints.Count == 0)
        {
            return false;
        }

        if (!_destination || ReachedDestination())
        {
            TeleportLocationControl newTeleportLocation = TeleportLocationControl.TeleportPoints[Random.Range(0, TeleportLocationControl.TeleportPoints.Count)];

            if (newTeleportLocation == _teleportLocationControl)
            {
                return false;
            }

            transform.position = new Vector3(newTeleportLocation.transform.position.x, newTeleportLocation.transform.position.y, newTeleportLocation.transform.position.z);
            _teleportLocationControl = newTeleportLocation;
        }
        return true;
    }

    public bool FindRandomSpot()
    {
        if (TeleportLocationControl.TeleportPoints.Count == 0)
        {
            return false;
        }

        if (!_destination || ReachedDestination())
        {
            TeleportLocationControl newTeleportLocation = TeleportLocationControl.TeleportPoints[Random.Range(0, TeleportLocationControl.TeleportPoints.Count)];

            if (newTeleportLocation == _teleportLocationControl)
            {
                return false;
            }

            _teleportLocationControl = newTeleportLocation;
            _destination = _teleportLocationControl;
        }
        return true;
    }
}
