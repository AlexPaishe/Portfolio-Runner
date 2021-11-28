using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    private AudioSource _sound;
    [SerializeField] private AudioClip _money;
    [SerializeField] private AudioClip _hit;
    [SerializeField] private AudioClip _bonus;

    void Start()
    {
        _sound = GetComponent<AudioSource>();
        _sound.volume = PlayerPrefs.GetFloat("ItemSound");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Money") || other.CompareTag("BigMoney"))
        {
            _sound.clip = _money;
            _sound.Play();
        }
        if(other.CompareTag("Bonus"))
        {
            _sound.clip = _bonus;
            _sound.Play();
        }
        if(other.CompareTag("Danger")||other.CompareTag("DangerLeft")||other.CompareTag("DangerRight"))
        {
            _sound.clip = _hit;
            _sound.Play();
        }
    }
}
