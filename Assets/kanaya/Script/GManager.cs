using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GManager : SingletonMonoBehaviour<GManager>
{
    [Header("現在フィーバーであるか"), SerializeField]
    private bool _isFever;

    [Header("ゲームが進行しているか"), SerializeField]
    private　bool _isGame;

    [Header("通行人"), SerializeField]
    private GameObject _passerby;

    [Header("ゲームの制限時間"), SerializeField]
    private float _timer;

    [Header("フィーバーの有効時間"), SerializeField]
    private float _feverTime;

    [Header("現在のフィーバー時間"), SerializeField]
    private float _feverTimer;

    [Header("何秒ごとか"), SerializeField]
    private float _feverSpan;

    [Header("現在の時間"), SerializeField]
    private float _spanTime;

    //プレイヤー１は[0],プレイヤー２は[1]でお願いします
    [Header("それぞれの現在の得点"), SerializeField]
    private float[] _score;

    [Header("スコアを何秒ごとにゲットするか"), SerializeField]
    private float _scoreGetSpan;

    [Header("スコアをゲットする現在の時間"), SerializeField]
    private float _scoreGetTime;

    [Header("それぞれの看板の数"), SerializeField]
    private int[] _signboard;

    //プレイヤー１は[0],プレイヤー２は[1]でお願いします
    [Header("得点テキスト"), SerializeField]
    private Text[] _scoreText;

    [Header("タイマーテキスト"), SerializeField]
    private Text _timerText;

    [Header("リザルトパネル"), SerializeField]
    private GameObject _resultPanel;

    //プレイヤー１は[0],プレイヤー２は[1]でお願いします
    [Header("プレイヤーリザルトスプライト"), SerializeField]
    private Sprite[] _playerSprite;

    [Header("勝利スプライト"), SerializeField]
    private Sprite _winSprite;

    [Header("敗北スプライト"), SerializeField]
    private Sprite _loseSprite;

    //プレイヤー１は[0],プレイヤー２は[1]でお願いします
    [Header("リザルトスコアテキスト"), SerializeField]
    private Text[] _resultScoreText;

    [Header("UnityEvent"), SerializeField]
    private UnityEvent _endEvent;

    int _num;

    void Start()
    {
        _isGame = true;
        _passerby.SetActive(false);
        _resultPanel.SetActive(false);        
    }

    void Update()
    {
        Timer();
        FeverTime();
        Score(0); //プレイヤー１
        Score(1); //プレイヤー２

        _scoreText[0].text = _score[0].ToString();
        _scoreText[1].text = _score[1].ToString();

        _timerText.text = _timer.ToString("f1");
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
            _endEvent.Invoke();
            Result();
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

    /// <summary>スコアを追加する</summary>
    /// <param name="id">どのプレイヤーに</param>
    public void Score(int id)
    {
        if (_isGame)
        {
            if (_isFever)
            {
                Debug.Log("フィーバー中");
                if (_scoreGetTime > _scoreGetSpan / 2)
                {
                    AddScore(_signboard[id]/*看板の数*/, id);
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
                    AddScore(_signboard[id]/*看板の数*/, id);
                    _scoreGetTime = 0;
                }
                else
                {
                    _scoreGetTime += Time.deltaTime;
                }
            }
        }
    }

    /// <summary>スコアを追加</summary>
    /// <param name="score">手に入れたスコア</param>
    /// <param name="id">どのプレイヤーに</param>
    public void AddScore(float score, int id)
    {
        _score[id] += score;
    }

    /// <summary>看板の数を追加</summary>
    /// <param name="id">どのプレイヤーに</param>
    public void  AddSignboard(int id)
    {
        _signboard[id] += 1;
    }

    /// <summary>看板の数を削減</summary>
    /// <param name="id">どのプレイヤーに</param>
    public void ReductionSignboard(int id)
    {
        _signboard[id] -= 1;
    }

    /// <summary>結果を表示</summary>
    public void Result()
    {
        _resultPanel.SetActive(true);
        _resultScoreText[0].text = _score[0].ToString();
        _resultScoreText[1].text = _score[1].ToString();

        if (_score[0] == _score[1])
        {
            //引き分け
        }
        else if(_score[0] > _score[1]) //プレイヤー１のスコアの方が多い
        {
            _playerSprite[0] = _winSprite;
            _playerSprite[1] = _loseSprite;
        }
        else　
        {
            _playerSprite[0] = _loseSprite;
            _playerSprite[1] = _winSprite;
        }

    }

}
