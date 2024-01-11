using UnityEngine;

namespace YNL.Utilities
{
    [AddComponentMenu("Don't Destroy On Load")]
    public class DontDestroyOnLoad : MonoBehaviour
    {
        [SerializeField] private bool isDontDestroyOnLoad = true;

        private void Awake()
        {
            if (isDontDestroyOnLoad) DontDestroyOnLoad(this);
        }
    }
}