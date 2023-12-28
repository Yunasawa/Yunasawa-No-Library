using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;
using YNL.Tools.UI;

namespace YNL.Extension.Method
{
    public static class MTween
    {
        /// <summary>
        /// Make a tweening transition for Image's fillAmount.
        /// </summary>
        public static Coroutine TweenFillAmount(this MonoBehaviour mono, Image image, float targetFillAmount, float duration, Action onComplete = null)
        {
            return mono.StartCoroutine(MTweenCoroutine.FillAmountTweenCoroutine(image, image.fillAmount, targetFillAmount, duration, onComplete));
        }

        /// <summary>
        /// Make a tweening transition for Image's color.
        /// </summary>
        public static Coroutine TweenColor(this MonoBehaviour mono, Image image, Color target, float duration, TweenType tween = TweenType.ExponentialInterpolation)
        {
            return mono.StartACoroutine(MTweenCoroutine.ColorCoroutine(image, target, duration, tween));
        }

        /// <summary>
        /// Make a tweening transition for TextMeshPro's color.
        /// </summary>
        public static Coroutine TweenColor(this MonoBehaviour mono, TextMeshProUGUI tmp, Color color2, float duration, TweenType tween = TweenType.ExponentialInterpolation)
        {
            return mono.StartCoroutine(MTweenCoroutine.ColorCoroutine(tmp, color2, duration, tween));
        }

        /// <summary>
        /// Make a tweening transition for UI's RectTransform.
        /// </summary>
        public static Coroutine TweenRectTransform(this MonoBehaviour mono, RectTransform rectTransform, RectTransform target, float duration, TweenType tween = TweenType.ExponentialInterpolation)
        {
            return mono.StartCoroutine(MTweenCoroutine.RectTransformCoroutine(rectTransform, target, duration, tween));
        }

        /// <summary>
        /// Make a tweening transition for RectTransform's anchoredPosition.
        /// </summary>
        public static Coroutine TweenAnchoredPosition(this MonoBehaviour mono, RectTransform rectTransform, Vector2 targetPosition, float duration, TweenType tween = TweenType.ExponentialInterpolation)
        {
            return mono.StartCoroutine(MTweenCoroutine.AnchoredPositionCoroutine(rectTransform, targetPosition, duration, tween));
        }

        /// <summary>
        /// Make a tweening transition for RectTransform's localRotation.
        /// </summary>
        public static Coroutine TweenRotation(this MonoBehaviour mono, RectTransform rectTransform, Vector3 target, float duration, TweenType tween = TweenType.ExponentialInterpolation)
        {
            return mono.StartCoroutine(MTweenCoroutine.RotationCoroutine(rectTransform, target, duration, tween));
        }

        /// <summary>
        /// Make a tweening transition for RectTransform's localScale.
        /// </summary>
        public static Coroutine TweenScale(this MonoBehaviour mono, RectTransform rectTransform, Vector3 target, float duration, TweenType tween = TweenType.ExponentialInterpolation)
        {
            return mono.StartCoroutine(MTweenCoroutine.ScaleCoroutine(rectTransform, target, duration, tween));
        }
    
        public static Coroutine TweenSize(this MonoBehaviour mono, RectTransform rectTransform, Vector2 target, float duration, TweenType tween = TweenType.ExponentialInterpolation)
        {
            return mono.StartCoroutine(MTweenCoroutine.SizeCoroutine(rectTransform, target, duration, tween));
        }

        public static Coroutine TweenMaterial(this MonoBehaviour mono, Material material, string key, float target, float duration, Action onComplete = null, TweenType tween = TweenType.ExponentialInterpolation)
        {
            return mono.StartCoroutine(MTweenCoroutine.MaterialCoroutine(material, key, target, duration, onComplete, tween));
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

        public static IEnumerator ColorCoroutine(Image image, Color target, float duration, TweenType tween)
        {
            float elapsedTime = 0f;
            Color start = image.color;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float normalizedTime = Mathf.Clamp01(elapsedTime / duration);

                if (tween == TweenType.ExponentialInterpolation) image.color = Color.Lerp(image.color, target, normalizedTime);
                if (tween == TweenType.LinearInterpolation) image.color = Color.Lerp(start, target, normalizedTime);

                yield return null;
            }

            image.color = target;
        }

        public static IEnumerator ColorCoroutine(TextMeshProUGUI tmp, Color target, float duration, TweenType tween)
        {
            float elapsedTime = 0f;
            Color start = tmp.color;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float normalizedTime = Mathf.Clamp01(elapsedTime / duration);

                if (tween == TweenType.ExponentialInterpolation) tmp.color = Color.Lerp(tmp.color, target, normalizedTime);
                if (tween == TweenType.LinearInterpolation) tmp.color = Color.Lerp(start, target, normalizedTime);

                yield return null;
            }

            tmp.color = target;
        }

