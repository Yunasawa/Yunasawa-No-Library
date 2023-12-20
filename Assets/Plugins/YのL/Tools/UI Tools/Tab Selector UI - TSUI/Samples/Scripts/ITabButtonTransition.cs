using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YNL.Tools.UI;

public class ITabButtonTransition : MonoBehaviour, ITabActionable
{
    [SerializeField] private bool _enable;
    [Space(10)]
    [SerializeField] private Image _thisImage;
    [SerializeField] private Color _unSelectColor;
    [SerializeField] private Color _selectColor;

    private void OnValidate()
    {
        if (_thisImage == null) _thisImage = GetComponent<Image>();
    }

    private void Awake()
    {
        if (_thisImage == null) _thisImage = GetComponent<Image>();
    }

    public void Select()
    {
        if (!_enable) return;

        _thisImage?.TweenColor(_thisImage.color, _selectColor, 0.25f);
    }

    public void Deselect()
    {
        if (!_enable) return;

        _thisImage?.TweenColor(_thisImage.color, _unSelectColor, 0.25f);
    }
}
