using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSignBoardCheck : MonoBehaviour
{
    [Header("�Ŕl�����́A���G����")]
    [Tooltip("�Ŕl�����́A���G����")] [SerializeField] private float _godTime = 2;

    [SerializeField] private float _rayMaxLong = 100;

    [SerializeField] private LayerMask _layerMaskSignBoard;

    private RaycastHit _raycastHitSignboard;

    private bool _isGetBoard = false;

    public bool IsGetBoard => _isGetBoard;
    private PlayerControl _playerControl;
    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }

    /// <summary>�Ŕ�T��</summary>
    public bool CheckSignboard()
    {
        if (_isGetBoard)
        {
            bool isHit = Physics.Raycast(_playerControl.PlayerT.position, -_playerControl.PlayerT.right, out _raycastHitSignboard, _rayMaxLong, _layerMaskSignBoard);

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
}
