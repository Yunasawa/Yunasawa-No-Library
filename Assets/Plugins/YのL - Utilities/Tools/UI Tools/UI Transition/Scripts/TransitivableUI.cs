using UnityEngine;

namespace YNL.Tools.UI
{
    public abstract class TransitivableUI : MonoBehaviour
    {
        public abstract void OnChange(string key);
    }
}
