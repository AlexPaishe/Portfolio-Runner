using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Vector3 _startDistance;

    private void Awake()
    {
        _startDistance = transform.position - _player.position;
    }

    void Update()
    {
        transform.position = _player.position + _startDistance;
    }
}
