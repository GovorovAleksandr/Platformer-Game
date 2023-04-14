using System.Collections;
using UnityEngine;

public class ZoneDrawer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _drawableSquareRenderer;
    [SerializeField] private float _fadeTime;

    private float _defaultAlpha;

    public void ReDraw(Vector3 offset, Vector3 size)
    {
        _drawableSquareRenderer.transform.position = transform.position + offset;
        _drawableSquareRenderer.transform.localScale = size;
    }

    public void Show() => StartCoroutine(FadeIn());
    public void Hide() => StartCoroutine(FadeOut());

    private IEnumerator FadeIn()
    {
        Color color = _drawableSquareRenderer.color;
        while (color.a < _defaultAlpha)
        {
            color.a += Time.deltaTime;
            _drawableSquareRenderer.color = color;
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        Color color = _drawableSquareRenderer.color;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime;
            _drawableSquareRenderer.color = color;
            yield return null;
        }
    }

    private void Awake()
    {
        _defaultAlpha = _drawableSquareRenderer.color.a;
        Hide();
    }
}
