using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

[RequireComponent(typeof(Image))]
public class ButtonAnimation : MonoBehaviour
{
    private Image _image;
    private Color _normalColor;

    [Header("Hightlighted Color Tint")]
    [SerializeField] private bool _useHightlightedColorTint;

    [SerializeField] [ShowIf(nameof(_useHightlightedColorTint))]
    private Color _hightlightedColor;

    [SerializeField] [ShowIf(nameof(_useHightlightedColorTint))]
    private float _hightlightedFadeDuration;

    [Space(5)]

    [Header("Hightlighted Scale")]
    [SerializeField] private bool _useHightlightedScale;

    [SerializeField] [ShowIf(nameof(_useHightlightedScale))]
    private Vector2 _hightlightedScale;

    [HideInInspector] public Vector2 _hightlightedDefaulScale;

    [Space(25)]

    [Header("Pressed Color Tint")]
    [SerializeField] private bool _usePressedColorTint;

    [SerializeField] [ShowIf(nameof(_usePressedColorTint))]
    private Color _pressedColor;

    [SerializeField] [ShowIf(nameof(_usePressedColorTint))]
    private float _pressedFadeDuration;

    [Space(5)]

    [Header("Pressed Scale")]
    [SerializeField] private bool _usePressedScale;

    [SerializeField] [ShowIf(nameof(_usePressedScale))]
    private Vector2 _pressedScale;

    [HideInInspector] public Vector2 _pressedDefaulScale;

    private void Start()
    {
        _image = GetComponent<Image>();

        _normalColor = _image.color;
        _image.color = Color.white;
        _image.CrossFadeColor(_normalColor, 0, false, true);

        _hightlightedDefaulScale = transform.localScale;
        _pressedDefaulScale = transform.localScale;
    }

    public void OnPointerEnter()
    {
        if (_useHightlightedColorTint == true)
            _image.CrossFadeColor(_hightlightedColor, _hightlightedFadeDuration, false, true);

        if (_useHightlightedScale == true) transform.localScale = _hightlightedScale;

        if(Input.GetMouseButton(0)) OnPointerDown();
    }

    public void OnPointerExit()
    {
        if (_useHightlightedColorTint == true) _image.CrossFadeColor(_normalColor, _hightlightedFadeDuration, false, true);
        if (_useHightlightedScale == true) transform.localScale = _hightlightedDefaulScale;

        OnPointerUp();
    }

    public void OnPointerDown()
    {
        if (_usePressedColorTint == true)
            _image.CrossFadeColor(_pressedColor, _pressedFadeDuration, false, true);

        if (_usePressedScale == true) transform.localScale = _pressedScale;
    }

    public void OnPointerUp()
    {
        if (_usePressedColorTint == true) _image.CrossFadeColor(_normalColor, _pressedFadeDuration, false, true);
        if (_usePressedScale == true) transform.localScale = _pressedDefaulScale;
    }

    private void OnDisable() => OnPointerExit();
}
