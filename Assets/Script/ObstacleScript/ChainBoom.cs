using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBoom : MonoBehaviour
{
    private Rigidbody _rb;
    private WorldController _world;
    [SerializeField] private float _timer;

    void Start()
    {
        _world = FindObjectOfType<WorldController>();
        _rb = GetComponent<Rigidbody>();
        if(_world != null)
        {
            _timer *= (10 / _world.speed);
        }
        StartCoroutine(Boom());
    }

    /// <summary>
    /// Реализация падения буловы
    /// </summary>
    /// <returns></returns>
    IEnumerator Boom()
    {
        yield return new WaitForSeconds(_timer);
        _rb.isKinematic = false;
    }
}
