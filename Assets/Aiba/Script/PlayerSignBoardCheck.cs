using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSignBoardCheck
{
    [Header("�Ŕl�����́A���G����")]
    [Tooltip("�Ŕl�����́A���G����")] [SerializeField] private float _godTime = 2;

    [SerializeField] private float _rayMaxLong = 100;

    [SerializeField] private LayerMask _layerMaskSignBoard;

    private RaycastHit _raycastHitSignboard;

    private bool _isGetBoard = false;

    private float _countGodTime = 0;

    public bool IsGetBoard => _isGetBoard;
    private PlayerControl _playerControl;
    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }

    /// <summary>�Ŕ�T��</summary>
    public bool CheckSignboard()
    {
        if (!_isGetBoard)
        {
            bool isHit = Physics.Raycast(_playerControl.PlayerT.position, -_playerControl.PlayerT.right, out _raycastHitSignboard, _rayMaxLong, _layerMaskSignBoard);

            Debug.Log(isHit);

            if (isHit)
            {
                _raycastHitSignboard.collider.gameObject.GetComponent<IGauge>()?.Gauge(_playerControl.InputManager.PlayerNumber);

                if (true)
                {
                    _isGetBoard = true;
                    _playerControl.AnimControl.GetBoard();
                }
            }

            return true;
        }
        else
        {
            return false;
        }

    }

    public void CountGodTime()
    {
        if(_isGetBoard)
        {
            _countGodTime += Time.deltaTime;

            if (_countGodTime > _godTime)
            {
                _countGodTime = 0;
                _isGetBoard = false;
            }
        }
    }


}
