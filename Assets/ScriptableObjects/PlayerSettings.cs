using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    public float speed = 10f;
    public float rotationSpeed = 5f;
    public float headBobAmplitude = 0.5f;
    public float headBobSpeed = 0.1f;
}
