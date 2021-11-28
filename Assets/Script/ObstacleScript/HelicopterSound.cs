using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterSound : MonoBehaviour
{
    private AudioSource _helicopter;

    void Start()
    {
        _helicopter = GetComponent<AudioSource>();
        _helicopter.volume = PlayerPrefs.GetFloat("EnvironmentSound");
    }
}
