using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnimControl
{
    [SerializeField] private float _rotateSpeed = 600;

    private Vector3 _moveDir;

    private PlayerControl _playerControl;

    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }


    public void ParametaSet()
    {
        if (_playerControl.Anim != null)
        {
            _playerControl.Anim.SetFloat("SpeedX", _playerControl.Rb.velocity.x);
            _playerControl.Anim.SetFloat("SpeedY", _playerControl.Rb.velocity.x);
        }
    }

    public void SetSignBoard(bool isHit)
    {
        if (_playerControl.Anim != null)
        {
            _playerControl.Anim.SetBool("IsPaint", isHit);
        }
    }

    public void DirSet()
    {
        bool isSignBoard = _playerControl.SignBoardCheck.CheckSignboard();

        _playerControl.AnimControl.SetSignBoard(isSignBoard);

        if (isSignBoard)
        {
            _playerControl.ModelT.forward = -_playerControl.PlayerT.right;
        }
        else
        {
            if (_playerControl.InputManager.HorizontalMove != 0)
            {
                Vector3 dir = _playerControl.WallCheck.CheckWallToMoveDir(_playerControl.PlayerT.forward);

                if (_playerControl.InputManager.HorizontalMove == -1)
                {
                    dir = -dir;
                }

                _playerControl.ModelT.forward = dir.normalized;
            }
        }



    }



}
