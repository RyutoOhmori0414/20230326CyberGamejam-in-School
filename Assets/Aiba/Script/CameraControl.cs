using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[System.Serializable]
public class CameraControl 
{
    private PlayerControl _playerControl;

    private CinemachineImpulseSource _source;
    CinemachineImpulseListener impulseListener;

    Vector3 saveVelo;

    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
        _source = _playerControl.Camera.GetComponent<CinemachineImpulseSource>();
    }

    public void CameraShake()
    {
        _source.GenerateImpulse();
    }
}
