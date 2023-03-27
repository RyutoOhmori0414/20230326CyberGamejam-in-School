using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PasserbyMove : MonoBehaviour
{
    Rigidbody rb;

    int _upForce;

    [Header("何秒ごと"), SerializeField]
    float _timeSpan;

    [Header("現在の時間"), SerializeField]
    float _timer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Move();
    }

    public void Update()
    {
        Jump();
    }

    public void Jump()
    {
        if(_timer > _timeSpan)
        {
            _upForce = Random.Range(200, 250);
            rb.AddForce(0f, _upForce, 0f);
            _timer = 0;
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }



    /*public void Move()
    {
        this.transform.DOMove(new Vector3(5.4f, -2.03f, 0f), 6f)
            .SetLoops(4, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }*/
}
