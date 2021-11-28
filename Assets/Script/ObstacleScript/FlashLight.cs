using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private float _tillingStep;

    [SerializeField] private Material [] _mat;
    private float _offsetFirst = 0;
    private float _offsetSecond = 0;

    private void Awake()
    {
        _mat[0].mainTextureOffset = new Vector2(0, _offsetFirst);
        _mat[1].mainTextureOffset = new Vector2(0, _offsetSecond);
    }

    void Update()
    {
        if (_time > 0)
        {
            _time -= Time.deltaTime;
            _offsetFirst -= _tillingStep;
            _mat[0].mainTextureOffset = new Vector2(0, _offsetFirst);
        }
        else
        {
            _offsetFirst -= _tillingStep;
            _offsetSecond -= _tillingStep;
            _mat[0].mainTextureOffset = new Vector2(0, _offsetFirst);
            _mat[1].mainTextureOffset = new Vector2(0, _offsetSecond);
        }
    }
}
