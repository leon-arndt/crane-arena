using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CraneMovementController : MonoBehaviour
{
    [SerializeField] GameObject m_BodyToRotate = null;
    [Header("Movement Properties")]
    [SerializeField] float m_MovementSpeed = 20f;
    [SerializeField] float m_RotationSpeed = 40f;
    [SerializeField] bool useAngularVelocity = false;
    private Rigidbody m_Rigidbody = null;

    private float forwardValue = 0f;
    private float rotationValue = 0f;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMove(InputValue value)
    {
        Vector2 trueValue = value.Get<Vector2>();
        forwardValue = trueValue.y;
        rotationValue = trueValue.x;
    }


    private void FixedUpdate()
    {
        m_Rigidbody.velocity += m_BodyToRotate.transform.forward * forwardValue * m_MovementSpeed * Time.deltaTime;


        if (useAngularVelocity)
        {
            m_Rigidbody.angularVelocity += m_BodyToRotate.transform.up * rotationValue * m_RotationSpeed * Time.deltaTime;

        }
        else
        {
            var toRotate = rotationValue * m_RotationSpeed * Time.deltaTime;

            m_BodyToRotate.transform.Rotate(new Vector3(0, toRotate, 0));
        }

    }
}
