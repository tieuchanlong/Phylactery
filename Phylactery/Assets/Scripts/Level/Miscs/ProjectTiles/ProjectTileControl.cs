using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTileControl : MonoBehaviour
{
    [SerializeField]
    private float _projecttileLifeTime = 0.0f;

    [SerializeField]
    private float _projectTileSpeed = 1.0f;

    [SerializeField]
    private Vector3 _projectTileDirection;

    private float _currentProjectTileLifeTime = 0.0f;

    public float ProjectTileLifeTime
    {
        set
        {
            _projecttileLifeTime = value;
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

    public float ProjectTileDamage = 0.5f;

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
        _currentProjectTileLifeTime += Time.deltaTime;
        transform.position += _projectTileSpeed * Time.deltaTime * _projectTileDirection;

        if (_currentProjectTileLifeTime >= _projecttileLifeTime)
        {
            Destroy(gameObject);
        }
    }
}
