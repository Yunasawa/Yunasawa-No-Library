using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YNL.Tools.UI;
using TMPro;
using Sirenix.OdinInspector;
using YNL.Attribute;

public class TabButtonTransition : MonoBehaviour, ITabSelectable
{
    [SerializeField] private TabTransition _tabTransition = TabTransition.ColorAndTransform;

    [Title("Color")]
    [ShowIf("_tabTransition", Value = TabTransition.ColorAndTransform), SerializeField] 
    private List<ImageTransitionColor> _imageTransition = new();
    [ShowIf("_tabTransition", Value = TabTransition.ColorAndTransform), SerializeField] 
    private List<TMPTransitionColor> _tmpTransition = new();

    [Title("Transform")]
    [ShowIf("_tabTransition", Value = TabTransition.ColorAndTransform)]
    [SerializeField] private List<ObjectTransitionPosition> _transformTransition = new();

    [Title("Animation")]
    [ShowIf("_tabTransition", Value = TabTransition.Animation), SerializeField] private string _selectAnimation = "Select";
    [ShowIf("_tabTransition", Value = TabTransition.Animation), SerializeField] private string _deselectAnimation = "Deselect";


    public void Select()
    {
        foreach (var transition in _imageTransition)
        {
            transition.Image?.TweenColor(transition.Image.color, transition.SelectColor, transition.Duration);
        }
        foreach (var transition in _tmpTransition)
        {
            transition.TMP?.TweenColor(transition.TMP.color, transition.SelectColor, transition.Duration);
        }
        foreach (var transition in _transformTransition)
        {
            transition.Transform?.TweenRectTransform(transition.SelectTransform, transition.Duration);
        }
    }

    public void Deselect()
    {
        foreach (var transition in _imageTransition)
        {
            transition.Image?.TweenColor(transition.Image.color, transition.DeselectColor, transition.Duration);
        }
        foreach (var transition in _tmpTransition)
        {
            transition.TMP?.TweenColor(transition.TMP.color, transition.DeselectColor, transition.Duration);
        }
        foreach (var transition in _transformTransition)
        {
            transition.Transform?.TweenRectTransform(transition.DeselectTransform, transition.Duration);
        }
    }
}

[System.Serializable]
public class ImageTransitionColor
{
    public Image Image;
    public Color SelectColor = Color.white;
    public Color DeselectColor = Color.white;
    public float Duration = 0.25f;
}

[System.Serializable]
public class TMPTransitionColor
{
    public TextMeshProUGUI TMP;
    public Color SelectColor = Color.white;
    public Color DeselectColor = Color.white;
    public float Duration = 0.25f;
}

[System.Serializable]
public class ObjectTransitionPosition
{
    public RectTransform Transform;
    public RectTransform SelectTransform;
    public RectTransform DeselectTransform;
    public float Duration = 0.25f;
}

public enum TabTransition
{
    None, ColorAndTransform, Animation
}