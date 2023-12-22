using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YNL.Extension.Method;

namespace YNL.Tools.UI
{
    [AddComponentMenu("YNL/Tools/UI/Tab Selector UI/Tab Button")]
    public class TabButton : MonoBehaviour
    {
        #region ▶ Properties
        public string Label = "";

        [Space(10)]
        [SerializeField] private TabManager _tabManager;
        private ITabActionable _thisTabSelectable;
        private Button _thisButton;

        [Space]
        public ETabState TabState = ETabState.Deselected;

        #endregion

        #region ▶ Monobehaviour
        protected void OnValidate()
        {
            if (Label == "") Label = gameObject.name;

            if (_thisButton == null) _thisButton = this.gameObject.GetOrAddComponent<Button>();
            if (_thisTabSelectable == null) _thisTabSelectable = this.GetComponent<ITabActionable>();
            if (_tabManager == null) _tabManager = this.transform.parent.GetComponent<TabManager>();
        }

        protected void Awake()
        {
            if (_thisTabSelectable == null) _thisTabSelectable = this.GetComponent<ITabActionable>();
            if (_tabManager == null) _tabManager = this.transform.parent.GetComponent<TabManager>();
            _thisButton.transition = Selectable.Transition.None;

            _thisButton.onClick.AddListener(OnClick);
        }

        private void OnEnable()
        {
            if (TabState == ETabState.Selected) OnSelect();
            if (TabState == ETabState.Deselected) OnDeselect();
        }
        #endregion

        #region ▶ Tab State Functions: Select/Deselect/OnClick
        public void OnSelect()
        {
            TabState = ETabState.Selected;
            _thisTabSelectable?.Select();
        }
        public void OnDeselect()
        {
            TabState = ETabState.Deselected;
            _thisTabSelectable?.Deselect();
        }

        public void OnClick()
        {
            if (TabState == ETabState.Deselected) _tabManager.TabSelected(this);
        }

        public void ForceSelect() => OnClick();
        #endregion   

    }

    [SerializeField]
    public enum ETabState
    {
        Selected, Deselected,
    }
}
