using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YNL.Tools.UI
{
    [AddComponentMenu("YNL/Tools/UI/Tab Selector UI/Tab Button")]
    public class TabButton : MonoBehaviour
    {
        #region ▶ Properties
        public string Label = "";

        [Space(10)]
        [SerializeField] private TabManager _tabManager;
        private ITabSelectable _thisTabSelectable;
        private Button _thisButton;

        [Space]
        public TabState TabState = TabState.Deselected;

        #endregion

        #region ▶ Monobehaviour
        protected void OnValidate()
        {
            if (Label == "") Label = gameObject.name;

            if (_thisButton == null)
            {
                if (GetComponent<Button>() == null) _thisButton = this.gameObject.AddComponent<Button>();
                else _thisButton = GetComponent<Button>();
            }
            if (_thisTabSelectable == null) _thisTabSelectable = this.GetComponent<ITabSelectable>();
            if (_tabManager == null) _tabManager = this.transform.parent.GetComponent<TabManager>();
        }

        protected void Awake()
        {
            if (_thisTabSelectable == null) _thisTabSelectable = this.GetComponent<ITabSelectable>();
            if (_tabManager == null) _tabManager = this.transform.parent.GetComponent<TabManager>();
            _thisButton.transition = Selectable.Transition.None;

            _thisButton.onClick.AddListener(OnClick);
        }

        private void OnEnable()
        {
            if (TabState == TabState.Selected) OnSelect();
            if (TabState == TabState.Deselected) OnDeselect();
        }
        #endregion

        #region ▶ Tab State Functions: Select/Deselect/OnClick
        public void OnSelect()
        {
            TabState = TabState.Selected;
            _thisTabSelectable?.Select();
        }
        public void OnDeselect()
        {
            TabState = TabState.Deselected;
            _thisTabSelectable?.Deselect();
        }

        public void OnClick()
        {
            if (TabState == TabState.Deselected) _tabManager.TabSelected(this);
        }

        public void ForceSelect() => OnClick();
        #endregion   

    }

    [SerializeField]
    public enum TabState
    {
        Selected, Deselected,
    }
}
