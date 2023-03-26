using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    [Header("現在フィーバーであるか"), SerializeField]
    bool _isFever;

    [Header("通行人"), SerializeField]
    GameObject _passerby;

    [Header("ゲームの制限時間"), SerializeField]
    float _timer;

    [Header("フィーバーの有効時間"), SerializeField]
    float _feverTime;

    [Header("現在のフィーバー時間"), SerializeField]
    float _feverTimer;

    [Header("何秒ごとか"), SerializeField]
    float _feverSpan;

    [Header("現在の時間"), SerializeField]
    float _spanTime;

    [Header("現在の得点"), SerializeField]
    float[] _score;

    [Header("何秒ごとか"), SerializeField]
    float _scoreGetSpan;

    [Header("現在の時間"), SerializeField]
    float _scoreGetTime;

    bool _isGame;

    int _num;

    void Start()
    {
        _isGame = true;
        _passerby.SetActive(false);
    }

    void Update()
    {
        Timer();
        FeverTime();
        Score(1); //本来は看板を持っていたらよぶ
    }


    /// <summary>通行人を出すか消すか</summary>
    /// <param name="passerby">出すか消すか</param>
    public void Passerby(bool passerby)
    {
        _passerby.SetActive(passerby);
        _isFever = passerby;
        Debug.Log($"通行人が{passerby}");
        FeverTime();
    }

    public void Timer()
    {
        if (_timer < 0)
        {
            //ゲーム終了
            _isGame = false;
            Debug.Log("ゲーム終了");
        }
        else
        {
            _timer -= Time.deltaTime;

            if (_spanTime > _feverSpan)
            {
                _num = Random.Range(0, 2);
                Debug.Log(_num);
                switch (_num)
                {
                    case 0:
                        Debug.Log("通行人を出す");
                        Passerby(true);
                        _spanTime = 0;
                        break;
                    case 1:
                        Debug.Log("通行人を出さない");
                        _spanTime = 0;
                        break;
                    default:
                        Debug.Log("通行人を出さない");
                        _spanTime = 0;
                        break;
                }
            }
            else
            {
                _spanTime += Time.deltaTime;
            }
        }
    }


    public void FeverTime()
    {
        if (_isFever)
        {

            if (_feverTimer > _feverTime)
            {
                Debug.Log("通行人を消す");
                Passerby(false);
                _feverTimer = 0;
            }
            else
            {
                Debug.Log("通行人がいる");
                _feverTimer += Time.deltaTime;
            }
        }
        else
        {
            Debug.Log("フィーバーではない");
        }

    }

    public void Score(int id)
    {
        if (_isGame)
        {
            if (_isFever)
            {
                Debug.Log("フィーバー中");
                if (_scoreGetTime > _scoreGetSpan)
                {
                    AddScore(1 * 2/*看板の数×2*/, id);
                    _scoreGetTime = 0;
                }
                else
                {
                    _scoreGetTime += Time.deltaTime;
                }
            }
            else
            {
                if (_scoreGetTime > _scoreGetSpan)
                {
                    AddScore(1/*看板の数*/, id);
                    _scoreGetTime = 0;
                }
                else
                {
                    _scoreGetTime += Time.deltaTime;
                }
            }
        }

    }

    public void AddScore(float score, int id)
    {
        _score[id] += score;
    }
}
