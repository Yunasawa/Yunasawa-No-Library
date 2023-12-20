using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MObject
{
    /// <summary> 
    /// Check whether object is null or not 
    /// </summary>
    public static bool IsNull(this object obj)
        => obj == null || ReferenceEquals(obj, null) || obj.Equals(null);
}

public static class MComponent
{

    /// <summary>
    /// Destroy an object/component/asset in OnValidate(), while Destroy() and DestroyImmediate() are not working.
    /// </summary>
    public static void DestroyOnValidate(this Object component)
    {   
        UnityEditor.EditorApplication.delayCall += () =>
        {
            MonoBehaviour.DestroyImmediate(component);
        };
    }
}
