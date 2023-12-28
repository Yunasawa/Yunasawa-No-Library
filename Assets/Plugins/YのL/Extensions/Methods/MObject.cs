using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace YNL.Extension.Method
{
    public static class MObject
    {
        /// <summary> 
        /// Check whether object is null or not 
        /// </summary>
        public static bool IsNull(this object obj)
            => obj == null || ReferenceEquals(obj, null) || obj.Equals(null);

        /// <summary>
        /// Destroy an object/component/asset in OnValidate(), while Destroy() and DestroyImmediate() are not working.
        /// </summary>
        public static void DestroyOnValidate(this UnityEngine.Object component)
        {
            UnityEditor.EditorApplication.delayCall += () =>
            {
                MonoBehaviour.DestroyImmediate(component);
            };
        }
    }

    public static class MType
    {
        /// <summary>
        /// Check if a type is same or subtype of another.
        /// </summary>
        public static bool IsSameOrSubtype(this Type potentialBase, Type potentialDescendant)
        {
            return potentialDescendant.IsSubclassOf(potentialBase) || potentialDescendant == potentialBase;
        }
    }

    public static class MTransform
    {
        /// <summary> 
        /// Rotates the current transform toward the target transform just by Y axis 
        /// </summary>
        public static void LookAtByY(this Transform currentTransform, Transform targetTransform)
        {
            currentTransform.LookAt(new Vector3(targetTransform.position.x, currentTransform.position.y, targetTransform.position.z));
        }

        /// <summary>
        /// Reset a tranform to default values.
        /// </summary>
        public static void ResetTransform(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        /// <summary> 
        /// Destroy children of an object. 
        /// </summary>
        public static void DestroyAllChildren(this Transform transform)
        {
            foreach (var child in transform.Cast<Transform>())
            {
                UnityEngine.Object.Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// Destroy child at index
        /// </summary>
        public static void DestroyChildAt(this Transform transform, int index)
        {
            UnityEngine.Object.Destroy(transform.Cast<Transform>().ToList()[index]);
        }
    }

    public static class MComponent
    {
        /// <summary> 
        /// Disable all Monobehavior script in an object. 
        /// </summary>
        public static void DisableAllMonobehavior(this GameObject thisObject)
        {
            MonoBehaviour[] scripts = thisObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts) script.enabled = false;
        }

        /// <summary>
        /// Get component from an object. If there's not, add the component.
        /// </summary>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : MonoBehaviour
        {
            var component = gameObject.GetComponent<T>();
            if (component == null) gameObject.AddComponent<T>();
            return component;
        }

        /// <summary>
        /// Check if an object has the component or not.
        /// </summary>
        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
            => gameObject.GetComponent<T>() != null;
    }

    public static class MCoroutine
    {
        /// <summary>
        /// Start a coroutine.
        /// </summary>
        public static Coroutine StartACoroutine(this MonoBehaviour mono, IEnumerator enumerator)
            => mono.StartCoroutine(enumerator);

        /// <summary>
        /// Stop a coroutine.
        /// </summary>
        public static void StopACoroutine(this MonoBehaviour mono, Coroutine coroutine)
        {
            if (coroutine.IsNull()) return;
            mono.StopCoroutine(coroutine);
            coroutine = null;
        }

        public static void StopCoroutines(this MonoBehaviour mono, List<Coroutine> list)
        {
            foreach (var coroutine in list) mono.StopACoroutine(coroutine);
            list.Clear();
        }
    }
}