using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeManajer : MonoBehaviour,IGauge
{
    [SerializeField, Tooltip("Player1Slider")]
    Slider _P1slider;
    [Min(0)] int Max1Slider = 100;
    public float Current1Slider;
    public float nowP1;
    //public float nowOverP1;

    [SerializeField, Tooltip("Player2Slider")]
    Slider _P2slider;
    [Min(0)] int Max2Slider = 100;
    public float Current2Slider;
    public float nowP2;
    //public float nowOverP2;

    public bool isNotGet;
    public bool isNotGetP1;

    [SerializeField,Header("ƒQ[ƒW‚Ì‘¬‚³")]
    public int x;

    public float  Gauge(int PlayerNumber)
    {
        //1
        if (PlayerNumber == 1)
        {
            if (isNotGet)
            {
                nowP1 += Time.deltaTime * x;
                _P1slider.value = (float)nowP1 / (float)Max1Slider;
                _P1slider.transform.SetAsLastSibling();

                if (nowP1 > 100)
                {
                    nowP2 = 0;
                    _P2slider.value = (float)nowP2 / (float)Max2Slider;
                    _P2slider.transform.SetAsLastSibling();
                    isNotGet = false;
                    isNotGetP1 = true;
                    GManager.Instance.AddSignboard(0);
                    return 100;
                }
                else
                {
                    return nowP1;
                }
            }
            else
            {
                if (isNotGetP1)
                {
                    if (/*nowOverP2*/nowP2 == 0)
                    {
                        return 0;
                    }
                    else if(nowP2 > 0)
                    {
                        nowP2 -= Time.deltaTime * x;
                        _P2slider.value = (float)nowP2 / (float)Max2Slider;
                        _P2slider.transform.SetAsLastSibling();
                        //nowOverP2 -= Time.deltaTime;
                    }
                }
                else if (isNotGetP1 == false)
                {
                    nowP1 += Time.deltaTime * x;
                    _P1slider.value = (float)nowP1 / (float)Max1Slider;
                    _P1slider.transform.SetAsLastSibling();

                    if (nowP1 > 100)
                    {
                        isNotGetP1 = true;
                        nowP2 = 0;
                        _P2slider.value = (float)nowP2 / (float)Max2Slider;
                        _P2slider.transform.SetAsLastSibling();
                        GManager.Instance.AddSignboard(0);
                        GManager.Instance.ReductionSignboard(1);
                        return 100;
                    }
                }
            }
        }

        //2
        else if (PlayerNumber == 2)
        {
            if (isNotGet)
            {
                nowP2 += Time.deltaTime * x;
                _P2slider.value = (float)nowP2 / (float)Max2Slider;
                _P2slider.transform.SetAsLastSibling();

                if (nowP2 > 100)
                {
                    nowP1 = 0;
                    _P1slider.value = (float)nowP1 / (float)Max1Slider;
                    _P1slider.transform.SetAsLastSibling();
                    isNotGet = false;
                    isNotGetP1 = false;
                    GManager.Instance.AddSignboard(1);
                    return 100;
                }
                else
                {
                    return nowP2;
                }
            }
            else
            {
                if (isNotGetP1 == false)
                {
                    if (nowP1 == 0)
                    {
                        return 0;
                    }
                    else if (nowP1 > 0)
                    {
                        nowP1 -= Time.deltaTime * x;
                        _P1slider.value = (float)nowP1 / (float)Max1Slider;
                        _P1slider.transform.SetAsLastSibling();
                    }
                }
                else if(isNotGetP1)
                {
                    nowP2 += Time.deltaTime * x;
                    _P2slider.value = (float)nowP2 / (float)Max2Slider;
                    _P2slider.transform.SetAsLastSibling();

                    if (nowP2 > 100)
                    {
                        isNotGetP1 = false;
                        nowP1 = 0;
                        _P1slider.value = (float)nowP1 / (float)Max1Slider;
                        _P1slider.transform.SetAsLastSibling();
                        GManager.Instance.AddSignboard(1);
                        GManager.Instance.ReductionSignboard(0);
                    }
                }
            }
        }

        //if(PlayerNumber == 1 && Current1Slider < 100)
        //{   
        //    Current1Slider += Time.deltaTime;
        //    _P1slider.value = (float)Current1Slider / (float)Max1Slider;
        //    _P1slider.transform.SetAsLastSibling();
        //    //_P1slider.transform.SetSiblingIndex(2);
        //    //renderer.sortingOrder = 10;
        //    _reset2 = true;
        //}
        //else if (PlayerNumber == 2 && Current2Slider < 100)
        //{
        //    Current2Slider += Time.deltaTime;
        //    _P2slider.value = (float)Current2Slider / (float)Max2Slider;
        //    _P2slider.transform.SetAsLastSibling();
        //    _reset1 = true;
        //}
        //else if (PlayerNumber == 1 && Current1Slider > 100 && Current2Slider > 0)
        //{
        //    Current2Slider -= Time.deltaTime;
        //    _P2slider.value = (float)Current2Slider / (float)Max2Slider;
        //    _P2slider.transform.SetAsLastSibling();
        //}
        //else if (PlayerNumber == 2 && Current2Slider > 100 && Current1Slider > 0)
        //{
        //    Current1Slider -= Time.deltaTime;
        //    _P1slider.value = (float)Current1Slider / (float)Max1Slider;
        //    _P1slider.transform.SetAsLastSibling();
        //}

        return Current2Slider;
        return Current1Slider;
    }
    

    

    


    void Start()
    {
        isNotGet = true;
    }

}
