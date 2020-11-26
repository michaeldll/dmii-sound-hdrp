#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CustomPathFollower : MonoBehaviour
{
    [SerializeField]
    private PathCreator _pathCreator;

    [SerializeField]
    private float _speed;

    private float progress = 0f;

    void Update()
    {
        progress += _speed * Time.deltaTime;

        Vector3 position = _pathCreator.path.GetPointAtTime(progress);
        Quaternion rotation = _pathCreator.path.GetRotation(progress);

        transform.position = position;
        transform.rotation = rotation;
    }
}
