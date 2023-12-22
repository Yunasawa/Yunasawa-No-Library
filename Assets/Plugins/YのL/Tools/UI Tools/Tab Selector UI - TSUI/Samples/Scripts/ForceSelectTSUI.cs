using Sirenix.OdinInspector;
using UnityEngine;
using YNL.Tools.UI;

public class ForceSelectTSUI : MonoBehaviour
{
    [SerializeField] private TabManager _tabManager;
    [Space(10)]
    [SerializeField] private string _buttonLabel;
    [SerializeField] private TabButton _buttonObject;

    [ButtonGroup(ButtonHeight = 25)]
    void SelectLabelFromManager()
    {
        _tabManager.ForceSelect(_buttonLabel);
    }

    [ButtonGroup(ButtonHeight = 25)]
    void SelectObjectFromManager()
    {
        _tabManager.ForceSelect(_buttonObject);
    }

    [Button(ButtonHeight = 25)]
    void DirectlySelectObject()
    {
        _buttonObject.ForceSelect();
    }
}
