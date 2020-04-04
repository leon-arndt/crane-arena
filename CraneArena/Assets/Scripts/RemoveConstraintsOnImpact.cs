using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Remove constrains on rigidbody on impact
/// </summary>
public class RemoveConstraintsOnImpact : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Invoke("RemoveConstraints", 1f);
    }

    public void RemoveConstraints()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
