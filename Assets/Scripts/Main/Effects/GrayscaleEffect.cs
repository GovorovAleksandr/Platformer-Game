using UnityEngine;

public class GrayscaleEffect : MonoBehaviour
{
    [SerializeField] private Shader _shader;
    private Material _material;
    [SerializeField] private float _redMultiplier;
    [SerializeField] private float _greenMultiplier;
    [SerializeField] private float _blueMultiplier;

    private void Start() => _material = new Material(_shader);

    private void OnRenderImage(RenderTexture _source, RenderTexture _destination)
    {
        _material.SetFloat("_redMultiplier", _redMultiplier);
        _material.SetFloat("_greenMultiplier", _greenMultiplier);
        _material.SetFloat("_blueMultiplier", _blueMultiplier);

        Graphics.Blit(_source, _destination, _material);
    }
}