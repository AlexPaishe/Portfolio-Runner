using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Setting : MonoBehaviour
{
    [SerializeField] private Toggle _tog;
    [SerializeField] private GameObject[] _panel;

    private WeatherController _weather;
    private int _number;

    [SerializeField] private AudioSource _click;
    [SerializeField] private AudioMixerGroup[] _mixer;
    [SerializeField] private Slider[] _soundSliders;
    [SerializeField] private string[] _nameSliders;

    private float[] _volume = new float[] { 0, 0, 0 };

    private void Awake()
    {
        _weather = FindObjectOfType<WeatherController>();
        _number = PlayerPrefs.GetInt("Weather");
        if (_number == 0)
        {
            _tog.isOn = false;
        }
        else
        {
            _tog.isOn = true;
        }

        for (int i = 0; i < _volume.Length; i++)
        {
            _volume[i] = PlayerPrefs.GetFloat(_nameSliders[i]);
            _soundSliders[i].value = _volume[i];
            _mixer[i].audioMixer.SetFloat(_nameSliders[i], PlayerPrefs.GetFloat(_nameSliders[i]));
        }
    }

    /// <summary>
    /// Реализация изменения погоды
    /// </summary>
    /// <param name="weather"></param>
    public void ToggleWeather(bool weather)
    {
        if(weather == true)
        {
            PlayerPrefs.SetInt("Weather", 1);
            _weather.variation = 1;
        }
        else
        {
            PlayerPrefs.SetInt("Weather", 0);
            _weather.variation = 0;
        }
    }


    /// <summary>
    /// Включение и выключение меню настроек
    /// </summary>
    public void SettingMenu()
    {
        if (_panel[0].activeSelf)
        {
            _panel[1].SetActive(true);
            _panel[0].SetActive(false);
        }
        else if (_panel[0].activeSelf == false)
        {
            _panel[0].SetActive(true);
            _panel[1].SetActive(false);
        }
    }

    /// <summary>
    /// Реализация изменения громкости музыки
    /// </summary>
    /// <param name="val"></param>
    public void SlidersMusic(float val)
    {
        PlayerPrefs.SetFloat(_nameSliders[0], val);
        _mixer[0].audioMixer.SetFloat(_nameSliders[0], val);
    }

    /// <summary>
    /// Реализация изменения громкости врагов
    /// </summary>
    /// <param name="val"></param>
    public void SlidersItem(float val)
    {
        PlayerPrefs.SetFloat(_nameSliders[2], val);
        _mixer[2].audioMixer.SetFloat(_nameSliders[2], val);
    }

    /// <summary>
    /// Реализация изменения громкости оружия
    /// </summary>
    /// <param name="val"></param>
    public void SlidersEffect(float val)
    {
        PlayerPrefs.SetFloat(_nameSliders[1], val);
        _mixer[1].audioMixer.SetFloat(_nameSliders[1], val);
    }

    /// <summary>
    /// Реализация клика
    /// </summary>
    public void Click()
    {
        _click.Play();
    }
}
