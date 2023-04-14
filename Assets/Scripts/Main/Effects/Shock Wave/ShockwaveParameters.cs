using UnityEngine;

public class ShockwaveParameters : EffectParameters
{
    [SerializeField] private float _upperFeather;
    [SerializeField] private float _bottomFeather;
    [SerializeField] private float _rippleIntensity;

    public void ChangeUpperFeather(float value) => _upperFeather = value;
    public void ChangeUpperFeather(string value) => _upperFeather = float.Parse(value);

    public void ChangeBottomFeather(float value) => _bottomFeather = value;
    public void ChangeBottomFeather(string value) => _bottomFeather = float.Parse(value);

    public void ChangeRippleIntensity(float value) => _rippleIntensity = value;
    public void ChangeRippleIntensity(string value) => _rippleIntensity = float.Parse(value);
}