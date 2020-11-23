using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

#pragma warning disable 0649

public class PathFollower : MonoBehaviour
{
    public PathCreator pathCreator;

    void Stuff()
    {
        float progress = 0f;

        Vector3 position = transform.position;
        position.x = pathCreator.path.GetPointAtTime(progress).x;
        position.z = pathCreator.path.GetPointAtTime(progress).z;

        Quaternion rotation = pathCreator.path.GetRotation(progress);
    }
}
