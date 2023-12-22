using System;
using System.IO;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace YNL.Extension.Method
{
    public static class MData
    {
        /// <summary>
        /// Convert data to json string (Not including unserializable type).
        /// </summary>
        public static bool SaveNewtonJson<T>(this T data, string path, Action saveDone = null)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
                Debug.LogWarning("Target json file doesn't exist! Created a new file.");
            }

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(path, json);
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
            return true;
        }


        /// <summary>
        /// Load data from json string (Not including unserializable type).
        /// </summary>
        public static T LoadNewtonJson<T>(string path, Action<T> complete = null, Action<string> fail = null)
        {
            T data = JsonConvert.DeserializeObject<T>("");

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                data = JsonConvert.DeserializeObject<T>(json);

                complete?.Invoke(data);
            }
            else
            {
                fail?.Invoke("Load Json Failed!");
            }

            return data;
        }
    }
}