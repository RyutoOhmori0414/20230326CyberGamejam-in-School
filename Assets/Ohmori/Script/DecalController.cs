using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DecalController : MonoBehaviour
{
    private void Start()
    {
        DOTween.Sequence()
            .SetDelay(Random.Range(3.0f, 6.0f))
            .OnComplete(() => Destroy(this.gameObject));
    }
}
