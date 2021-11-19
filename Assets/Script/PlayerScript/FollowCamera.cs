using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Vector3 _startDistance;
    private Vector3 _moveVec;

    private void Awake()
    {
        _startDistance = transform.position - _target.position;
    }

    void Update()
    {
        _moveVec = _target.position + _startDistance;

        _moveVec.z = 0;
        _moveVec.y = _startDistance.y;

        transform.position = _moveVec;
    }
}
