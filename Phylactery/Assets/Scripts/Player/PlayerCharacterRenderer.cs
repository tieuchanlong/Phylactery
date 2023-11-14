using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterRenderer : MonoBehaviour
{
    private Animator _animator;
    private int _lastDirection;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _lastDirection = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDirection(Vector2 direction, string[] directionArray)
    {
        _lastDirection = DirectionToIndex(direction, 8);
        _animator.Play(directionArray[_lastDirection]);
    }

    public static int DirectionToIndex(Vector2 dir, int sliceCount)
    {
        Vector2 normDir = dir.normalized;
        float step = 360f / sliceCount;
        float halfstep = step / 2;
        float angle = Vector2.SignedAngle(Vector2.up, normDir);
        angle += halfstep;
        if (angle < 0)
        {
            angle += 360;
        }
        float stepCount = angle / step;
        return Mathf.FloorToInt(stepCount);
    }
}
