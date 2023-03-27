using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnimControl
{
    [SerializeField] private float _rotateSpeed = 600;

    private Vector3 _moveDir;

    private PlayerControl _playerControl;

    private bool _isGettingBoard = false;

    public bool IsGettingBoard => _isGettingBoard;

    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }


    public void ParametaSet()
    {
        if (_playerControl.Anim != null)
        {
            _playerControl.Anim.SetFloat("SpeedX", _playerControl.Rb.velocity.magnitude);
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

    public void DamageAnim(bool isDamage)
    {
        _playerControl.Anim.SetBool("IsDamage",isDamage);
    }

    public void AttackAnim()
    {
        _playerControl.Anim.SetTrigger("IsAttack");
    }


    public void GetBoard()
    {
        _playerControl.Anim.SetTrigger("IsGetBoard");
    }

    public void DirSet()
    {
        var h = _playerControl.InputManager.HorizontalMove;
        var v = _playerControl.InputManager.VerticalMove;


        bool isSignBoard = _playerControl.SignBoardCheck.CheckSignboard();

        if (h == 0 && v==0)
        {
            if (isSignBoard)
            {
                _playerControl.ModelT.forward = -_playerControl.PlayerT.right;
                _isGettingBoard = true;
                _playerControl.AnimControl.SetSignBoard(isSignBoard);
            }
        }
        else
        {

            Vector3 dir = _playerControl.WallCheck.CheckWallToMoveDir(_playerControl.PlayerT.forward);

            if (_playerControl.InputManager.HorizontalMove == -1)
            {
                dir = -dir;
            }

            _playerControl.ModelT.forward = dir.normalized;


            _playerControl.AnimControl.SetSignBoard(false);
            _isGettingBoard = false;
        }
    }






}
