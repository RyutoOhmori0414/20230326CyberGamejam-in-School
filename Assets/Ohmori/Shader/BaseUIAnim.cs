using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways, RequireComponent(typeof(Graphic))]
public class BaseUIAnim : MonoBehaviour, IMaterialModifier
{
    private Graphic _animGraphic;
    protected Material _material;
    public Graphic AnimGraphic
    {
        get
        {
            _animGraphic ??= GetComponent<Graphic>();
            return _animGraphic;
        }
    }

    Material IMaterialModifier.GetModifiedMaterial(Material baseMaterial)
    {
        if (!isActiveAndEnabled || !_animGraphic)
        {
            return baseMaterial;
        }

        UpdataMaterial(baseMaterial);
        return _material;
    }

    void OnDidApplyAnimationProperties()
    {
        if (!isActiveAndEnabled || !_animGraphic)
        {
            return;
        }

        _animGraphic.SetMaterialDirty();
    }

    protected virtual void UpdataMaterial(Material baseMaterial)
    {
    }

    private void OnEnable()
    {
        if (!_animGraphic)
        {
            return;
        }

        _animGraphic.SetMaterialDirty();
    }

    private void OnDisable()
    {
        if (_material)
        {
            DestroyMaterial();
        }

        if (!AnimGraphic)
        {
            _animGraphic.SetMaterialDirty();
        }
    }

    public void DestroyMaterial()
    {
#if UNITY_EDITOR
        if (!EditorApplication.isPlaying)
        {
            DestroyImmediate(_material);
            _material = null;
            return;
        }
#endif
        Destroy(_material);
        _material = null;
    }

    private void OnValidate()
    {
        if (!isActiveAndEnabled || !AnimGraphic)
        {
            return ;
        }

        AnimGraphic.SetMaterialDirty();
    }
}
