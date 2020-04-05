using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneMainBody : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        var col = collision.transform.GetComponent<CraneMainBody>();
        if (col == null) return;

        //SoundPlayer.PlayRandomPitch(SoundEventEnum.Collision, 0.5f, 1.5f);
    }
}
