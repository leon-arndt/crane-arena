using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CraneMovementController : MonoBehaviour
{
    [SerializeField] GameObject m_TracksToRotate = null;
    [SerializeField] GameObject m_CircleToRotate = null;
    [Header("Movement Properties")]
    [SerializeField] float m_MovementSpeed = 20f;
    [SerializeField] float m_TrackRotationSpeed = 40f;
    [SerializeField] float m_CircleRotationSpeed = 30f;
    [SerializeField] bool useAngularVelocity = false;
    [Header("Jump Properties")]
    [SerializeField] float m_JumpForce = 20f;

    [Header("GroundChecks")]
    [SerializeField] private GameObject[] groundChecks = null;
    [SerializeField] private float distanceToCheck = 1f;

    private Rigidbody m_Rigidbody = null;

    private float forwardValue = 0f;
    private float trackRotationValue = 0f;
    private float circleRotationValue = 0f;

    #region UNITY EVENTS
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = m_TracksToRotate.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMove(InputValue value)
    {
        Vector2 trueValue = value.Get<Vector2>();
        forwardValue = trueValue.y;
        trackRotationValue = trueValue.x;
    }

    public void OnRotate(InputValue value)
    {
        float trueValue = value.Get<float>();
        circleRotationValue = trueValue;
    }
    public void OnJump(InputValue value)
    {
        //check grounded 
        //check used jump

        m_Rigidbody.AddForce(Vector3.up * m_JumpForce);
    }
    private void FixedUpdate()
    {
        SetVelocity();
        RotateBody(m_TracksToRotate, trackRotationValue, m_TrackRotationSpeed);
        RotateBody(m_CircleToRotate, circleRotationValue, m_CircleRotationSpeed);
    }

    private void SetVelocity()
    {
        //Do ground Checks
        m_Rigidbody.velocity += m_TracksToRotate.transform.forward * forwardValue * m_MovementSpeed * Time.deltaTime;
        if (CheckGround())
        {
        }
    }

    private bool CheckGround()
    {
        var onGround = true;
        foreach (var check in groundChecks)
        {
            var hit = Physics.Raycast(check.transform.position, Vector3.down * distanceToCheck);
            if (!hit) { onGround = false; break; }

        }
        return onGround;
    }

    private void RotateBody(GameObject bodyToRotate, float rotationValue, float rotationspeed)
    {
        if (useAngularVelocity)
        {
            m_Rigidbody.angularVelocity += bodyToRotate.transform.up * rotationValue * rotationspeed * Time.deltaTime;
        }
        else
        {
            var toRotate = rotationValue * rotationspeed * Time.deltaTime;

            bodyToRotate.transform.Rotate(new Vector3(0, toRotate, 0));
        }
    }


    #endregion
}


