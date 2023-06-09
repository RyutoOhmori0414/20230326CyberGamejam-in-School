using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSignBoardCheck
{
    [Header("看板獲得時の、無敵時間")]
    [Tooltip("看板獲得時の、無敵時間")] [SerializeField] private float _godTime = 2;

    [SerializeField] private float _rayMaxLong = 100;

    [SerializeField] private LayerMask _layerMaskSignBoard;

    [SerializeField] private Vector3 _posAdd = default;

    private RaycastHit _raycastHitSignboard;

    private RaycastHit _raycastHitSignboardLeft;

    private bool _isGetBoard = false;

    private float _countGodTime = 0;

    private bool _isCall = false;

    private IGauge _gauge;


    public bool IsGetBoard => _isGetBoard;
    private PlayerControl _playerControl;
    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }


    public void Call()
    {
        var h = _playerControl.InputManager.HorizontalMove;
        var v = _playerControl.InputManager.VerticalMove;

        if (h != 0 || v != 0)
        {
            _isCall = false;
        }


        if (_isCall)
        {

            if (_gauge != null)
            {
                float num = _gauge.Gauge(_playerControl.InputManager.PlayerNumber);

                if (num == 100)
                {
                    _isGetBoard = true;
                    _playerControl.AnimControl.GetBoard();
                }
            }

        }
    }

    /// <summary>看板を探す</summary>
    public bool CheckSignboard()
    {
        //計測
        CountGodTime();
        if (!_isGetBoard)
        {
            bool isHit = Physics.Linecast(_playerControl.PlayerT.position + _posAdd, _posAdd + _playerControl.ModelT.right * 100, out _raycastHitSignboard, _layerMaskSignBoard);

            bool isHitLeft = Physics.Linecast(_playerControl.PlayerT.position + _posAdd, _posAdd + -_playerControl.ModelT.right * 100, out _raycastHitSignboardLeft, _layerMaskSignBoard);

            Debug.Log($"{_playerControl.InputManager.PlayerNumber}右):{isHit}");
            Debug.Log($"{_playerControl.InputManager.PlayerNumber}左):{isHitLeft}");

            if (isHit)
            {
                _isCall = true;
                _raycastHitSignboard.collider.gameObject.TryGetComponent<IGauge>(out IGauge gage);

                if (gage != null)
                {
                    _gauge = gage;
                }
                Debug.Log("呼んでいる");

                return true;
            }
            else if (isHitLeft)
            {
                _isCall = true;

                _raycastHitSignboardLeft.collider.gameObject.TryGetComponent<IGauge>(out IGauge gage);

                if (gage != null)
                {
                    _gauge = gage;
                }

                Debug.Log("呼んでいる");
                return true;
            }
            else
            {
                return false;
            }


        }
        else
        {
            return false;
        }

    }

    public void CountGodTime()
    {
        if (_isGetBoard)
        {
            _countGodTime += Time.deltaTime;

            if (_countGodTime > _godTime)
            {
                _countGodTime = 0;
                _isGetBoard = false;
            }
        }
    }

    public void OnDrawGizmos(Transform player, Transform model)
    {
        Gizmos.color = Color.green;

        // Gizmos.DrawLine(player.position+ _posAdd, player.position + _posAdd + (-model.right * 10));
        //Gizmos.DrawLine(player.position+ _posAdd, player.position + _posAdd + (model.right * 10));
    }
}
