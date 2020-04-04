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
    [SerializeField] bool useAngularVelocityForTracks = false;


    [SerializeField] float m_CircleRotationSpeed = 30f;
    [SerializeField] bool useAngularVelocityForCircle = false;


    [Header("Jump Properties")]
    [SerializeField] float m_JumpForce = 20f;

    [Header("GroundChecks")]
    [SerializeField] private List<GameObject> m_GroundChecks = null;
    [SerializeField] private float distanceToCheck = 1f;

    private Rigidbody m_RigidbodyTracks = null;
    private Rigidbody m_RigidbodyCircle = null;

    private float forwardValue = 0f;
    private float trackRotationValue = 0f;
    private float circleRotationValue = 0f;
    private bool bCanJump = true;
    private bool bCanMove = false;

    public bool CanMove { get => bCanMove; set => bCanMove = value; }
    #region UNITY EVENTS
    // Start is called before the first frame update
    void Start()
    {
        //Setup ObjectsToRotate
    }

    internal void SetupComponents()
    {
        m_TracksToRotate = GetComponentInChildren<Tracks>().gameObject;
        m_CircleToRotate = GetComponentInChildren<CraneMainBody>().gameObject;
        m_RigidbodyTracks = m_TracksToRotate.GetComponent<Rigidbody>();
        m_RigidbodyCircle = m_CircleToRotate.GetComponent<Rigidbody>();

        m_GroundChecks.Clear();
        var groundchecks = GetComponentsInChildren<GroundCheck>();

        foreach (var check in groundchecks)
        {
            m_GroundChecks.Add(check.gameObject);
        }

        bCanMove = true;
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
    public void OnRespawn(InputValue value)
    {
        var manager =GetComponent<PlayerManager>();
        if(!manager){ return; }
        manager.Respawn();
    }

    public void OnRotate(InputValue value)
    {
        circleRotationValue = value.Get<float>();
    }
    public void OnJump(InputValue value)
    {
        //check grounded 
        if (!CheckGround() || !bCanJump) { return; }
        //check used jump
        m_RigidbodyTracks.AddForce(Vector3.up * m_JumpForce);
        bCanJump = false;
        StartCoroutine(setJumpAfterTime());
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator setJumpAfterTime()
    {
        yield return new WaitForSeconds(0.5f);
        bCanJump = true;
    }

    private void FixedUpdate()
    {
        if (bCanMove)
        {
            SetVelocity();
            RotateBody(m_TracksToRotate, m_RigidbodyTracks, trackRotationValue, m_TrackRotationSpeed, useAngularVelocityForTracks);
            RotateBody(m_CircleToRotate, m_RigidbodyCircle, circleRotationValue, m_CircleRotationSpeed, useAngularVelocityForCircle);
        }
    }

    private void SetVelocity()
    {
        //Do ground Checks
        if (!CheckGround()) { return; }
        m_RigidbodyTracks.velocity += m_TracksToRotate.transform.forward * forwardValue * m_MovementSpeed * Time.deltaTime;

    }

    private bool CheckGround()
    {
        var onGround = true;

        foreach (var check in m_GroundChecks)
        {
            Debug.DrawLine(check.transform.position, check.transform.position+ check.transform.up * -1 * distanceToCheck,Color.red);
            var hit = Physics.Raycast(check.transform.position, check.transform.up * -1 * distanceToCheck, 1 << LayerMask.NameToLayer("Default"));
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


