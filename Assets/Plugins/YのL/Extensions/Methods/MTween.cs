using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;
using static UnityEngine.GraphicsBuffer;
using Newtonsoft.Json.Bson;

namespace YNL.Extension.Method
{
    public static class MTween
    {
        /// <summary>
        /// Make a tweening transition for Image's fillAmount.
        /// </summary>
        public static Coroutine TweenFillAmount(this Image image, float targetFillAmount, float duration, Action onComplete = null)
        {
            return image.StartCoroutine(MTweenCoroutine.FillAmountTweenCoroutine(image, image.fillAmount, targetFillAmount, duration, onComplete));
        }
        public static Coroutine TweenFillAmount(this MonoBehaviour mono, Image image, float targetFillAmount, float duration, Action onComplete = null)
        {
            return mono.StartCoroutine(MTweenCoroutine.FillAmountTweenCoroutine(image, image.fillAmount, targetFillAmount, duration, onComplete));
        }

        /// <summary>
        /// Make a tweening transition for Image's color.
        /// </summary>
        public static Coroutine TweenColor(this Image image, Color color1, Color color2, float duration)
        {
            return image.StartCoroutine(MTweenCoroutine.ColorCoroutine(image, color1, color2, duration));
        }
        public static Coroutine TweenColor(this MonoBehaviour mono, Image image, Color color1, Color color2, float duration)
        {
            return mono.StartACoroutine(MTweenCoroutine.ColorCoroutine(image, color1, color2, duration));
        }

        /// <summary>
        /// Make a tweening transition for TextMeshPro's color.
        /// </summary>
        public static Coroutine TweenColor(this TextMeshProUGUI tmp, Color color1, Color color2, float duration)
        {
            return tmp.StartCoroutine(MTweenCoroutine.ColorCoroutine(tmp, color1, color2, duration));
        }
        public static Coroutine TweenColor(this MonoBehaviour mono, TextMeshProUGUI tmp, Color color1, Color color2, float duration)
        {
            return mono.StartCoroutine(MTweenCoroutine.ColorCoroutine(tmp, color1, color2, duration));
        }

        /// <summary>
        /// Make a tweening transition for UI's RectTransform.
        /// </summary>
        public static Coroutine TweenRectTransform(this RectTransform rectTransform, RectTransform target, float duration)
        {
            return rectTransform.GetComponent<MonoBehaviour>()?.StartCoroutine(MTweenCoroutine.RectTransformCoroutine(rectTransform, target, duration));
        }
        public static Coroutine TweenRectTransform(this MonoBehaviour mono, RectTransform rectTransform, RectTransform target, float duration)
        {
            return mono.StartCoroutine(MTweenCoroutine.RectTransformCoroutine(rectTransform, target, duration));
        }

        /// <summary>
        /// Make a tweening transition for RectTransform's anchoredPosition.
        /// </summary>
        public static Coroutine TweenAnchoredPosition(this RectTransform rectTransform, Vector2 targetPosition, float duration)
        {
            return rectTransform.GetComponent<MonoBehaviour>()?.StartCoroutine(MTweenCoroutine.AnchoredPositionCoroutine(rectTransform, targetPosition, duration));
        }
        public static Coroutine TweenAnchoredPosition(this MonoBehaviour mono, RectTransform rectTransform, Vector2 targetPosition, float duration)
        {
            return mono.StartCoroutine(MTweenCoroutine.AnchoredPositionCoroutine(rectTransform, targetPosition, duration));
        }
    }

    public static class MTweenCoroutine
    {
        public static IEnumerator FillAmountTweenCoroutine(Image image, float startFillAmount, float targetFillAmount, float duration, Action onComplete)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float normalizedTime = Mathf.Clamp01(elapsedTime / duration);
                float interpolatedFillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, normalizedTime);

                image.fillAmount = interpolatedFillAmount;

                yield return null;
            }

            image.fillAmount = targetFillAmount;

            onComplete?.Invoke();
        }

        public static IEnumerator ColorCoroutine(Image image, Color color1, Color color2, float duration)
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

        public static IEnumerator ColorCoroutine(TextMeshProUGUI tmp, Color color1, Color color2, float duration)
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

        public static IEnumerator RectTransformCoroutine(RectTransform rectTransform, RectTransform target, float duration)
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
    
        public static IEnumerator AnchoredPositionCoroutine(RectTransform rectTransform, Vector2 targetPositoin, float duration)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float normalizedTime = Mathf.Clamp01(elapsedTime / duration);

                rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPositoin, normalizedTime);
                yield return null;
            }

            rectTransform.anchoredPosition = targetPositoin;
        }
    }
}