using UnityEngine;

public class EffectShaderList : MonoBehaviour
{
    [SerializeField] private Shader[] _shaderList;
    private static Shader[] _shaders;

    public enum Shaders
    {
        Shockwave = 0
    }

    public static Shader GetShader(Shaders shaders) => _shaders[(int)shaders];

    private void Start() => _shaders = _shaderList;
}
