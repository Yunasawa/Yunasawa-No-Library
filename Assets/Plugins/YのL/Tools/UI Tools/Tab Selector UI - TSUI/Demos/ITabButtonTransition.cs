using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YNL.Tools.UI;

public class ITabButtonTransition : MonoBehaviour, ITabSelectable
{
    [SerializeField] private Image _thisImage;
    [SerializeField] private Color _unSelectColor;
    [SerializeField] private Color _selectColor;

    private void Start()
    {
        _thisImage = GetComponent<Image>();
    }

    public void Selected()
    {
        //_thisImage.TweenColor(_unSelectColor, _selectColor, 0.25f);
    }

    public void Deselected()
    {
        //_thisImage.TweenColor(_selectColor, _unSelectColor, 0.25f); ;
    }

    public void SelectingEvent()
    {
        Debug.Log("asdasdasd");
        _thisImage.TweenColor(_unSelectColor, _selectColor, 0.25f);
    }
}
