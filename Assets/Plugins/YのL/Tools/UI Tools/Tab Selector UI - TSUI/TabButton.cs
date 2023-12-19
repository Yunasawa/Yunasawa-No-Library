using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YNL.Tools.UI
{
    [AddComponentMenu("YNL/Tools/UI/Tab Selector UI/Tab Button")]
    public class TabButton : MonoBehaviour
    {
        private TabManager _tabSelectorManager;
        private ITabSelectable _thisTabSelectable;
        private Button _thisButton;

        public TabState TabState = TabState.Deselected;

        protected void OnValidate()
        {
            if (_thisButton == null)
            {
                if (GetComponent<Button>() == null) _thisButton = this.gameObject.AddComponent<Button>();
                else _thisButton = GetComponent<Button>();
            }
        }

        protected void OnEnable()
        {
            _tabSelectorManager = this.transform.parent.GetComponent<TabManager>();
            _thisTabSelectable = this.GetComponent<ITabSelectable>();

            _thisButton.onClick.AddListener(OnClick);

            if (_tabSelectorManager.CurrentSelectedTab != null)
            {
                if (_tabSelectorManager.CurrentSelectedTab == this) this.TabState = TabState.Selected;
                else this.TabState = TabState.Deselected;
            }

            if (this.TabState == TabState.Selected) _tabSelectorManager.UpdateTabState(this);
        }

        #region Tab State Functions: Selected/Deselected
        public void OnSelectedUpdate()
        {
            _thisTabSelectable?.Selected();
        }
        public void OnDeselectedUpdate()
        {
            _thisTabSelectable?.Deselected();
        }
        #endregion

        public void OnClick()
        {
            if (_thisTabSelectable != null) _thisTabSelectable.SelectingEvent();
            _tabSelectorManager.UpdateTabState(this);
        }

    }

    [SerializeField]
    public enum TabState
    {
        Selected, Deselected,
    }
}
