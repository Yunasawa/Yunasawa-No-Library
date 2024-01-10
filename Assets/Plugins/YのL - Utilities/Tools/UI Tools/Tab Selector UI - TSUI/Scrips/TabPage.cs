using UnityEngine;

namespace YNL.Tools.UI
{
    [AddComponentMenu("YのL/Tools/UI/Tab Selector UI/Tab Page")]
    public class TabPage : MonoBehaviour
    {
        #region ▶ Properties
        [Space(10)]
        public ETabState TabState = ETabState.Deselected;
        #endregion

        #region ▶ Tab State Functions
        public void OnSelect()
        {
            if (TabState == ETabState.Selected) return;

            TabState = ETabState.Selected;
        }
        public void OnDeselect()
        {
            if (TabState == ETabState.Deselected) return;

            TabState = ETabState.Deselected;
        }
        #endregion
    }
}