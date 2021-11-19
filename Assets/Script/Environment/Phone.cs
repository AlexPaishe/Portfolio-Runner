using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public Transform EndPoint;
    // Start is called before the first frame update
    void Start()
    {
        PhoneController.Instance.OnPhoneMovement += TryDelAndAddPhone;
    }

    // Update is called once per frame
    void Update()
    {
        
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
