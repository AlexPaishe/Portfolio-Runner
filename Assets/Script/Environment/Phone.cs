using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public Transform EndPoint;
    [SerializeField] private Transform[] _points;
    [SerializeField] private GameObject[] _building;

    void Start()
    {
        PhoneController.Instance.OnPhoneMovement += TryDelAndAddPhone;
        for(int i = 0; i < _points.Length; i++)
        {
            int rand = Random.Range(0, _building.Length / 2);
            rand += i * (_building.Length/2);
            Instantiate(_building[rand], _points[i].position, Quaternion.identity,this.gameObject.transform);
        }
    }

    private void TryDelAndAddPhone()
    {
        if (transform.position.x < PhoneController.Instance.minX)
        {
            PhoneController.Instance.PhoneBuild.CreatPhone();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (PhoneController.Instance != null)
        {
            PhoneController.Instance.OnPhoneMovement -= TryDelAndAddPhone;
        }
    }
}
