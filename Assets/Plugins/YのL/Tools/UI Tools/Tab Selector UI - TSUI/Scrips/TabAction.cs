using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YNL.Tools.UI;
using TMPro;
using Sirenix.OdinInspector;
using YNL.Attribute;
using System;
using UnityEngine.Events;

namespace YNL.Tools.UI
{
    public class TabAction : MonoBehaviour, ITabActionable
    {
        #region ▶ Properties
        #region ▶ Tab Event
        [Title("Tab Event")]
        [SerializeField] private ETabEvent _tabEvent = ETabEvent.None;
        [Space()]
        [ShowIf("_tabEvent", Value = ETabEvent.Invoke), SerializeField] private UnityEvent OnSelect;
        [ShowIf("_tabEvent", Value = ETabEvent.Invoke), SerializeField] private UnityEvent OnDeselect;
        #endregion

        #region ▶ Tab Transition
        [Title("Tab Transition")]
        [SerializeField] private ETabTransition _tabTransition = ETabTransition.ColorAndTransform;

        [Title("Color")]
        [ShowIf("_tabTransition", Value = ETabTransition.ColorAndTransform), SerializeField]
        private List<ImageTransitionColor> _imageTransition = new();
        [ShowIf("_tabTransition", Value = ETabTransition.ColorAndTransform), SerializeField]
        private List<TMPTransitionColor> _tmpTransition = new();

        [Title("Transform")]
        [ShowIf("_tabTransition", Value = ETabTransition.ColorAndTransform)]
        [SerializeField] private List<ObjectTransitionPosition> _transformTransition = new();

        [Title("Animation")]
        private Animator _animator;
        [ShowIf("_tabTransition", Value = ETabTransition.Animation), SerializeField] private string _selectAnimation = "Select";
        [ShowIf("_tabTransition", Value = ETabTransition.Animation), SerializeField] private string _deselectAnimation = "Deselect";
        #endregion

        #endregion

        #region ▶ Methods
        private void OnValidate()
        {
            if (_tabTransition == ETabTransition.Animation)
            {
                if (GetComponent<Animator>().IsNull()) MDebug.Warning("This object must have Animator component to use Animation transition!");
                else if (_animator.IsNull()) _animator = GetComponent<Animator>();
            }
        }

        public void Select()
        {
            if (_tabEvent == ETabEvent.Invoke) OnSelect?.Invoke();

            if (_tabTransition == ETabTransition.ColorAndTransform)
            {
                foreach (var transition in _imageTransition)
                {
                    transition.Image?.TweenColor(transition.Image.color, transition.SelectColor, transition.Duration);
                }
                foreach (var transition in _tmpTransition)
                {
                    transition.TMP?.TweenColor(transition.TMP.color, transition.SelectColor, transition.Duration);
                }
                foreach (var transition in _transformTransition)
                {
                    transition.Transform?.TweenRectTransform(transition.SelectTransform, transition.Duration);
                }
            }

            else if (_tabTransition == ETabTransition.Animation)
            {
                _animator.Play(_selectAnimation);
            }
        }

        public void Deselect()
        {
            if (_tabEvent == ETabEvent.Invoke) OnDeselect?.Invoke();

            if (_tabTransition == ETabTransition.ColorAndTransform)
            {
                foreach (var transition in _imageTransition)
                {
                    transition.Image?.TweenColor(transition.Image.color, transition.DeselectColor, transition.Duration);
                }
                foreach (var transition in _tmpTransition)
                {
                    transition.TMP?.TweenColor(transition.TMP.color, transition.DeselectColor, transition.Duration);
                }
                foreach (var transition in _transformTransition)
                {
                    transition.Transform?.TweenRectTransform(transition.DeselectTransform, transition.Duration);
                }
            }

            else if (_tabTransition == ETabTransition.Animation)
            {
                _animator.Play(_deselectAnimation);
            }
        }
        #endregion
    }

    [System.Serializable]
    public class ImageTransitionColor
    {
        public Image Image;
        public Color SelectColor = Color.white;
        public Color DeselectColor = Color.white;
        public float Duration = 0.25f;
    }

    [System.Serializable]
    public class TMPTransitionColor
    {
        public TextMeshProUGUI TMP;
        public Color SelectColor = Color.white;
        public Color DeselectColor = Color.white;
        public float Duration = 0.25f;
    }

    [System.Serializable]
    public class ObjectTransitionPosition
    {
        public RectTransform Transform;
        public RectTransform SelectTransform;
        public RectTransform DeselectTransform;
        public float Duration = 0.25f;
    }
    public enum ETabEvent
    {
        None, Invoke
    }

    public enum ETabTransition
    {
        None, ColorAndTransform, Animation
    }
}