using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace YNL.Tools.UI
{
    public class SwitchUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private bool _switchOn = false;

        private ISwitchable[] _switchTransition;

        [Title("Switch Event")]
        public UnityEvent OnSwitchOn;
        public UnityEvent OnSwitchOff;

        private void Awake()
        {
            _switchTransition = GetComponents<ISwitchable>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Switch();
        }

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

    }
}