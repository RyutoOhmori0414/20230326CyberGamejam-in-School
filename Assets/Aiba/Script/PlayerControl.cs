using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    [SerializeField] private PlayerInputManager _playerInput;

    [SerializeField] private PlayerMove _move;

    [SerializeField] private PlayerWallCheck _wallCheck;

    [SerializeField] private PlayerSignBoardCheck _signBoardCheck;

    [SerializeField] private PlayerDir _playerDir;

    [SerializeField] private PlayerAnimControl _animControl;


    [SerializeField] private Transform _playerT;

    [SerializeField] private Transform _modelT;

    [SerializeField] private Rigidbody _rb;

    [SerializeField] private Animator _anim;



    public PlayerSignBoardCheck SignBoardCheck => _signBoardCheck;
    public Animator Anim => _anim;
    public PlayerAnimControl AnimControl => _animControl;
    public Transform ModelT => _modelT;
    public Transform PlayerT => _playerT;
    public PlayerInputManager InputManager => _playerInput;
    public PlayerMove Move => _move;
    public PlayerDir PlayerDir => _playerDir;
    public PlayerWallCheck WallCheck => _wallCheck;
    public Rigidbody Rb => _rb;

    private void Awake()
    {
        _playerInput.Init(this);
        _signBoardCheck.Init(this);
        _animControl.Init(this);

        _playerInput.Init(this);
        _move.Init(this);
        _wallCheck.Init(this);

        _playerDir.Init(this);

    }

    void Start()
    {

    }


    void Update()
    {
        //Inputの更新
        _playerInput.Update();

        //移動方向を決める
        _playerDir.SetDir();

        //モデルの方向
        _animControl.DirSet();

        //速度制限
        _move.LimitSpeed();

        _animControl.ParametaSet();


    }

    private void FixedUpdate()
    {
        //速度を加える
        _move.Move();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        _wallCheck.OnDrawGizmos(_playerT);
    }
}
