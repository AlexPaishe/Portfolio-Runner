using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneController : MonoBehaviour
{
    public float speed = 10f;

    public PhoneBuilder PhoneBuild;

    public float minX = -1;

    public delegate void TryToDelAndAddPhone();
    public event TryToDelAndAddPhone OnPhoneMovement;

    public static PhoneController Instance;

    private void Awake()
    {
        if (PhoneController.Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        PhoneController.Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        PhoneController.Instance = null;
    }
    void Start()
    {
        StartCoroutine(OnPhoneMovementCorutine());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= Vector3.right * speed * Time.deltaTime;
    }

    IEnumerator OnPhoneMovementCorutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (OnPhoneMovement != null)
            {
                OnPhoneMovement();
            }
        }
    }
}
