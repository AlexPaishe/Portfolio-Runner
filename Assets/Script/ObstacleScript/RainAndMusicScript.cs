using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainAndMusicScript : MonoBehaviour
{
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _rain;
    [SerializeField] private AudioClip[] _musicVariations;
    private WeatherController _weather;

    private bool go;
    private int _currentMusic = 0;
    private int _maxMusic;

    private void Awake()
    {
        _weather = FindObjectOfType<WeatherController>();
        go = false;
        _rain.volume = PlayerPrefs.GetFloat("EnvironmentSound");
        _music.volume = PlayerPrefs.GetFloat("MusicSound");
        _maxMusic = _musicVariations.Length;
        _music.Play();
    }

    void Update()
    {
        if(_weather.variation == 1&& go == false)
        {
            _rain.Play();
            go = true;
        }
        else if(_weather.variation !=1)
        {
            _rain.Pause();
            go = false;
        }

        if(_music.isPlaying == false)
        {
            _currentMusic++;
            if(_currentMusic > _maxMusic)
            {
                _currentMusic = 0;
            }
            _music.clip = _musicVariations[_currentMusic];
            _music.Play();
        }
    }
}
