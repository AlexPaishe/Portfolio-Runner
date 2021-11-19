using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformController : MonoBehaviour
{
    public Transform EndPoint;

    void Start()
    {
        WorldController.Instance.OnPlatformMovement += TryDelAndAddPlatform;
    }

    /// <summary>
    /// Уничтожение плотформы, когда она уходит за определенную зону
    /// </summary>
    private void TryDelAndAddPlatform()
    {
        if (transform.position.x < WorldController.Instance.minX)
        {
            WorldController.Instance.worldBuilder.CreatPlatform();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (WorldController.Instance != null)
        {
            WorldController.Instance.OnPlatformMovement -= TryDelAndAddPlatform;
        }
    }
}
