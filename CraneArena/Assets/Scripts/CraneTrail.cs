using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CraneTrail : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void DestroyAfterLifetime()
    {
        var positionConstraint = GetComponent<PositionConstraint>();
        positionConstraint.enabled = false;
        var trailRenderer = GetComponent<TrailRenderer>();
        if(!trailRenderer){ return; }

        StartCoroutine(DestroyAfterTime(trailRenderer.time));
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }
}
