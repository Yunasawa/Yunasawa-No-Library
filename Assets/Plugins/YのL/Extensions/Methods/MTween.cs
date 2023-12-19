using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class MTween
{
    public static void TweenColor(this Image image, Color color1, Color color2, float duration)
    {
        image.StartCoroutine(MTweenCoroutine.ChangeColorCoroutine(image, color1, color2, duration));
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
            Color currentColor = Color.Lerp(color1, color2, normalizedTime);

            Debug.Log(currentColor);

            image.color = currentColor;

            yield return null;
        }

        image.color = color2;
    }
}