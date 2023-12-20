using UnityEngine;

namespace YNL.Tools.UI
{
    [AddComponentMenu("YNL/Tools/UI/Tab Selector UI/Tab Page")]
    public class TabPage : MonoBehaviour
    {
        #region ▶ Properties
        public string Label = "";

        [Space(10)]
        public TabState TabState = TabState.Deselected;
        #endregion

        #region ▶ Monobehaviour
        private void OnValidate()
        {
            if (Label == "") Label = gameObject.name;
        }
        #endregion

        #region ▶ Tab State Functions
        public void OnSelect()
        {
            if (TabState == TabState.Selected) return;

            TabState = TabState.Selected;
        }
        public void OnDeselect()
        {
            if (TabState == TabState.Deselected) return;

            TabState = TabState.Deselected;
        }
        #endregion
    }
}