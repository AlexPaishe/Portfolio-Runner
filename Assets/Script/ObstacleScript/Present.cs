using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour
{
    [SerializeField] private Material[] _mats;
    private MeshRenderer _mesh;
    void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        int first = Random.Range(0, _mats.Length);
        int second = Random.Range(0, _mats.Length);
        if(second == first && second != 0)
        {
            second--;
        }
        else if(second == first && second == 0)
        {
            second++;
        }
        Material[] mats = new Material[] { _mats[first], _mats[second] };
        _mesh.sharedMaterials = mats;
    }
}
