using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    [SerializeField] private GameObject[] _freePlatforms;
    [SerializeField] private GameObject[] _obstaclePlatforms;
    [SerializeField] private Transform _platformContainer;

    [SerializeField] private float _level;
    [SerializeField] private float _strong;
    [SerializeField] private int _newStep;
    [SerializeField] private float _stepSpeed;

    private int _currentStep = 0;

    private WorldController _world;
    private PhoneController _phone;
    private Transform _lastPlatform = null;

    void Start()
    {
        Init();
        _world = FindObjectOfType<WorldController>();
        _phone = FindObjectOfType<PhoneController>();
    }

    /// <summary>
    /// Создание платформы и ускорение пути
    /// </summary>
    public void CreatPlatform()
    {
        float rand = Random.Range(0, 101);
        if(_level>=rand)
        {
            CreatFreePlatform();
            _level -= _strong;
        }
        else if(_level<rand)
        {
            CreatObstaclePlatform();
            _level -= _strong;
        }
        if(_level<15f)
        {
            _level = 15f;
        }
        _currentStep++;
        if(_currentStep > _newStep)
        {
            _currentStep = 0;
            _world.speed += _stepSpeed;
            _phone.speed += _stepSpeed;
            Debug.Log(_world.speed);
        }
    }

    /// <summary>
    /// Инициализация начальными платформами
    /// </summary>
    public void Init()
    {
        CreatFreePlatform();
        CreatFreePlatform();
        for(int i = 0;i<10;i++)
        {
            CreatPlatform();
        }
    }

    /// <summary>
    /// Создание свободной платформы
    /// </summary>
    private void CreatFreePlatform()
    {
        Vector3 pos = (_lastPlatform == null) ?
            _platformContainer.position :
            _lastPlatform.GetComponent<PlatformController>().EndPoint.position;
        int index = Random.Range(0, _freePlatforms.Length);
        GameObject res = Instantiate(_freePlatforms[index], pos, Quaternion.identity, _platformContainer);
        _lastPlatform = res.transform;
    }

    /// <summary>
    /// Создание платформы с препятствием
    /// </summary>
    private void CreatObstaclePlatform()
    {
        Vector3 pos = (_lastPlatform == null) ?
            _platformContainer.position :
            _lastPlatform.GetComponent<PlatformController>().EndPoint.position;
        int index = Random.Range(0, _obstaclePlatforms.Length);
        GameObject res = Instantiate(_obstaclePlatforms[index], pos, Quaternion.identity, _platformContainer);
        _lastPlatform = res.transform;
    }
}
