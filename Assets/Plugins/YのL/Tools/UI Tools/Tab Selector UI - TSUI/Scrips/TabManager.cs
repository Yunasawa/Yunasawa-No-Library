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
        #region ▶ Properties
        [Space(10)]
        [SerializeField] private ETabSelectorType _tabSelectorType = ETabSelectorType.SwitchTab;

        [Title("Switch Tab")]
        [ShowIfGroup("_tabSelectorType", value: ETabSelectorType.SwitchTab)]
        [SerializeField] private SerializableDictionary<TabButton, TabPage> _tabSelectionPair;
        public SerializableDictionary<TabButton, TabPage> TabSelectionPair => _tabSelectionPair;

        [ShowIfGroup("_tabSelectorType", value: ETabSelectorType.SwitchTab)]
        public TabButton CurrentSelectedTab;

        [PropertySpace(20)]
        [ShowIfGroup("_tabSelectorType", value: ETabSelectorType.SwitchTab)]
        [Button("Get All TSUI Buttons To Key")]
        public void GetAllTSUIButtonsToKey()
        {
            List<TabButton> tempList = this.GetComponentsInChildren<TabButton>().ToList();
            _tabSelectionPair.Clear();
            foreach (var i in tempList) _tabSelectionPair.Add(i, null);
        }
        #endregion

        #region ▶ Monobehaviour
        private void OnEnable()
        {
            if (CurrentSelectedTab == null) CurrentSelectedTab = TabSelectionPair.First().Key;

            TabSelected(CurrentSelectedTab);
        }
        #endregion

        #region ▶ Tab Manager Methods
        public void TabSelected(TabButton selected)
        {
            CurrentSelectedTab = selected;

            switch (_tabSelectorType)
            {
                case ETabSelectorType.SwitchTab:
                    foreach (var pair in _tabSelectionPair)
                    {
                        pair.Key.OnDeselect();
                        pair.Value.gameObject.SetActive(false);
                        pair.Value.OnDeselect();
                    }
                    CurrentSelectedTab.OnSelect();
                    _tabSelectionPair[CurrentSelectedTab].gameObject.SetActive(true);
                    _tabSelectionPair[CurrentSelectedTab].OnSelect();
                    break;
            }
        }

        public void ForceSelect(string label)
        {
            KeyValuePair<TabButton, TabPage> pair = _tabSelectionPair.FirstOrDefault(i => i.Key.Label == label);

            TabButton button = pair.Key;
            button.ForceSelect();
        }

        public void ForceSelect(TabButton button) => button.ForceSelect();
        #endregion
    }


    [System.Serializable]
    public enum ETabSelectorType
    {
        None, SwitchTab
    }
}