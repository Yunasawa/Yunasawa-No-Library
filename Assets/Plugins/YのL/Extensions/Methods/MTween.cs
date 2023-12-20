using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using static UnityEngine.GraphicsBuffer;

public static class MTween
{
    /// <summary>
    /// Make a tweening transition for Image's color.
    /// </summary>
    public static void TweenColor(this Image image, Color color1, Color color2, float duration)
    {
        image.StartCoroutine(MTweenCoroutine.ChangeColorCoroutine(image, color1, color2, duration));
    }

    /// <summary>
    /// Make a tweening transition for TextMeshPro's color.
    /// </summary>
    public static void TweenColor(this TextMeshProUGUI tmp, Color color1, Color color2, float duration)
    {
        tmp.StartCoroutine(MTweenCoroutine.ChangeColorCoroutine(tmp, color1, color2, duration));
    }

    /// <summary>
    /// Make a tweening transition for UI's RectTransform.
    /// </summary>
    public static void TweenRectTransform(this RectTransform rectTransform, RectTransform target, float duration)
    {
        rectTransform.GetComponent<MonoBehaviour>()?.StartCoroutine(MTweenCoroutine.TransitionCoroutine(rectTransform, target, duration));
    }
}

public static class MTweenCoroutine
{
    public static IEnumerator ChangeColorCoroutine(Image image, Color color1, Color color2, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / duration);

            image.color = Color.Lerp(color1, color2, normalizedTime);

            yield return null;
        }

        image.color = color2;
    }

    public static IEnumerator ChangeColorCoroutine(TextMeshProUGUI tmp, Color color1, Color color2, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / duration);

            tmp.color = Color.Lerp(color1, color2, normalizedTime);

            yield return null;
        }

        tmp.color = color2;
    }

    public static IEnumerator TransitionCoroutine(RectTransform rectTransform, RectTransform target, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / duration);

            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, target.anchoredPosition, normalizedTime);
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, target.localScale, normalizedTime);
            rectTransform.rotation = Quaternion.Slerp(rectTransform.rotation, target.rotation, normalizedTime);

            yield return null;
        }

        rectTransform.anchoredPosition = target.anchoredPosition;
        rectTransform.localScale = target.localScale;
        rectTransform.rotation = target.rotation;
    }
}