using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundDictionary", menuName = "ScriptableObjects/SoundDictionary", order = 1)]
public class SoundDictionary : ScriptableObject
{
    public SoundEvent[] dictionary;
}
