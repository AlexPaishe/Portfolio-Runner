using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMoney : MonoBehaviour
{
    [SerializeField] private Text _money;
    [SerializeField] private Text _pause;
    [SerializeField] private Text _death;
    [SerializeField] private Text _youDied;
    [SerializeField] private Transform _platform;
    [SerializeField] private int _supplement;
    private MagnetScript _bonus;

    public bool theEnd = false;

    private float _distance;
    private float _bank = 0;
    private float _numb = 0;

    private int _count;
    private int _bonusOrb = 0;
    private int _bigMoney = 0;
    private int _bestRecord = 0;

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
        }
        if (other.CompareTag("BigMoney"))
        {
            int money = other.GetComponent<MoneyScript>().money;
            if (_bonus.go == true)
            {
                money *= 2;
                _count += money;
            }
            else
            {
                _count += money;
            }
            _bigMoney++;
        }
        if(other.CompareTag("Bonus"))
        {
            _bonusOrb++;
        }
    }

    private void Awake()
    {
        _bonus = GetComponent<MagnetScript>();
        _count = 0;
        _bestRecord = PlayerPrefs.GetInt("BestRecord");
    }

    private void Update()
    {
        if (theEnd == false)
        {
            _distance = ((_platform.position.x * -1) - 3) / 3;
            _bank = _distance - _numb;
            if (_bank > _supplement)
            {
                _bank = 0;
                _numb += _supplement;
                _count += _supplement;
            }
            _distance = Mathf.Round(_distance);
        }
        else
        {
            if(_bestRecord < _count)
            {
                PlayerPrefs.SetInt("BestRecord", _count);
                _bestRecord = _count;
                _youDied.text = "New Record";
            }
        }
        _money.text = $"Money: {_count} Metre: {_distance}";
        _pause.text = $"Money: {_count} \nMetre: {_distance}\nBigMoney: {_bigMoney}\nBonus: {_bonusOrb}\nRecord: {_bestRecord}";
        _death.text = $"Money: {_count} \nMetre: {_distance}\nBigMoney: {_bigMoney}\nBonus: {_bonusOrb}\nRecord: {_bestRecord}";
    }
}
