﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneBuilder : MonoBehaviour
{
    public GameObject[] Phone;
    public Transform PlatformContainer;

    private Transform LastPhone = null;

    void Start()
    {
        Init();
    }

    public void CreatPhone()
    {
        Vector3 pos = (LastPhone == null) ?
            PlatformContainer.position :
            LastPhone.GetComponent<Phone>().EndPoint.position;
        int index = Random.Range(0, Phone.Length);
        GameObject res = Instantiate(Phone[index], pos, Quaternion.identity, PlatformContainer);
        LastPhone = res.transform;
    }

    private void Init()
    {
        for (int i = 0; i < 4; i++)
        {
            CreatPhone();
        }
    }
}
