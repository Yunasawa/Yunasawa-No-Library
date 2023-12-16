using UnityEngine;
using UnityEngine.EventSystems;

namespace YNL.Tools.UI
{
    [AddComponentMenu("YNL/Tools/UI/Tab Selector UI/Tab Button")]
    public class TabButton : PointableUI, IPointerClickHandler
    {
        private TabManager _tabSelectorManager;
        private ITabSelectable _thisTabSelectable;

        public TabState TabState = TabState.Deselected;

        protected void OnEnable()
        {
            _tabSelectorManager = this.transform.parent.GetComponent<TabManager>();
            _thisTabSelectable = this.GetComponent<ITabSelectable>();

            this.LeftClick.AddListener(OnClick);

            if (_tabSelectorManager.CurrentSelectedTag != null)
            {
                if (_tabSelectorManager.CurrentSelectedTag == this) this.TabState = TabState.Selected;
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
            _thisTabSelectable.SelectingEvent();
            _tabSelectorManager.UpdateTabState(this);
        }

    }

    [SerializeField]
    public enum TabState
    {
        Selected, Deselected,
    }
}
