using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YNL.Extension.Method;
using YNL.Utilities;

namespace YNL.Tools.UI
{
    public class RectTransformTransition : TransitivableUI
    {
        private List<Coroutine> _coroutines = new();
        public TweenType TransitionType = TweenType.ExponentialInterpolation;
        [Space(10)]
        public bool UsingGeneralDuration = true;
        public float GeneralDuration = 1;
        [Space(10)]
        [SerializeField] private RectTransformTransitionMode _transition;
        bool _positionTransition => _transition.Contain(RectTransformTransitionMode.Position);
        bool _rotationTransition => _transition.Contain(RectTransformTransitionMode.Rotation);
        bool _scaleTransition => _transition.Contain(RectTransformTransitionMode.Scale);
        bool _sizeTransition => _transition.Contain(RectTransformTransitionMode.Size);

        [ShowIf("_positionTransition", Value = true), SerializeField] private List<RectTransitionPosition> _positionTransitions;
        [ShowIf("_rotationTransition", Value = true), SerializeField] private List<RectTransitionRotation> _rotationTransitions;
        [ShowIf("_scaleTransition", Value = true), SerializeField] private List<RectTransitionScale> _scaleTransitions;
        [ShowIf("_sizeTransition", Value = true), SerializeField] private List<RectTransitionSize> _sizeTransitions;

        public override void OnChange(string key)
        {
            this.StopCoroutines(_coroutines);

            if (_positionTransition)
            {
                foreach (var transition in _positionTransitions)
                {
                    if (transition.Position.ContainsKey(key)) _coroutines.Add(this.TweenAnchoredPosition(transition.RectTransform, transition.Position[key] * (transition.Scalable ? transition.RectTransform.localScale : Vector3.one), UsingGeneralDuration ? GeneralDuration : transition.Duration));
                }
            }
            if (_rotationTransition)
            {
                foreach (var transition in _rotationTransitions)
                {
                    if (transition.Rotation.ContainsKey(key)) _coroutines.Add(this.TweenRotation(transition.RectTransform, transition.Rotation[key], UsingGeneralDuration ? GeneralDuration : transition.Duration));
                }
            }
            if (_scaleTransition)
            {
                foreach (var transition in _scaleTransitions)
                {
                    if (transition.Scale.ContainsKey(key))
                    {
                        if (transition.Scale.ContainsKey(key))
                        {
                            Vector3 scaleRate = transition.Scale[key].Scale.DividedBy(transition.RectTransform.localScale);
                            _coroutines.Add(this.TweenAnchoredPosition(transition.RectTransform, transition.RectTransform.anchoredPosition * scaleRate, UsingGeneralDuration ? GeneralDuration : transition.Duration));
                        }
                        _coroutines.Add(this.TweenScale(transition.RectTransform, transition.Scale[key].Scale, UsingGeneralDuration ? GeneralDuration : transition.Duration));
                    }
                }
            }     
            if (_sizeTransition)
            {

            }
        }
    }

    [System.Flags]
    public enum RectTransformTransitionMode
    {
        None,
        Position = 1,
        Rotation = 1 << 2,
        Scale = 1 << 3,
        Size = 1 << 4
    }

    [System.Serializable]
    public class RectTransitionPosition
    {
        public RectTransform RectTransform;
        public SerializableDictionary<string, Vector2> Position = new();
        [Tooltip("With this be \"true\", position will be scaled with RectTrasform's localScale")]
        public bool Scalable = true;
        public float Duration = 1;
    }

    [System.Serializable]
    public class RectTransitionRotation
    {
        public RectTransform RectTransform;
        public SerializableDictionary<string, Vector3> Rotation = new();
        public float Duration = 1;
    }

    [System.Serializable]
    public class RectTransitionScale
    {
        public RectTransform RectTransform;
        [Tooltip("Tick \"Scalable\" as true to scale RectTransform's anchoredPosition on Event")]
        public SerializableDictionary<string, ScalablePostion> Scale = new();
        public float Duration = 1;
    }

    [System.Serializable]
    public class RectTransitionSize
    {
        public RectTransform RectTransform;
        public SerializableDictionary<string, Vector2> Size = new();
        public float Duration = 1;
    }

    [System.Serializable]
    public class ScalablePostion
    {
        public bool ScalePosition = true;
        public Vector3 Scale = Vector3.one;
    }
}