using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMove
{
    [Header("�v���C���[�̉��A�ړ����x")]
    [Tooltip("�v���C���[�̉��A�ړ����x")] [SerializeField] private float _moveSpeedX;

    [Header("�v���C���[�̏c�A�ړ����x")]
    [Tooltip("�v���C���[�̏c�A�ړ����x")] [SerializeField] private float _moveSpeedY;

    [Header("���A�ړ����x����")]
    [Tooltip("���A�ړ����x����")] [SerializeField] private float _moveSpeedLimitX;

    [Header("�c�A�ړ����x����")]
    [Tooltip("�c�A�ړ����x����")] [SerializeField] private float _moveSpeedLimitY;

    private PlayerControl _playerControl;

    private float _timeCount = 0;

    private float _timeCountMove = 0;

    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }


    public void Move()
    {
        if (!_playerControl.Attack.IsDamage)
        {
            if (!_playerControl.SignBoardCheck.IsGetBoard)
            {
                Vector3 velo = (_playerControl.PlayerDir.MoveDir * _moveSpeedX * _playerControl.InputManager.HorizontalMove);
                velo.y =_playerControl.InputManager.VerticalMove*3;
                _playerControl.Rb.velocity = velo;
            }
        }


        //if (!_playerControl.Attack.IsDamage)
        //{
        //    if (!_playerControl.SignBoardCheck.IsGetBoard)
        //    {

        //        //�c�����̓���
        //        if (_playerControl.InputManager.VerticalMove != 0)
        //        {
        //            _playerControl.Rb.AddForce(_playerControl.PlayerT.up * _moveSpeedY * _playerControl.InputManager.VerticalMove);
        //        }

        //        if (_playerControl.InputManager.HorizontalMove != 0)
        //        {


        //            _timeCountMove += Time.deltaTime;
        //            if (_timeCountMove >= 1)
        //            {
        //                _timeCountMove = 1;
        //            }



        //            Vector3 velo = (_playerControl.PlayerDir.MoveDir * _timeCountMove * _moveSpeedX * _playerControl.InputManager.HorizontalMove);
        //            velo.y = _playerControl.Rb.velocity.y;
        //            _playerControl.Rb.velocity = velo;

        //            _timeCount = 0;
        //        }
        //        else
        //        {
        //            _timeCountMove = 0;

        //            float speed = 0;

        //            if (_playerControl.Rb.velocity == Vector3.zero)
        //            {
        //                return;
        //            }

        //            if (_playerControl.InputManager.BeforHorizontal == 1)
        //            {
        //                speed = _playerControl.Rb.velocity.x - _timeCount;

        //                if (speed < 0)
        //                {
        //                    speed = 0;
        //                    return;
        //                }
        //            }
        //            else
        //            {
        //                speed = _playerControl.Rb.velocity.x + _timeCount;
        //                speed = -speed;
        //                if (speed < 0)
        //                {
        //                    speed = 0;
        //                    return;
        //                }
        //            }


        //            Vector3 velo = (_playerControl.PlayerDir.MoveDir * speed * _playerControl.InputManager.BeforHorizontal);
        //            velo.y = _playerControl.Rb.velocity.y;
        //            _playerControl.Rb.velocity = velo;


        //            _timeCount += Time.deltaTime * 2;
        //        }
        //    }
        //    else
        //    {
        //        _playerControl.Rb.velocity = Vector3.zero;
        //    }
        //}
    }

    public void LimitSpeed()
    {
        //X�̑��x����
        if (_playerControl.Rb.velocity.x > _moveSpeedLimitX)
        {
            _playerControl.Rb.velocity = new Vector3(_moveSpeedLimitX, _playerControl.Rb.velocity.y, _playerControl.Rb.velocity.z);
        }

        //-X�̑��x����
        if (_playerControl.Rb.velocity.x < -_moveSpeedLimitX)
        {
            _playerControl.Rb.velocity = new Vector3(-_moveSpeedLimitX, _playerControl.Rb.velocity.y, _playerControl.Rb.velocity.z);
        }

        //Y�̑��x����
        if (_playerControl.Rb.velocity.x > _moveSpeedLimitY)
        {
            _playerControl.Rb.velocity = new Vector3(_playerControl.Rb.velocity.x, _moveSpeedLimitY, _playerControl.Rb.velocity.z);
        }

        //-Y�̑��x����
        if (_playerControl.Rb.velocity.x < -_moveSpeedLimitY)
        {
            _playerControl.Rb.velocity = new Vector3(_playerControl.Rb.velocity.x, _moveSpeedLimitY, _playerControl.Rb.velocity.z);
        }
    }

}
