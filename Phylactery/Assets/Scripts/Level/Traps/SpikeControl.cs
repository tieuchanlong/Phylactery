using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeControl : MonoBehaviour
{
    public float SpikeDamage = 1.0f;
    private float _delayBtwnDamage = 1.0f;
    private bool _isDamagingPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GiveDamage(PhylacteryPlayerMovement player)
    {
        if (!_isDamagingPlayer)
        {
            player.TakeDamage(SpikeDamage);
            StartCoroutine(DoDamageAnimation());
        }
    }

    IEnumerator DoDamageAnimation()
    {
        _isDamagingPlayer = true;
        yield return new WaitForSeconds(_delayBtwnDamage);

        _isDamagingPlayer = false;
    }
}
