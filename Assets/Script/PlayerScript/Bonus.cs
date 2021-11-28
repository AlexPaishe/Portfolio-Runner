using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private GameObject _bonus;
    [SerializeField] private int _max;

    private int _percent = 101;

    void Start()
    {
        int rand = Random.Range(0, _percent);
        if(rand < _max)
        {
            _bonus.SetActive(true);
        }
    }
}
