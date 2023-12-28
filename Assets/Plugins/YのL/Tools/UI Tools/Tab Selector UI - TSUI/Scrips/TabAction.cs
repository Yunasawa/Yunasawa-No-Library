using System.Collections.Generic;
using UnityEngine;
using YNL.Extension.Objects;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using YNL.Extension.Method;
using YNL.Extension.Constant;

namespace YNL.Tools.UI
{
    [AddComponentMenu("YのL/Tools/UI/Tab Selector UI/Tab Action")]
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
        [SerializeField] private EUITransition _tabTransition = EUITransition.ColorAndTransform;

        [Title("Color")]
        [ShowIf("_tabTransition", Value = EUITransition.ColorAndTransform), SerializeField]
        private List<ImageTransitionColor> _imageTransition = new();
        [ShowIf("_tabTransition", Value = EUITransition.ColorAndTransform), SerializeField]
        private List<TMPTransitionColor> _tmpTransition = new();

        [Title("Transform")]
        [ShowIf("_tabTransition", Value = EUITransition.ColorAndTransform)]
        [SerializeField] private List<RectTransitionTransform> _transformTransition = new();

        private Animator _animator;
        [Title("Animation")]
        [ShowIf("_tabTransition", Value = EUITransition.Animation), SerializeField] private string _selectAnimation = "Select";
        [ShowIf("_tabTransition", Value = EUITransition.Animation), SerializeField] private string _deselectAnimation = "Deselect";
        #endregion

        #endregion

        #region ▶ Methods
        private void OnValidate()
        {
            if (_tabTransition == Extension.Constant.EUITransition.Animation)
            {
                if (GetComponent<Animator>().IsNull()) MDebug.Warning("This object must have Animator component to use Animation transition!");
                else if (_animator.IsNull()) _animator = GetComponent<Animator>();
            }
        }

        public void Select()
        {
            if (_tabEvent == ETabEvent.Invoke) OnSelect?.Invoke();

            if (_tabTransition == Extension.Constant.EUITransition.ColorAndTransform)
            {
                foreach (var transition in _imageTransition)
                {
                    this.TweenColor(transition.Image, transition.Select, transition.Duration);
                }
                foreach (var transition in _tmpTransition)
                {
                    this.TweenColor(transition.TMP, transition.Select, transition.Duration);
                }
                foreach (var transition in _transformTransition)
                {
                    this.TweenRectTransform(transition.Transform, transition.Select, transition.Duration);
                }
            }

            else if (_tabTransition == Extension.Constant.EUITransition.Animation)
            {
                _animator.Play(_selectAnimation);
            }
        }

        public void Deselect()
        {
            if (_tabEvent == ETabEvent.Invoke) OnDeselect?.Invoke();

            if (_tabTransition == Extension.Constant.EUITransition.ColorAndTransform)
            {
                foreach (var transition in _imageTransition)
                {
                    this.TweenColor(transition.Image, transition.Deselect, transition.Duration);
                }
                foreach (var transition in _tmpTransition)
                {
                    this.TweenColor(transition.TMP, transition.Deselect, transition.Duration);
                }
                foreach (var transition in _transformTransition)
                {
                    this.TweenRectTransform(transition.Transform, transition.Deselect, transition.Duration);
                }
            }

            else if (_tabTransition == Extension.Constant.EUITransition.Animation)
            {
                _animator.Play(_deselectAnimation);
            }
        }
        #endregion
    }

    public enum ETabEvent
    {
        None, Invoke
    }
}