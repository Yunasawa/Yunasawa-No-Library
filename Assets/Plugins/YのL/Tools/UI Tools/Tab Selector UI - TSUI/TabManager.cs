using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YNL.Utilities;

namespace YNL.Tools.UI
{
    [AddComponentMenu("YNL/Tools/UI/Tab Selector UI/Tab Manager")]
    public class TabManager : MonoBehaviour
    {
        #region TSUI: Selectable Buttons
        [BoxGroup("TSUI: Selectable Buttons", centerLabel: true)]
        [SerializeField] private List<TabButton> _tabButtonList;
        public List<TabButton> TabButtonList => _tabButtonList;

        [BoxGroup("TSUI: Selectable Buttons", centerLabel: true)]
        public TabButton CurrentSelectedTab;

        [BoxGroup("TSUI: Selectable Buttons", centerLabel: true)]
        [Button("Get All TSUI Buttons")]
        public void GetAllTSUIButtons()
        {
            _tabButtonList = this.GetComponentsInChildren<TabButton>().ToList();
            CurrentSelectedTab = _tabButtonList[0];
        }
        #endregion

        [Space(10)]
        [SerializeField] private TabSelectorType _tabSelectorType;

        #region TSUI: Switch Tab
        [ShowIfGroup("_tabSelectorType", value: TabSelectorType.SwitchTab)]
        [BoxGroup("_tabSelectorType/TSUI: Switch Tab", centerLabel: true)]
        [SerializeField] private SerializableDictionary<TabButton, TabPage> _tabSelectionPair;
        public SerializableDictionary<TabButton, TabPage> TabSelectionPair => _tabSelectionPair;

        [BoxGroup("_tabSelectorType/TSUI: Switch Tab", centerLabel: true)]
        [Button("Get All TSUI Buttons To Key")]
        public void GetAllTSUIButtonsToKey()
        {
            List<TabButton> tempList = this.GetComponentsInChildren<TabButton>().ToList();
            _tabSelectionPair.Clear();
            foreach (var i in tempList) _tabSelectionPair.Add(i, null);
        }
        #endregion

        private void Update()
        {
            UpdateTagSelecting();
        }

        public void UpdateTabState(TabButton selected)
        {
            CurrentSelectedTab = selected;

            switch (_tabSelectorType)
            {
                case TabSelectorType.None:
                    foreach (var tag in _tabButtonList) tag.TabState = TabState.Deselected;
                    CurrentSelectedTab.TabState = TabState.Selected;
                    break;
                case TabSelectorType.SwitchTab:
                    foreach (var pair in _tabSelectionPair)
                    {
                        pair.Key.TabState = TabState.Deselected;
                        pair.Value.gameObject.SetActive(false);
                    }
                    CurrentSelectedTab.TabState = TabState.Selected;
                    _tabSelectionPair[CurrentSelectedTab].gameObject.SetActive(true);
                    break;
            }
        }

        private void UpdateTagSelecting()
        {
            foreach (var tag in _tabButtonList)
            {
                if (tag.TabState == TabState.Selected) tag.OnSelectedUpdate();
                else if (tag.TabState == TabState.Deselected) tag.OnDeselectedUpdate();
            }
        }
    }
}

[System.Serializable]
public enum TabSelectorType
{
    None, SwitchTab
}