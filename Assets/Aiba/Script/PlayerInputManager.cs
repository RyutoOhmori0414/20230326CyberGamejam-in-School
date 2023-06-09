using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInputManager
{
    [Header("プレイヤーの番号")]
    [Range(1, 2)]
    [Tooltip("プレイヤーの番号")] [SerializeField] private int _playerNumber;

    private float _horizontalMove = 0;

    private float _verticalMove = 0;

    private bool _isAttack = false;

    private int _beforHorizontal = 1;

    public float HorizontalMove => _horizontalMove;

    public float VerticalMove => _verticalMove;

    public int PlayerNumber => _playerNumber;

    public bool IsAttack => _isAttack;
    public int BeforHorizontal => _beforHorizontal;

    private PlayerControl _playerControl;

    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }

    public void Update()
    {

        if (_playerNumber == 1)
        {
            _horizontalMove = Input.GetAxisRaw("Horizontal1");
            _verticalMove = Input.GetAxisRaw("Vertical1");

            if (_horizontalMove > 0)
            {
                _beforHorizontal = 1;
            }
            else if (_horizontalMove < 0)
            {
                _beforHorizontal = -1;
            }

            _isAttack = Input.GetButtonDown("Jump");
        }
        else if (_playerNumber == 2)
        {
            _horizontalMove = Input.GetAxisRaw("Horizontal2");
            _verticalMove = Input.GetAxisRaw("Vertical2");

            if (_horizontalMove > 0)
            {
                _beforHorizontal = 1;
            }
            else if (_horizontalMove < 0)
            {
                _beforHorizontal = -1;
            }
            _isAttack = Input.GetButtonDown("IsAttack2");
        }


    }
}
