using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnControl : MonoBehaviour
{
    private SpriteRenderer _sprite;

    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
