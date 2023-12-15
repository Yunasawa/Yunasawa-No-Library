using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Yunasawa.Utilities.UI;

namespace Yunasawa.Utilities.TSUI
{
    [AddComponentMenu("Yunasawa/Tab Selector UI/Tab Button")]
    public class TabButton : PointerUI, IPointerClickHandler
    {
        private TabManager _tabSelectorManager;
        private ITabSelectable _thisTabSelectable;

        public TabState TabState = TabState.Deselected;

        private void Awake()
        {
            _tabSelectorManager = this.transform.parent.GetComponent<TabManager>();
            _thisTabSelectable = this.GetComponent<ITabSelectable>();

            this.LeftClick.AddListener(OnLeftClicked);
            this.RightClick.AddListener(OnRightClicked);
            this.MiddleClick.AddListener(OnMiddleClicked);

            if (_tabSelectorManager.CurrentSelectedTag != null)
            {
                if (_tabSelectorManager.CurrentSelectedTag == this) this.TabState = TabState.Selected;
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

        #region Tab Button Functions: LeftClicked, RightClicked, MiddleClicked
        private void OnLeftClicked()
        {
            _thisTabSelectable.SelectingEvent();
            _tabSelectorManager.UpdateTabState(this);
        }
        private void OnRightClicked()
        {
            
        }
        private void OnMiddleClicked()
        {

        }
        #endregion

    }

    [SerializeField]
    public enum TabState
    {
        Selected, Deselected,
    }
}
