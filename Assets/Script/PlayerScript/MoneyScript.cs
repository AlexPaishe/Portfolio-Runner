using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem _trace;
    public int money;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _trace.Play();
            Destroy(gameObject);
        }
    }
}
