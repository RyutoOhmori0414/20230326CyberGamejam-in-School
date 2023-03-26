using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDir
{
    private Vector3 _lookDir;

    private Vector3 _moveDir;

    private float _beforInput = 0;

    public Vector3 MoveDir => _moveDir;

    private PlayerControl _playerControl;
    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
        _lookDir = _playerControl.PlayerT.forward;

        SetDir();
    }

    /// <summary>プレイヤーの進行方向を決める</summary>
    public void SetDir()
    {
        _moveDir = _playerControl.WallCheck.CheckWallToMoveDir(_playerControl.PlayerT.forward);
    }



}
