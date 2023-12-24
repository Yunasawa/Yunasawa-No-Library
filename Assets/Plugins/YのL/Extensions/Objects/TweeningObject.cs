using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YNL.Extension.Objects
{
    [System.Serializable]
    public class ImageTransitionColor
    {
        public Image Image;
        public Color Select = Color.white;
        public Color Deselect = Color.white;
        public float Duration = 0.2f;
    }

    [System.Serializable]
    public class TMPTransitionColor
    {
        public TextMeshProUGUI TMP;
        public Color Select = Color.white;
        public Color Deselect = Color.white;
        public float Duration = 0.2f;
    }

    [System.Serializable]
    public class RectTransitionTransform
    {
        public RectTransform Transform;
        public RectTransform Select;
        public RectTransform Deselect;
        public float Duration = 0.2f;
    }
}