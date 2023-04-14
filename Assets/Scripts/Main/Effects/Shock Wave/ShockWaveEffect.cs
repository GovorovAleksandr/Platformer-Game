using UnityEngine;

public sealed class ShockWaveEffect : BaseEffect
{
    [SerializeField] private float _upperFeather;
    [SerializeField] private float _bottomFeather;
    [SerializeField] private float _rippleIntensity;

    [SerializeField] private Shader _shader;
    [SerializeField] private Camera _camera;

    private Material _material;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (_shader == null) return;
        if (_material == null) _material = new Material(_shader);

        _material.SetFloat("_UpperFeather", _upperFeather);
        _material.SetFloat("_BottomFeather", _bottomFeather);
        _material.SetFloat("_RippleIntensity", _rippleIntensity);

        if (Started) Graphics.Blit(source, destination, _material);
    }
}
