using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// =====================================================
//  MenuButtonAnimation.cs
//  Attach to: EACH Button GameObject in your menu
//  Requires: an Image child called "ShineOverlay"
// =====================================================

public class MenuButtonAnimation : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler,
    IPointerDownHandler, IPointerUpHandler
{
    [Header("Shine")]
    public Image shineOverlay;         // child Image with white semi-transparent sprite
    public float shineDuration = 0.4f; // how fast the shine sweeps

    [Header("Scale")]
    public float hoverScaleX   = 1.04f;
    public float normalScaleX  = 1.0f;
    public float scaleDuration = 0.15f;

    [Header("Press")]
    public float pressScale    = 0.96f;
    public float pressDuration = 0.08f;

    private RectTransform rect;
    private Coroutine scaleCoroutine;
    private Coroutine shineCoroutine;
    private bool isHovered = false;

    // ── Unity lifecycle ───────────────────────────────────────────────────────

    void Awake()
    {
        rect = GetComponent<RectTransform>();

        // hide shine at start
        if (shineOverlay)
        {
            Color c = shineOverlay.color;
            c.a = 0f;
            shineOverlay.color = c;
        }
    }

    // ── Pointer events ────────────────────────────────────────────────────────

    public void OnPointerEnter(PointerEventData _)
    {
        isHovered = true;
        ScaleTo(hoverScaleX, scaleDuration);
        PlayShine();
    }

    public void OnPointerExit(PointerEventData _)
    {
        isHovered = false;
        ScaleTo(normalScaleX, scaleDuration);
    }

    public void OnPointerDown(PointerEventData _)
    {
        ScaleTo(pressScale, pressDuration);
    }

    public void OnPointerUp(PointerEventData _)
    {
        ScaleTo(isHovered ? hoverScaleX : normalScaleX, pressDuration);
    }

    // ── Shine sweep ───────────────────────────────────────────────────────────

    void PlayShine()
    {
        if (shineOverlay == null) return;
        if (shineCoroutine != null) StopCoroutine(shineCoroutine);
        shineCoroutine = StartCoroutine(ShineRoutine());
    }

    IEnumerator ShineRoutine()
    {
        RectTransform shinRT = shineOverlay.GetComponent<RectTransform>();
        float btnWidth = rect.rect.width;

        // start shine to the left, off-screen
        shinRT.anchoredPosition = new Vector2(-btnWidth, 0f);
        SetShineAlpha(0.28f);

        float elapsed  = 0f;
        float startX   = -btnWidth * 0.5f;
        float endX     =  btnWidth * 1.1f;

        while (elapsed < shineDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / shineDuration);
            float x = Mathf.Lerp(startX, endX, t);
            shinRT.anchoredPosition = new Vector2(x, 0f);
            yield return null;
        }

        SetShineAlpha(0f);
    }

    void SetShineAlpha(float a)
    {
        if (shineOverlay == null) return;
        Color c = shineOverlay.color;
        c.a = a;
        shineOverlay.color = c;
    }

    // ── Scale helper ──────────────────────────────────────────────────────────

    void ScaleTo(float targetX, float duration)
    {
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(ScaleRoutine(targetX, duration));
    }

    IEnumerator ScaleRoutine(float targetX, float duration)
    {
        Vector3 startScale = rect.localScale;
        Vector3 endScale   = new Vector3(targetX, 1f, 1f);
        float   elapsed    = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            // smooth step for a snappier feel
            t = t * t * (3f - 2f * t);
            rect.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }

        rect.localScale = endScale;
    }
}