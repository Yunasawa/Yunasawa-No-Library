using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace YNL.Tools.UI
{
    [AddComponentMenu("YのL/Tools/UI/Switch UI")]
    public class SwitchUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        #region ▶ Properties
        [SerializeField] private bool _switchOn = false;

        private ISwitchable[] _switchTransition;

        [Title("Switch Event")]
        public UnityEvent OnSwitchOn;
        public UnityEvent OnSwitchOff;

        public UnityEvent<bool> OnEnter;
        public UnityEvent<bool> OnExit;
        #endregion

        private void Awake()
        {
            _switchTransition = GetComponents<ISwitchable>();
        }

        #region ▶ Pointer Handler Methods
        public void OnPointerClick(PointerEventData eventData)
        {
            Switch();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnEnter?.Invoke(_switchOn);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnExit?.Invoke(_switchOn);
        }
        #endregion

        #region ▶ Switch Handler Methods
        private void Switch()
        {
            _switchOn = !_switchOn;

            if (_switchOn) SwitchOn();
            else SwitchOff();
        }

        private void SwitchOn()
        {
            foreach (var switcher in _switchTransition) switcher.SwitchOn();
            OnSwitchOn?.Invoke();
        }

        private void SwitchOff()
        {
            foreach (var switcher in _switchTransition) switcher.SwitchOff();
            OnSwitchOff?.Invoke();
        }
        #endregion
    }
}