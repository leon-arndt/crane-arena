using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMoveForward : MonoBehaviour
{
    [SerializeField] private float m_MovementSpeed = 1f;
    private Rigidbody m_Rigidbody = null;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.velocity = transform.forward * m_MovementSpeed;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
