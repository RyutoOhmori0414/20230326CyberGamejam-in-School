using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerWallCheck
{

    [SerializeField] private float _rayMaxLong = 100;

    [SerializeField] private Vector3 _rayDir = Vector3.forward;

    [SerializeField] private LayerMask _layerMask;


    private PlayerControl _playerControl;

    private RaycastHit _raycastHitMoeWall;

    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }


    /// <summary>�ړ��p�̕ǂ��v�Z</summary>
    /// <param name="playerFoward">�v���C���[�̐���</param>
    /// <returns></returns>
    public Vector3 CheckWallToMoveDir(Vector3 playerFoward)
    {
        ////////����////////

        //����΂�
        Physics.Raycast(_playerControl.PlayerT.position, -_playerControl.PlayerT.right, out _raycastHitMoeWall, _rayMaxLong, _layerMask);

        //�@�������
        Vector3 wallNomal = _raycastHitMoeWall.normal;

        //�O�ς��g���A�i�s���������
        Vector3 _moveDirLeft = Vector3.Cross(wallNomal, _playerControl.PlayerT.up);

        //�v���X�ƃ}�C�i�X�̊O�σx�N�g���Ǝ��g�̌����Ă���������ׂ�B
        //�߂�����i�ޕ����Ƃ���
        if ((playerFoward - _moveDirLeft).magnitude > (playerFoward - -_moveDirLeft).magnitude)
        {
            _moveDirLeft = -_moveDirLeft;
        }

        return _moveDirLeft;
    }


    public void OnDrawGizmos(Transform player)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(player.position, player.position + (-player.right * 10));

        Gizmos.color = Color.blue;
        //Vector3 dir2 = player.position + _moveDir;
        //Gizmos.DrawLine(player.position, _moveDir);

    }
}
