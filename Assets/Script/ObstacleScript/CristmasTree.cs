using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristmasTree : MonoBehaviour
{
    [SerializeField] private Material _mat;
    [SerializeField] private float _timer;
    [SerializeField] private float _tillingStep;
    private float _offset = 0;
    private float _currentTimer;

    private void Awake()
    {
        _currentTimer = _timer;
    }

    void Update()
    {
        _currentTimer -= Time.deltaTime;
        if(_currentTimer<0)
        {
            _currentTimer = _timer;
            _offset += _tillingStep;
            _mat.mainTextureOffset = new Vector2(0, _offset);
        }
    }
}
