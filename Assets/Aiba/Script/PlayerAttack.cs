using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAttack
{
    [Header("吹き飛ばしパワー")]
    [Tooltip("吹き飛ばしパワー")] [SerializeField] private float _power;

    [Header("クールタイム")]
    [Tooltip("クールタイム")] [SerializeField] private float _coolTime;

    [Header("ダメージ時間")]
    [Tooltip("ダメージ時間")] [SerializeField] private float _damageTime;

    [Header("無敵時間")]
    [Tooltip("無敵時間")] [SerializeField] private float _godTime = 5;

    [Header("プレイヤーのレイヤー")]
    [Tooltip("プレイヤーのレイヤー")] [SerializeField] private LayerMask _playerLayer;

    [Header("動きが止まるまでの減速具合")]
    [Tooltip("動きが止まるまでの減速具合")] [SerializeField] private float _speedLowTime = 6f;

    [SerializeField] private Vector3 _offset;

    [SerializeField] private Vector3 _size;

    private float _countSlowTime;

    int _dir;

    private float _damageTimeCount = 0;

    private float _countCoolTime = 0;

    private bool _isCanAttack = true;

    private bool _isDamage = false;

    private bool _isGod;

    private float _godTimeCount = 0;
    public bool IsDamage => _isDamage;

    private PlayerControl _playerControl;
    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }

    public void AttackCoolTime()
    {
        if (!_isCanAttack)
        {
            _countCoolTime += Time.deltaTime;

            if (_countCoolTime > _coolTime)
            {

                _countCoolTime = 0;
                _isCanAttack = true;
            }

        }
    }


    public void Attack()
    {
        if (!_isCanAttack || _playerControl.AnimControl.IsGettingBoard)
        {
            return;
        }
        else
        {
            if (_playerControl.InputManager.IsAttack)
            {
                Debug.Log($"{_playerControl.InputManager.PlayerNumber }:Attack");
                _isCanAttack = false;
                IsHit();

                _playerControl.AnimControl.SetSignBoard(false);
                _playerControl.AnimControl.AttackAnim();
            }
        }
    }


    public void Damage(int dir)
    {
        if (_playerControl.SignBoardCheck.IsGetBoard || _isGod)
        {
            return;
        }
        else
        {
            _dir = dir;
            Debug.Log($"{_playerControl.InputManager.PlayerNumber }:Hit");
            _isDamage = true;
            _isGod = true;

            _playerControl.AnimControl.SetSignBoard(false);

            _playerControl.CameraControl.CameraShake();
            _playerControl.ParticleSystem.Play();

            _playerControl.AnimControl.DamageAnim(true);
        }
    }


    public void DamageTime()
    {
        if (_isDamage)
        {
            _damageTimeCount += Time.deltaTime;

            Vector3 velo = (_playerControl.PlayerDir.MoveDir * _power * _dir);
            _playerControl.Rb.velocity = velo;



            if (_damageTimeCount >= _damageTime)
            {
                _countSlowTime = 0;
                _damageTimeCount = 0;
                _playerControl.Rb.velocity = Vector3.zero;
                _isDamage = false;
                _playerControl.ParticleSystem.Stop();
                _playerControl.AnimControl.DamageAnim(false);
            }
        }

        if (_isGod)
        {
            _godTimeCount += Time.deltaTime;

            if (_godTimeCount >= _godTime)
            {
                _godTime = 0;
                _isGod = false;
            }
        }


    }

    /// <summary>プレイヤーの前方に当たり判定を出してプレイヤーが当たったかどうか計算する</summary>
    public void IsHit()
    {
        var horizontalRotation = Quaternion.AngleAxis(_playerControl.ModelT.eulerAngles.y, Vector3.up);
        Vector3 velo = horizontalRotation * new Vector3(_offset.x, 0, _offset.z);
        velo.y = _offset.y;

        var posX = _playerControl.PlayerT.position.x + velo.x;
        var posY = _playerControl.PlayerT.position.y + velo.y;
        var posz = _playerControl.PlayerT.position.z + velo.z;

        Collider[] col = Physics.OverlapBox(new Vector3(posX, posY, posz), _size, Quaternion.identity, _playerLayer);
        Debug.Log(col.Length);
        foreach (var a in col)
        {
            a.TryGetComponent<IDamageble>(out IDamageble damageble);
            a.TryGetComponent<PlayerControl>(out PlayerControl playerControl);

            if (damageble != null && playerControl.InputManager.PlayerNumber != _playerControl.InputManager.PlayerNumber)
            {
                damageble.AddDamage((int)_playerControl.InputManager.BeforHorizontal);
                return;
            }
        }
    }


    public void OnDrawGizmos(Transform player, Transform model)
    {
        Gizmos.color = Color.blue;
        var horizontalRotation = Quaternion.AngleAxis(model.eulerAngles.y, Vector3.up);
        Vector3 velo = horizontalRotation * new Vector3(_offset.x, 0, _offset.z);
        velo.y = _offset.y;

        var posX = player.position.x + velo.x;
        var posY = player.position.y + velo.y;
        var posz = player.position.z + velo.z;
        Gizmos.DrawCube(new Vector3(posX, posY, posz), _size);


    }

}
