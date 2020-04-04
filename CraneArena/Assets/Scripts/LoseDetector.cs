using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseDetector : MonoBehaviour
{
    private PlayerManager m_Manager = null;
    private void Start()
    {
        //Register PlayerManager
        m_Manager = GetComponentInParent<PlayerManager>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if(!other.GetComponent<LoseZone>()){ return; }
        m_Manager.InLoseZone();

    }
}
