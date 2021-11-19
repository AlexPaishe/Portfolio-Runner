using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WeatherController : MonoBehaviour
{ 
    [SerializeField] private ParticleSystem _snowOne;
    [SerializeField] private ParticleSystem _snowTwo;
    [SerializeField] private ParticleSystem _snowThree;

    private PlayerController _player;
    private Camera _cam;

    [SerializeField] private int _timer;
    private bool _go = true;
    private int _weather;
    public int variation;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        _cam = GetComponent<Camera>();
        _weather = 1;
        _cam.cullingMask = 1 << 0 | 0 << 8;
        _weather = PlayerPrefs.GetInt("Weather");
    }

    void Update()
    {      
        if(_go == true)
        {
            if (_weather == 1)
            {
                variation = Random.Range(0, 3);
                Switcher(variation);
            }
            else
            {
                variation = 2;
                Switcher(variation);
            }
        }
    }

    /// <summary>
    /// Реализация ливня
    /// </summary>
    /// <returns></returns>
    IEnumerator Rain()
    {
        _go = false;
        _cam.cullingMask = 1<<0|1 << 8;
        _snowOne.Stop();
        _snowTwo.Stop();
        _snowThree.Stop();
        yield return new WaitForSeconds(_timer);
        if (_player.go == false)
        {
            _go = false;
        }
        else
        {
            _go = true;
        }
    }

    /// <summary>
    /// Реализация снежной погоды
    /// </summary>
    /// <returns></returns>
    IEnumerator Snow()
    {
        _go = false;
        _cam.cullingMask = 1<<0|0 << 8;
        _snowOne.Play();
        _snowTwo.Play();
        _snowThree.Play();
        yield return new WaitForSeconds(_timer);
        if (_player.go == false)
        {
            _go = false;
        }
        else
        {
            _go = true;
        }
    }

    /// <summary>
    /// Реализация солнечной погоды
    /// </summary>
    /// <returns></returns>
    IEnumerator Sun()
    {
        _go = false;
        _cam.cullingMask = 1<<0|0 << 8;
        _snowOne.Stop();
        _snowTwo.Stop();
        _snowThree.Stop();
        yield return new WaitForSeconds(_timer);
        if (_player.go == false)
        {
            _go = false;
        }
        else
        {
            _go = true;
        }
    }

    /// <summary>
    /// Реалзация изменения погоды
    /// </summary>
    /// <param name="variation"></param>
    private void Switcher(int variation)
    {
        switch (variation)
        {
            case 0:
                StartCoroutine(Sun());
                break;
            case 1:
                StartCoroutine(Rain());
                break;
            case 2:
                StartCoroutine(Snow());
                break;
        }
    }
}
