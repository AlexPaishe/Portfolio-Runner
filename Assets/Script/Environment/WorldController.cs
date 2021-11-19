using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public float speed = 2.5f;

    public WorldBuilder worldBuilder;

    public float minX = -1;

    public delegate void TryToDelAndAddPlatform();
    public event TryToDelAndAddPlatform OnPlatformMovement;

    public static WorldController Instance;

    private void Awake()
    {
       if(WorldController.Instance != null)
       {
            Destroy(gameObject);
            return; 
       }
        WorldController.Instance = this;
    }

    private void OnDestroy()
    {
        WorldController.Instance = null;
    }
    void Start()
    {
        StartCoroutine(OnPlatformMovementCorutine());
    }

    void Update()
    {
        transform.position -= Vector3.right * speed * Time.deltaTime;
    }

    IEnumerator OnPlatformMovementCorutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            if(OnPlatformMovement != null)
            {
                OnPlatformMovement();
            }
        }
    }
}
