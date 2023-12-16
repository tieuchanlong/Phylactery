using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromBeneathSpikeControl : MonoBehaviour
{
    public float Damage = 1.0f;
    public bool CanDamagePlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartDamagePlayer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartDamagePlayer()
    {
        yield return new WaitForSeconds(0.2f);
        CanDamagePlayer = true;
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
