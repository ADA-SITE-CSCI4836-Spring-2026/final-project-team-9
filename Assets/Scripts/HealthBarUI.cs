using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public TimeHealth timeHealth;

    public Image fillImage;
    public RectTransform barTransform;

    public Color green = Color.green;
    public Color yellow = Color.yellow;
    public Color red = Color.red;

    Vector2 originalPos;

    void Start()
    {
        originalPos = barTransform.localPosition;
    }

    void Update()
    {
        float t = timeHealth.GetNormalized();

        fillImage.fillAmount = t;

        if (t > 0.5f)
            fillImage.color = Color.Lerp(yellow, green, (t - 0.5f) * 2f);
        else
            fillImage.color = Color.Lerp(red, yellow, t * 2f);

        if (timeHealth.currentState == TimeHealth.PlayerState.Idle || t < 0.3f)
        {
            ShakeBar(3f);
        }
        else
        {
            barTransform.localPosition = originalPos;
        }
    }

    void ShakeBar(float strength)
    {
        barTransform.localPosition = originalPos + new Vector2(
            Random.Range(-strength, strength),
            Random.Range(-strength, strength)
        );
    }
}