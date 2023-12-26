using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YNL.Extension.Method;
using YNL.Utilities;

namespace YNL.Tools.UI
{
    public class ImageTransition : TransitivableUI
    {
        private List<Coroutine> _coroutines = new();
        public TweenType TransitionType = TweenType.ExponentialInterpolation;
        [Space(10)]
        public bool UsingGeneralDuration = true;
        public float GeneralDuration = 1;
        [Space(10)]
        [SerializeField] private ImageTransitionMode _transition;
        bool _spriteTransition => _transition.Contain(ImageTransitionMode.Sprite);
        bool _colorTransition => _transition.Contain(ImageTransitionMode.Color);
        bool _grayscaleTransition => _transition.Contain(ImageTransitionMode.Grayscale);


        [ShowIf("_spriteTransition", Value = true), SerializeField] private List<ImageTransitionSprite> _spriteTransitions;
        [ShowIf("_colorTransition", Value = true), SerializeField] private List<ImageTransitionColors> _colorTransitions;
        [ShowIf("_grayscaleTransition", Value = true), SerializeField] private List<ImageTransitionGrayscale> _grayscaleTransitions;

        public override void OnChange(string key)
        {
            this.StopCoroutines(_coroutines);

            if (_spriteTransition)
            {
                foreach (var transition in _spriteTransitions) transition.Image.sprite = transition.Sprite[key];
            }
            if (_colorTransition)
            {
                foreach (var transition in _colorTransitions)
                {
                    _coroutines.Add(this.TweenColor(transition.Image, transition.Color[key], UsingGeneralDuration ? GeneralDuration : transition.Duration));
                }
            }
            if (_grayscaleTransition)
            {
                foreach (var transition in _grayscaleTransitions)
                {
                    if (!transition.EffectAmount[key]) transition.Image.material = transition.TransitionMaterial;
                    _coroutines.Add(this.TweenMaterial(transition.TransitionMaterial, "_EffectAmount", transition.EffectAmount[key] ? 1 : 0, UsingGeneralDuration ? GeneralDuration : transition.Duration, () => 
                    {
                        if (transition.EffectAmount[key]) transition.Image.material = transition.OriginalMaterial;
                    }));
                }
            }
        }
    }

    [System.Flags]
    public enum ImageTransitionMode
    {
        None,
        Sprite = 1,
        Color = 1 << 2,
        Grayscale = 1 << 3
    }

    [System.Serializable]
    public class ImageTransitionColors
    {
        public Image Image;
        public SerializableDictionary<string, Color> Color = new();
        public float Duration = 1;
    }

    [System.Serializable]
    public class ImageTransitionSprite
    {
        public Image Image;
        public SerializableDictionary<string, Sprite> Sprite = new();
    }

    [System.Serializable]
    public class ImageTransitionGrayscale
    {
        public Image Image;
        public Material OriginalMaterial;
        [Tooltip("Recommend to create an instance of Sprite-Grayscale material foreach Grayscale Transition")]
        public Material TransitionMaterial;
        public SerializableDictionary<string, bool> EffectAmount = new();
        public float Duration = 1;
    }

}