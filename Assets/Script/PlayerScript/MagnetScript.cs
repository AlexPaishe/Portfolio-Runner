using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem _bonusIndicator;
    public bool go = false;

    void Start()
    {
        _bonusIndicator.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bonus"))
        {
            Destroy(other.gameObject);
            if (go == false)
            {
                StartCoroutine(GoMoney());
            }
        }
    }

    /// <summary>
    /// Создание видимости индикатара двойного сбора монет
    /// </summary>
    /// <returns></returns>
    IEnumerator GoMoney()
    {
        go = true;
        _bonusIndicator.Play();
        yield return new WaitForSeconds(19);
        _bonusIndicator.Stop();
        go = false;
    }
}
