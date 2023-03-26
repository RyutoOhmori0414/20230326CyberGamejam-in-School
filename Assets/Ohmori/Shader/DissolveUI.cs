using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveUI : BaseUIAnim
{
    [SerializeField, Tooltip("ディゾルブに使用するTexture")]
    Texture2D _dissolveTex;
    [SerializeField, Range(0.0f, 1.0f), Tooltip("ディゾルブの閾値")]
    float _dissolveAmount;
    [SerializeField, Range(0.0f, 1.0f), Tooltip("境目の太さ")]
    float _dissolveRange;
    [SerializeField, ColorUsage(false, true), Tooltip("境目の色")]
    Color _dissolveColor;
    [SerializeField, Tooltip("ディゾルブのシェーダー")]
    Shader _dissolveShader;
    [SerializeField, Tooltip("ディゾルブのタイリング")]
    Vector2 _dissolveTilling = Vector2.one;
    [SerializeField, Tooltip("ディゾルブのオフセット")]
    Vector2 _dissolveOffset = Vector2.zero;

    int _dissolveTexId = Shader.PropertyToID("_DissolveTex");
    int _dissolveAmountId = Shader.PropertyToID("_DissolveAmount");
    int _dissolveRangeId = Shader.PropertyToID("_DissolveRange");
    int _dissolveColorId = Shader.PropertyToID("_DissolveColor");

    protected override void UpdataMaterial(Material baseMaterial)
    {
        if (!_material)
        {
            _material = new Material(_dissolveShader);
            _material.CopyPropertiesFromMaterial(baseMaterial);
            _material.hideFlags = HideFlags.HideAndDontSave;
        }

        _material.SetTexture(_dissolveTexId, _dissolveTex);
        _material.SetTextureScale(_dissolveTexId, _dissolveTilling);
        _material.SetTextureOffset(_dissolveTexId, _dissolveOffset);
        _material.SetFloat(_dissolveAmountId, _dissolveAmount);
        _material.SetFloat(_dissolveRangeId, _dissolveRange);
        _material.SetColor(_dissolveColorId, _dissolveColor);
    }
}
