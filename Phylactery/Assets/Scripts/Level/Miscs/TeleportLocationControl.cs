using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportLocationControl : DestinationLocationControl
{
    public static List<TeleportLocationControl> TeleportPoints = new List<TeleportLocationControl>();
    private SpriteRenderer _spriteRenderer;

    public TeleportLocationControl()
    {
        
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
        _destinationType = DestinationType.TELEPORT_POINT;
        TeleportPoints.Add(this);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