        public static IEnumerator RectTransformCoroutine(RectTransform rectTransform, RectTransform target, float duration, TweenType tween)
        {
            float elapsedTime = 0f;
            Vector2 startPosition = rectTransform.anchoredPosition;
            Vector3 startScale = rectTransform.localScale;
            Quaternion startRotation = rectTransform.localRotation;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float normalizedTime = Mathf.Clamp01(elapsedTime / duration);

                if (tween == TweenType.ExponentialInterpolation) rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, target.anchoredPosition, normalizedTime);
                if (tween == TweenType.LinearInterpolation) rectTransform.anchoredPosition = Vector2.Lerp(startPosition, target.anchoredPosition, normalizedTime);

                if (tween == TweenType.ExponentialInterpolation) rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, target.localScale, normalizedTime);
                if (tween == TweenType.LinearInterpolation) rectTransform.localScale = Vector3.Lerp(startScale, target.localScale, normalizedTime);
                
                if (tween == TweenType.ExponentialInterpolation) rectTransform.localRotation = Quaternion.Slerp(rectTransform.localRotation, target.localRotation, normalizedTime);
                if (tween == TweenType.LinearInterpolation) rectTransform.localRotation = Quaternion.Slerp(startRotation, target.localRotation, normalizedTime);

                yield return null;
            }

            rectTransform.anchoredPosition = target.anchoredPosition;
            rectTransform.localScale = target.localScale;
            rectTransform.localRotation = target.localRotation;
        }
    
        public static IEnumerator AnchoredPositionCoroutine(RectTransform rectTransform, Vector2 targetPositoin, float duration, TweenType tween)
        {
            float elapsedTime = 0f;
            Vector2 start = rectTransform.anchoredPosition;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float normalizedTime = Mathf.Clamp01(elapsedTime / duration);

                if (tween == TweenType.ExponentialInterpolation) rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPositoin, normalizedTime);
                if (tween == TweenType.LinearInterpolation) rectTransform.anchoredPosition = Vector2.Lerp(start, targetPositoin, normalizedTime);
                yield return null;
            }

            rectTransform.anchoredPosition = targetPositoin;
        }

        public static IEnumerator RotationCoroutine(RectTransform rectTransform, Vector3 target, float duration, TweenType tween)
        {
            float elapsedTime = 0f;
            Quaternion start = rectTransform.localRotation;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float normalizedTime = Mathf.Clamp01(elapsedTime / duration);

                if (tween == TweenType.ExponentialInterpolation) rectTransform.localRotation = Quaternion.Lerp(rectTransform.localRotation, Quaternion.Euler(target.x, target.y, target.z) , normalizedTime);
                if (tween == TweenType.LinearInterpolation) rectTransform.localRotation = Quaternion.Lerp(start, Quaternion.Euler(target.x, target.y, target.z) , normalizedTime);
                
                yield return null;
            }

            rectTransform.localRotation = Quaternion.Euler(target.x, target.y, target.z);
        }

        public static IEnumerator ScaleCoroutine(RectTransform rectTransform, Vector3 target, float duration, TweenType tween)
        {
            float elapsedTime = 0f;
            Vector3 start = rectTransform.localScale;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float normalizedTime = Mathf.Clamp01(elapsedTime / duration);

                if (tween == TweenType.ExponentialInterpolation) rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, target, normalizedTime);
                if (tween == TweenType.LinearInterpolation) rectTransform.localScale = Vector3.Lerp(start, target, normalizedTime);

                yield return null;
            }

            rectTransform.localScale = target;
        }

        public static IEnumerator SizeCoroutine(RectTransform rectTransform, Vector2 target, float duration, TweenType tween)
        {
            float elapsedTime = 0f;
            Vector2 start = rectTransform.sizeDelta;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float normalizedTime = Mathf.Clamp01(elapsedTime / duration);

                if (tween == TweenType.ExponentialInterpolation) rectTransform.sizeDelta = Vector2.Lerp(rectTransform.sizeDelta, target, normalizedTime);
                if (tween == TweenType.LinearInterpolation) rectTransform.sizeDelta = Vector2.Lerp(start, target, normalizedTime);

                yield return null;
            }

            rectTransform.sizeDelta = target;
        }

        public static IEnumerator MaterialCoroutine(Material material, string key, float target, float duration, Action onComplete, TweenType tween)
        {
            float elapsedTime = 0f, normalizedTime, start = material.GetFloat(key);

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                normalizedTime = Mathf.Clamp01(elapsedTime / duration);

                if (tween == TweenType.ExponentialInterpolation) material.SetFloat(key, Mathf.Lerp(material.GetFloat(key), target, normalizedTime));
                if (tween == TweenType.LinearInterpolation) material.SetFloat(key, Mathf.Lerp(start, target, normalizedTime));

                yield return null;
            }

            material.SetFloat(key, target);
            onComplete?.Invoke();
        }
    }
}