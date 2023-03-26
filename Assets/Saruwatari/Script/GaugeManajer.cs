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
    public bool _reset1;

    [SerializeField, Tooltip("Player2Slider")]
    Slider _P2slider;
    [Min(0)] int Max2Slider = 100;
    public float Current2Slider;
    public bool _reset2;

    public float  Gauge(int PlayerNumber)
    {
        if(PlayerNumber == 1 && Current1Slider < 100)
        {   
            Current1Slider += Time.deltaTime;
            _P1slider.value = (float)Current1Slider / (float)Max1Slider;
            _P1slider.transform.SetAsLastSibling();
            //_P1slider.transform.SetSiblingIndex(2);
            //renderer.sortingOrder = 10;
            _reset2 = true;
        }
        else if (PlayerNumber == 2 && Current2Slider < 100)
        {
            Current2Slider += Time.deltaTime;
            _P2slider.value = (float)Current2Slider / (float)Max2Slider;
            _P2slider.transform.SetAsLastSibling();
            _reset1 = true;
        }
        else if (PlayerNumber == 1 && Current1Slider > 100 && Current2Slider > 0)
        {
            Current2Slider -= Time.deltaTime;
            _P2slider.value = (float)Current2Slider / (float)Max2Slider;
            _P2slider.transform.SetAsLastSibling();
        }
        else if (PlayerNumber == 2 && Current2Slider > 100 && Current1Slider > 0)
        {
            Current1Slider -= Time.deltaTime;
            _P1slider.value = (float)Current1Slider / (float)Max1Slider;
            _P1slider.transform.SetAsLastSibling();
        }

        return Current1Slider;
    }
    void Reeset()
    {
        if (Current1Slider > 100 && _reset2 == true)
        {
            Current2Slider = 0;
            _reset2 = false;
        }
        else if (Current2Slider > 100 && _reset1 == true)
        {
            Current1Slider = 0;
            _reset1 = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Reeset();
    }

}
