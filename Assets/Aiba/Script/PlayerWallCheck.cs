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


    /// <summary>移動用の壁を計算</summary>
    /// <param name="playerFoward">プレイヤーの正面</param>
    /// <returns></returns>
    public Vector3 CheckWallToMoveDir(Vector3 playerFoward)
    {
        ////////左側////////

        //例を飛ばす
        Physics.Raycast(_playerControl.PlayerT.position, -_playerControl.PlayerT.right, out _raycastHitMoeWall, _rayMaxLong, _layerMask);

        //法線を取る
        Vector3 wallNomal = _raycastHitMoeWall.normal;

        //外積を使い、進行方向を取る
        Vector3 _moveDirLeft = Vector3.Cross(wallNomal, _playerControl.PlayerT.up);

        //プラスとマイナスの外積ベクトルと自身の向いている方向を比べる。
        //近い方を進む方向とする
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
