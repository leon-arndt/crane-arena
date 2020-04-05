using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroy everything that collides with this object
/// </summary>
public class DestroyCollided : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.HasStarted) { return; }

        Destroy(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!GameManager.Instance.HasStarted) { return; }

        Destroy(collision.gameObject);
    }
}