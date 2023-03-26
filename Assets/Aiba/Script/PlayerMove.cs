using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMove
{
    [Header("プレイヤーの横、移動速度")]
    [Tooltip("プレイヤーの横、移動速度")] [SerializeField] private float _moveSpeedX;

    [Header("プレイヤーの縦、移動速度")]
    [Tooltip("プレイヤーの縦、移動速度")] [SerializeField] private float _moveSpeedY;

    [Header("横、移動速度制限")]
    [Tooltip("横、移動速度制限")] [SerializeField] private float _moveSpeedLimitX;

    [Header("縦、移動速度制限")]
    [Tooltip("縦、移動速度制限")] [SerializeField] private float _moveSpeedLimitY;

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
        //Xの速度制限
        if(_playerControl.Rb.velocity.x>_moveSpeedLimitX)
        {
            _playerControl.Rb.velocity = new Vector3(_moveSpeedLimitX, _playerControl.Rb.velocity.y, _playerControl.Rb.velocity.z);
        }

        //-Xの速度制限
        if (_playerControl.Rb.velocity.x < -_moveSpeedLimitX)
        {
            _playerControl.Rb.velocity = new Vector3(-_moveSpeedLimitX, _playerControl.Rb.velocity.y, _playerControl.Rb.velocity.z);
        }

        //Yの速度制限
        if (_playerControl.Rb.velocity.x > _moveSpeedLimitY)
        {
            _playerControl.Rb.velocity = new Vector3(_playerControl.Rb.velocity.x, _moveSpeedLimitY, _playerControl.Rb.velocity.z);
        }

        //-Yの速度制限
        if (_playerControl.Rb.velocity.x < -_moveSpeedLimitY)
        {
            _playerControl.Rb.velocity = new Vector3(_playerControl.Rb.velocity.x, _moveSpeedLimitY, _playerControl.Rb.velocity.z);
        }
    }

}
