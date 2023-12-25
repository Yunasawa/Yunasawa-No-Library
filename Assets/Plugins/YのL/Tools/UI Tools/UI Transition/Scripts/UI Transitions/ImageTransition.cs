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
        [SerializeField] private ImageTransitionMode _transition;
        bool _spriteTransition => _transition.Contain(ImageTransitionMode.Sprite);
        bool _colorTransition => _transition.Contain(ImageTransitionMode.Color);
        bool _grayscaleTransition => _transition.Contain(ImageTransitionMode.Grayscale);


        [ShowIf("_spriteTransition", Value = true), SerializeField] private List<ImageTransitionSprite> _spriteTransitions;
        [ShowIf("_colorTransition", Value = true), SerializeField] private List<ImageTransitionColors> _colorTransitions;
        [ShowIf("_grayscaleTransition", Value = true), SerializeField] private List<ImageTransitionGrayscale> _grayscaleTransitions;

        public override void OnChange(string key)
        {
            if (_spriteTransition)
            {
                foreach (var transition in _spriteTransitions) transition.Image.sprite = transition.Sprite[key];
            }
            if (_colorTransition)
            {
                foreach (var transition in _colorTransitions)
                {
                    this.TweenColor(transition.Image, transition.Image.color, transition.Color[key], transition.Duration);
                }
            }
            if (_grayscaleTransition)
            {

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
        public float Duration = 0.2f;
    }

    [System.Serializable]
    public class ImageTransitionSprite
    {
        public Image Image;
        public SerializableDictionary<string, Sprite> Sprite = new();
        //public float Duration = 0.2f;
    }

    [System.Serializable]
    public class ImageTransitionGrayscale
    {
        public Image Image;
        public Material Grayscale;
        public float Duration = 0.2f;
    }

}