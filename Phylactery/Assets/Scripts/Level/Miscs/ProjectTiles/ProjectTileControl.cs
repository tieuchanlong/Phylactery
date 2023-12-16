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

    [SerializeField]
    private int _hp = 1;

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

        get
        {
            return _projectTileSpeed;
        }
    }

    public Vector3 ProjectTileDirection
    {
        set
        {
            _projectTileDirection = value;
        }
    }

    public void SetHP(int newHP)
    {
        _hp = newHP;
    }

    public void ReduceHP()
    {
        _hp--;

        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public float ProjectTileDamage = 0.5f;

    private PhylacteryPlayerMovement _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PhylacteryPlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Travel();
    }

    public virtual void Travel()
    {
        if (tag == "ProjectTile" && _player.IsInAxeAttackRange(transform.position))
        {
            Destroy(gameObject);
        }

        _currentProjectTileLifeTime += Time.deltaTime;
        transform.position += _projectTileSpeed * Time.deltaTime * _projectTileDirection;

        if (_currentProjectTileLifeTime >= _projecttileLifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        
    }
}
