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
    [SerializeField] bool useAngularVelocity = false;
    [SerializeField] float m_CircleRotationSpeed = 30f;
    [Header("Jump Properties")]
    [SerializeField] float m_JumpForce = 20f;

    [Header("GroundChecks")]
    [SerializeField] private GameObject[] groundChecks = null;
    [SerializeField] private float distanceToCheck = 1f;

    private Rigidbody m_RigidbodyTracks = null;
    private Rigidbody m_RigidbodyCircle = null;

    private float forwardValue = 0f;
    private float trackRotationValue = 0f;
    private float circleRotationValue = 0f;

    #region UNITY EVENTS
    // Start is called before the first frame update
    void Start()
    {
        m_RigidbodyTracks = m_TracksToRotate.GetComponent<Rigidbody>();
        m_RigidbodyCircle = m_CircleToRotate.GetComponent<Rigidbody>();
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
        circleRotationValue = value.Get<float>();
    }
    public void OnJump(InputValue value)
    {
        //check grounded 
        //check used jump

        m_RigidbodyTracks.AddForce(Vector3.up * m_JumpForce);
    }
    private void FixedUpdate()
    {
        SetVelocity();
        RotateBody(m_TracksToRotate, m_RigidbodyTracks, trackRotationValue, m_TrackRotationSpeed,useAngularVelocity);
        RotateBody(m_CircleToRotate, m_RigidbodyCircle, circleRotationValue, m_CircleRotationSpeed,false);
    }

    private void SetVelocity()
    {
        //Do ground Checks
        m_RigidbodyTracks.velocity += m_TracksToRotate.transform.forward * forwardValue * m_MovementSpeed * Time.deltaTime;
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

    private void RotateBody(GameObject bodyToRotate, Rigidbody rigidbody, float rotationValue, float rotationspeed, bool useAngular)
    {
        if (useAngular)
        {
            rigidbody.angularVelocity += bodyToRotate.transform.up * rotationValue * rotationspeed * Time.deltaTime;
        }
        else
        {
            var toRotate = rotationValue * rotationspeed * Time.deltaTime;

            bodyToRotate.transform.Rotate(new Vector3(0, toRotate, 0));
        }
    }


    #endregion
}


