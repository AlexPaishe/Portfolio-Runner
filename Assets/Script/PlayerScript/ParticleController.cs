using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _boom;
    [SerializeField] private ParticleSystem _starsCenter;
    [SerializeField] private ParticleSystem _starsLeft;
    [SerializeField] private ParticleSystem _starsRight;
    [SerializeField] private ParticleSystem _dustRight;
    [SerializeField] private ParticleSystem _dustLeft;

    private PlayerController _player;
    private bool _stun = false;
    private bool _dust = false;

    private void Awake()
    {
        _player = GetComponent<PlayerController>();
    }

    void Update()
    {
        Duster();
        Boomer();
    }
    
    /// <summary>
    /// Реализация появления звездочек над головой
    /// </summary>
    /// <returns></returns>
    IEnumerator Star()
    {
        yield return new WaitForSeconds(2);
        if (_player.BackDeath == true)
        {
            _starsCenter.Play();
        }
        if (_player.RightDeath == true)
        {
            _starsRight.Play();
        }
        if (_player.LeftDeath == true)
        {
            _starsLeft.Play();
        }
    }

    /// <summary>
    /// Реализация появления пыли под ногами
    /// </summary>
    private void Duster()
    {
        if (_player.RunParticle == true)
        {
            if (_dust == false)
            {
                _dustLeft.Play();
                _dustRight.Play();
                _dust = true;
            }
        }
        else
        {
            _dustLeft.Stop();
            _dustRight.Stop();
            _dust = false;
        }
    }

    /// <summary>
    /// Реализация пояления силовой волны при ударе
    /// </summary>
    private void Boomer()
    {
        if (_player.go == false && _stun == false)
        {
            _boom.Play();
            if (_player.DeadVariation == false)
            {
                StartCoroutine(Star());
            }
            _stun = true;
        }
    }
}
