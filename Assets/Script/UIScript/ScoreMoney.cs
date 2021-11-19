using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMoney : MonoBehaviour
{
    [SerializeField] private Text _money;
    [SerializeField] private Text _pause;
    [SerializeField] private Text _death;
    [SerializeField] private Transform _platform;
    [SerializeField] private int _supplement;
    private MagnetScript _bonus;
    private int _count;
    private float _distance;
    private float _bank = 0;
    private float _numb = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Money"))
        {
            int money = other.GetComponent<MoneyScript>().money;
            if(_bonus.go == true)
            {
                money *= 2;
                _count += money;
            }
            else
            {
                _count+=money;
            }
            _money.text = $"Money: {_count} metre {_distance}";
            _pause.text = $"Money: {_count} metre {_distance}";
            _death.text = $"Money: {_count} metre {_distance}";
        }
    }

    private void Awake()
    {
        _bonus = GetComponent<MagnetScript>();
        _count = 0;
        _money.text = $"Money: {_count} metre {_distance}";
        _pause.text = $"Money: {_count} metre {_distance}";
        _death.text = $"Money: {_count} metre { _distance}";
    }

    private void Update()
    {
        _distance = ((_platform.position.x * -1) - 3) / 10;
        _bank = _distance - _numb;
        if(_bank > _supplement)
        {
            _bank = 0;
            _numb += _supplement;
            _count += _supplement;
        }
        _distance = Mathf.Round(_distance);
        _money.text = $"Money: {_count} metre {_distance}";
        _pause.text = $"Money: {_count} metre {_distance}";
        _death.text = $"Money: {_count} metre {_distance}";
    }
}
