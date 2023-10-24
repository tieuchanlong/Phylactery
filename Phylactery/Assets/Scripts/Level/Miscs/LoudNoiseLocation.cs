using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoudNoiseLocation : DestinationLocationControl
{
    public static List<LoudNoiseLocation> LoudNoiseLocations = new List<LoudNoiseLocation>();
    private static int _loudNoiseStackSize = 10;

    [SerializeField]
    private float _maxNoiseLifeSpanInSeconds = 10.0f;

    private float _currentNoiseLifeSpan = 0.0f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        _currentNoiseLifeSpan += Time.deltaTime;

        if (_currentNoiseLifeSpan >= _maxNoiseLifeSpanInSeconds)
        {
            LoudNoiseLocations.Remove(this);
            Destroy(gameObject);
        }
    }

    protected virtual void ClearNoise()
    {
        if (LoudNoiseLocations.Count >= _loudNoiseStackSize)
        {
            // Destroy loud noise when it reaches max stack
            // Though need to manage it through ObjectFactory
            Destroy(LoudNoiseLocations[0].gameObject);
        }
    }
}
