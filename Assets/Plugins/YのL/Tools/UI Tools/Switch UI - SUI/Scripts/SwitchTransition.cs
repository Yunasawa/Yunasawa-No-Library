using Microsoft.Win32.SafeHandles;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YNL.Extension.Constant;
using YNL.Extension.Method;
using YNL.Extension.Objects;

namespace YNL.Tools.UI
{
    public class SwitchTransition : MonoBehaviour, ISwitchable
    {
        #region ▶ Properties
        private List<Coroutine> _coroutineList = new();

        [SerializeField] private ImageTransitionColor _switchFrame;
        [SerializeField] private RectTransform _switchToggle;

        [SerializeField] private float _duration = 0.2f;

        private Vector2 _offingTogglePosition;
        #endregion

        private void Start()
        {
            _offingTogglePosition = _switchToggle.anchoredPosition;
        }

        public void SwitchOn()
        {
            StopTransitionCoroutine();

            _coroutineList.Add(this.TweenColor(_switchFrame.Image, _switchFrame.Image.color, _switchFrame.Select, _switchFrame.Duration));
            _coroutineList.Add(this.TweenAnchoredPosition(_switchToggle, _offingTogglePosition * -1, _duration));
        }

        public void SwitchOff()
        {
            StopTransitionCoroutine();

            _coroutineList.Add(this.TweenColor(_switchFrame.Image, _switchFrame.Image.color, _switchFrame.Deselect, _switchFrame.Duration));
            _coroutineList.Add(this.TweenAnchoredPosition(_switchToggle, _offingTogglePosition, _duration));
        }

        public void StopTransitionCoroutine()
        {
            foreach (var coroutine in _coroutineList) this.StopACoroutine(coroutine);
        }
    }
}