using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YNL.Extension.Method;
using YNL.Utilities;

namespace YNL.Tools.UI
{
    public class UITransitionManager : MonoBehaviour
    {
        public SerializableDictionary<string, UnityEvent> Events = new();

        public void InvokeEvent(string key)
        {
            Events[key].Invoke();
        }
    }

    [System.Serializable]
    public enum TweenType
    {
        ExponentialInterpolation, LinearInterpolation
    }
}