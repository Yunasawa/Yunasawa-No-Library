using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yunasawa.Utilities;

namespace Yunasawa.Utilities.TSUI
{
    [AddComponentMenu("Yunasawa/Tab Selector UI/Tab Manager")]
    public class TabManager : MonoBehaviour
    {
        #region TSUI: Selectable Buttons
        [BoxGroup("TSUI: Selectable Buttons", centerLabel: true)]
        [SerializeField] private List<TabButton> _tagButtonList;
        public List<TabButton> TabButtonList => _tagButtonList;

        [BoxGroup("TSUI: Selectable Buttons", centerLabel: true)]
        public TabButton CurrentSelectedTag;

        [BoxGroup("TSUI: Selectable Buttons", centerLabel: true)]
        [Button("Get All TSUI Buttons")]
        public void GetAllTSUIButtons()
        {
            _tagButtonList = this.GetComponentsInChildren<TabButton>().ToList();
        }
        #endregion

        [Space(10)]
        [SerializeField] private TagSelectorType _tagSelectorType;

        #region TSUI: Switch Tab
        [ShowIfGroup("_tagSelectorType", value: TagSelectorType.SwitchTab)]
        [BoxGroup("_tagSelectorType/TSUI: Switch Tab", centerLabel: true)]
        [SerializeField] private SerializableDictionary<TabButton, TabPage> _tabSelectionPair;
        public SerializableDictionary<TabButton, TabPage> TabSelectionPair => _tabSelectionPair;

        [BoxGroup("_tagSelectorType/TSUI: Switch Tab", centerLabel: true)]
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
            CurrentSelectedTag = selected;

            switch (_tagSelectorType)
            {
                case TagSelectorType.None:
                    foreach (var tag in _tagButtonList) tag.TabState = TabState.Deselected;
                    CurrentSelectedTag.TabState = TabState.Selected;
                    break;
                case TagSelectorType.SwitchTab:
                    foreach (var pair in _tabSelectionPair)
                    {
                        pair.Key.TabState = TabState.Deselected;
                        pair.Value.gameObject.SetActive(false);
                    }
                    CurrentSelectedTag.TabState = TabState.Selected;
                    _tabSelectionPair[CurrentSelectedTag].gameObject.SetActive(true);
                    break;
            }
        }

        private void UpdateTagSelecting()
        {
            foreach (var tag in _tagButtonList)
            {
                if (tag.TabState == TabState.Selected) tag.OnSelectedUpdate();
                else if (tag.TabState == TabState.Deselected) tag.OnDeselectedUpdate();
            }
        }
    }
}

[System.Serializable]
public enum TagSelectorType
{
    None, SwitchTab
}