using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBoom : MonoBehaviour
{
    private Rigidbody _rb;
    private WorldController _world;
    [SerializeField] private MeshRenderer[] _mesh;
    [SerializeField] private float _timer;

    void Start()
    {
        _world = FindObjectOfType<WorldController>();
        _rb = GetComponent<Rigidbody>();
        if (_world != null)
        {
            _timer *= (10 / _world.speed);
        }
        StartCoroutine(Boom());
    }

    IEnumerator Boom()
    {
        yield return new WaitForSeconds(_timer);
        _rb.isKinematic = false;
        for(int i = 0; i < _mesh.Length; i++)
        {
            _mesh[i].enabled = true;
        }
    }
}
