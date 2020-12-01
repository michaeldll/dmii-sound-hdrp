using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerLookManager : MonoBehaviour
{
    private CinemachineVirtualCamera _cam1;
    private CinemachineVirtualCamera _cam2LookAt;

    public void LookAt(Transform lookAt)
    {
        if (lookAt)
        {
            _cam2LookAt.LookAt = lookAt;
            _cam1.m_Priority = 0;
            _cam2LookAt.m_Priority = 1;
        }

        else
        {
            _cam2LookAt.LookAt = null;
            _cam1.m_Priority = 1;
            _cam2LookAt.m_Priority = 0;
        }
    }

    void Start()
    {
        CinemachineVirtualCamera[] cams;
        cams = GetComponentsInChildren<CinemachineVirtualCamera>();
        foreach (CinemachineVirtualCamera cam in cams)
        {
            switch (cam.tag)
            {
                case "NoFollowCamera":
                    _cam1 = cam;
                    break;

                case "FollowCamera":
                    _cam2LookAt = cam;
                    break;

                default:
                    break;
            }
        }
    }
}
