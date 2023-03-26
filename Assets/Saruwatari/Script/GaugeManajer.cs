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

    [SerializeField, Tooltip("Player2Slider")]
    Slider _P2slider;
    [Min(0)] int Max2Slider = 100;
    public float Current2Slider;

    public void  Gauge(int PlayerNumber)
    {
        if(PlayerNumber == 1)
        {
            Current1Slider += Time.deltaTime;
            _P1slider.value = (float)Current1Slider / (float)Max1Slider;
            _P1slider.transform.SetAsLastSibling();
            //_P1slider.transform.SetSiblingIndex(2);
            //renderer.sortingOrder = 10;

        }
        else
        {
            Current2Slider += Time.deltaTime;
            _P2slider.value = (float)Current2Slider / (float)Max2Slider;
            _P2slider.transform.SetAsLastSibling();
        }
    }

    // Start is called before the first frame update
    void Start()
    { 
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
