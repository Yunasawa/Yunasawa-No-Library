using UnityEngine;
using YNL.Utilities;

[CreateAssetMenu(fileName = "Hierarchy Object Container", menuName = "YのL/Hierarchy Object Container", order = 1)]
public class HierarchyObjectContainerSO : ScriptableObject
{
    public SerializableDictionary<string, GameObject> ConfigObject = new();

    public SerializableDictionary<string, GameObject> ObjectList = new();
}