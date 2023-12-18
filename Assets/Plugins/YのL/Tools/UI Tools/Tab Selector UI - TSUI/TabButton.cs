using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YNL.Tools.UI
{
    [AddComponentMenu("YNL/Tools/UI/Tab Selector UI/Tab Button")]
    public class TabButton : PointableUI, IPointerClickHandler
    {
        private TabManager _tabSelectorManager;
        private ITabSelectable _thisTabSelectable;

        public TabState TabState = TabState.Deselected;

        protected override void OnValidate()
        {
            base.OnValidate();

            Button button = GetComponent<Button>();
            if (button != null) button.DestroyOnValidate();
        }

        protected void OnEnable()
        {
            _tabSelectorManager = this.transform.parent.GetComponent<TabManager>();
            _thisTabSelectable = this.GetComponent<ITabSelectable>();

            this.LeftClick.AddListener(OnClick);

            if (_tabSelectorManager.CurrentSelectedTab != null)
            {
                if (_tabSelectorManager.CurrentSelectedTab == this) this.TabState = TabState.Selected;
                else this.TabState = TabState.Deselected;
            }

            if (this.TabState == TabState.Selected) _tabSelectorManager.UpdateTabState(this);

            _ = Mode == PUIMode.OnlyClickButton;
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
