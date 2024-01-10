using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using System.Collections;
using UnityEngine.AddressableAssets;
using UnityEngine;
using UnityEngine.AddressableAssets.ResourceLocators;
using Newtonsoft.Json;
using YNL.Pattern.Singleton;

namespace YNL.Pattern.Addressable
{
    public class AddressableManager : Singleton<AddressableManager>
    {
        private readonly Dictionary<string, AsyncOperationHandle> dicAsset = new();

        public void CreateAsset<T>(string key, Action<T> onComplete, Action onFailed = null)
        {
            if (dicAsset.ContainsKey(key))
            {
                onComplete?.Invoke((T)(dicAsset[key].Result));
            }
            else
            {
                StartCoroutine(LoadAsset(key, onComplete, onFailed));
            }
        }

        private IEnumerator LoadAsset<T>(string key, Action<T> onComplete, Action onFailed = null)
        {
            var opHandle = Addressables.LoadAssetAsync<T>(key);
            yield return opHandle;

            if (opHandle.Status == AsyncOperationStatus.Succeeded)
            {
                onComplete?.Invoke(opHandle.Result);
                if (dicAsset.ContainsKey(key))
                {
                    RemoveAsset(key);
                }
                dicAsset[key] = opHandle;
            }
            else if (opHandle.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogError($"Load Asset Failed: {key}");
                onFailed?.Invoke();
            }
        }

        public void RemoveAsset(string key)
        {
            Addressables.Release(dicAsset[key]);
            dicAsset.Remove(key);
        }

        public void LoadJson<T>(string key, Action<T> onComplete = null, Action<string> onFail = null)
        {
            //Debug.Log($"Load JSON: {key}");
            Addressables.LoadAssetAsync<TextAsset>(key).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    T obj = JsonUtility.FromJson<T>(handle.Result.text);
                    //MyUtils.DumpToConsole(obj);
                    onComplete?.Invoke(obj);
                    Addressables.Release(handle);
                }
                else if (handle.Status == AsyncOperationStatus.Failed)
                {
                    Debug.LogError($"Load JSON Fail {key}");
                    onFail?.Invoke("Load JSON Fail");
                    Addressables.Release(handle);
                }
            };
        }

        public void LoadNewtonJson<T>(string key, Action<T> onComplete = null, Action<string> onFail = null)
        {
            //Debug.Log($"Load JSON: {key}");
            Addressables.LoadAssetAsync<TextAsset>(key).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    T obj = JsonConvert.DeserializeObject<T>(handle.Result.text);
                    //MyUtils.DumpToConsole(obj);
                    onComplete?.Invoke(obj);
                    Addressables.Release(handle);
                }
                else if (handle.Status == AsyncOperationStatus.Failed)
                {
                    Debug.LogError($"Load JSON Fail {key}");
                    onFail?.Invoke("Load JSON Fail");
                    Addressables.Release(handle);
                }
            };
        }

        public void PreloadAsset(string key, Action<bool> onComplete, Action<float> onProgress = null)
        {
            StartCoroutine(StartPreload(key, onComplete, onProgress));
        }

        private IEnumerator StartPreload(string key, Action<bool> onComplete, Action<float> onProgress = null)
        {
            AsyncOperationHandle<IResourceLocator> handle = Addressables.InitializeAsync(true);
            yield return handle;

            AsyncOperationHandle downloadHandle = Addressables.DownloadDependenciesAsync(key, false);
            float progress = 0;

            while (downloadHandle.Status == AsyncOperationStatus.None)
            {
                float percentageComplete = downloadHandle.GetDownloadStatus().Percent;
                if (percentageComplete > progress * 1.1) // Report at most every 10% or so
                {
                    progress = percentageComplete; // More accurate %
                    Debug.Log($"Preload data progress {progress}");
                    onProgress?.Invoke(progress);
                }
                yield return null;
            }

            Debug.Log($"Download key '{key}' {downloadHandle.Status}");
            onComplete?.Invoke(downloadHandle.Status == AsyncOperationStatus.Succeeded);
            Addressables.Release(downloadHandle); //Release the operation handle
        }
    }
}