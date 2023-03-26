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

    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }


    public void Move()
    {
        _playerControl.Rb.AddForce(_playerControl.PlayerDir.MoveDir * _moveSpeedX * _playerControl.InputManager.HorizontalMove);
        _playerControl.Rb.AddForce(_playerControl.PlayerT.up * _moveSpeedY * _playerControl.InputManager.VerticalMove);
    }

    public void LimitSpeed()
    {
        //X�̑��x����
        if(_playerControl.Rb.velocity.x>_moveSpeedLimitX)
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
