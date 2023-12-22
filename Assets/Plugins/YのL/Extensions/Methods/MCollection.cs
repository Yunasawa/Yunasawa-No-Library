using System.Collections.Generic;
using System.Linq;
using System;

namespace YNL.Extension.Method
{
    public static class MArray
    {
        /// <summary> 
        /// Get random element in a array. 
        /// </summary>
        public static T GetRandom<T>(this T[] array)
        {
            if (array.Length == 0) throw new IndexOutOfRangeException("Array is empty.");
            return array[UnityEngine.Random.Range(0, array.Length)];
        }

        /// <summary> 
        /// Get amount of elements satisfy the condition in array. 
        /// </summary>
        public static int Count<T>(this T[] list, Func<T, bool> predicate)
            => list.Where(predicate).ToArray().Length;

        /// <summary>
        /// Shuffle an array.
        /// </summary>
        public static void Shuffle<T>(this T[] list)
        {
            for (var i = list.Length - 1; i > 1; i--)
            {
                var j = UnityEngine.Random.Range(0, i + 1);
                var value = list[j];
                list[j] = list[i];
                list[i] = value;
            }
        }

        /// <summary>
        /// Return a array satisfies multiple predicates.
        /// </summary>
        public static IList<T> Wheres<T>(this T[] list, IList<Func<T, bool>> predicate)
        {
            List<T> newList = new List<T>();

            foreach (var item in predicate)
            {
                List<T> addList = list.Where(item).ToList();
                foreach (var element in addList) newList.Add(element);
            }

            return newList;
        }
    }

    public static class MList
    {
        /// <summary> 
        /// Get random element in a list. 
        /// </summary>
        public static T GetRandom<T>(this IList<T> list)
        {
            if (list.Count == 0) throw new IndexOutOfRangeException("List is empty!");
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        /// <summary>
        /// Get random element in a list but not the same with current.
        /// </summary>
        public static T GetRandomBut<T>(this IList<T> list, int current)
        {
            if (list.Count == 0) throw new IndexOutOfRangeException("List is empty!");
            int next = UnityEngine.Random.Range(0, list.Count);
            if (next == current) return list.GetRandomBut(current);
            else return list[next];
        }

        /// <summary>
        /// Remove random element in a list. Return that element.
        /// </summary>
        public static T RemoveRandom<T>(this IList<T> list)
        {
            if (list.Count == 0) throw new IndexOutOfRangeException("List is empty!");
            int index = UnityEngine.Random.Range(0, list.Count);
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }

        /// <summary> 
        /// Get amount of elements satisfy the condition in list. 
        /// </summary>
        public static int Count<T>(this IList<T> list, Func<T, bool> predicate)
            => list.Where(predicate).ToList().Count;

        /// <summary>
        /// Shuffle a list.
        /// </summary>
        public static void Shuffle<T>(this IList<T> list)
        {
            for (var i = list.Count - 1; i > 1; i--)
            {
                var j = UnityEngine.Random.Range(0, i + 1);
                var value = list[j];
                list[j] = list[i];
                list[i] = value;
            }
        }

        /// <summary>
        /// Return a list satisfies multiple predicates.
        /// </summary>
        public static List<T> Wheres<T>(this IList<T> list, IList<Func<T, bool>> predicate)
        {
            List<T> newList = new List<T>();

            foreach (var item in predicate)
            {
                List<T> addList = list.Where(item).ToList();
                foreach (var element in addList) newList.Add(element);
            }

            return newList;
        }

        /// <summary>
        /// Check if this item is the last item in list.
        /// </summary>
        public static bool IsLast<T>(this IList<T> list, T item)
        {
            if (list.IndexOf(item) >= list.Count - 1) return true;
            else return false;
        }

        /// <summary>
        /// Convenience way to declare a Foreach loop.
        /// </summary>
        public static void Foreach<T>(this IList<T> list, Action<T> action)
        {
            foreach (T item in list)
            {
                action(item);
            }
        }

        /// <summary>
        /// Check if a list is null or not.
        /// </summary>
        public static bool IsNull<T>(this IList<T> list)
            => list == null ? true : false;

        /// <summary>
        /// Check if a list is empty or not.
        /// </summary>
        public static bool IsEmpty<T>(this IList<T> list)
            => !list.IsNull() && list.Count <= 0 ? true : false;

        /// <summary>
        /// Check if a list is null or empty or not.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IList<T> list)
            => list.IsNull() || list.IsEmpty() ? true : false;
    }

    public static class MDictionary
    {

    }
}