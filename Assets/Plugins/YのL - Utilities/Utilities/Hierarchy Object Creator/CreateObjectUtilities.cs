// Link: https://www.youtube.com/watch?v=1uqrSONpXkM&ab_channel=Andrew

using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using YNL.Extension.Method;

public static class CreateObjectUtilities
{
    public static void CreatePrefab(string name, MenuCommand menuCommand)
    {
        HierarchyObjectContainerSO containerSO = Resources.Load<HierarchyObjectContainerSO>("Hierarchy Object Container");
        if (!containerSO.PrefabObject.ContainsKey(name) || containerSO.PrefabObject[name] == null)
        {
            MDebug.Caution($"[{name}] not found!!! May be it is disable from [Prefab Object] container.");
            containerSO.ShowWindow();
            return;
        }
        GameObject newObject = PrefabUtility.InstantiatePrefab(containerSO.PrefabObject[name]) as GameObject;
        PlaceOnHierarchy(newObject, menuCommand);
        PrefabUtility.UnpackPrefabInstance(newObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
    }

    public static GameObject CreateConfig(string name, MenuCommand menuCommand)
    {
        HierarchyObjectContainerSO containerSO = Resources.Load<HierarchyObjectContainerSO>("Hierarchy Object Container");
        if (!containerSO.ConfigObject.ContainsKey(name) || containerSO.ConfigObject[name] == null)
        {
            MDebug.Caution($"[{name}] not found!!! May be it is disable from [Config Object] container.");
            containerSO.ShowWindow();
            return null;
        }
        GameObject newObject = PrefabUtility.InstantiatePrefab(containerSO.ConfigObject[name]) as GameObject;
        PlaceOnHierarchy(newObject, menuCommand);
        PrefabUtility.UnpackPrefabInstance(newObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);

        return newObject;
    }

    public static void CreateObject(string name, MenuCommand menuCommand, params Type[] types)
    {
        GameObject newObject = ObjectFactory.CreateGameObject(name, types);
        PlaceOnHierarchy(newObject, menuCommand);
    }

    public static void PlaceOnHierarchy(GameObject gameObject, MenuCommand menuCommand)
    {
        StageUtility.PlaceGameObjectInCurrentStage(gameObject);
        GameObjectUtility.EnsureUniqueNameForSibling(gameObject);

        Undo.RegisterCreatedObjectUndo(gameObject, $"Create Object: {gameObject.name}");

        if (!Selection.activeGameObject.IsNull())
        {
            GameObjectUtility.SetParentAndAlign(gameObject, Selection.activeGameObject);
        }
        else
        {
            if (gameObject.HasComponent<CanvasRenderer>())
            {
                Canvas canvas;

                canvas = GameObject.FindObjectOfType<Canvas>();
                if (!canvas.IsNull())
                {
                    GameObjectUtility.SetParentAndAlign(gameObject, canvas.gameObject);
                }
                else
                {
                    canvas = CreateConfig("Canvas", menuCommand)?.GetComponent<Canvas>();
                    if (canvas != null) GameObjectUtility.SetParentAndAlign(gameObject, canvas.gameObject);
                }
                if (GameObject.FindObjectOfType<EventSystem>().IsNull())
                {
                    GameObject eventSystem = CreateConfig("EventSystem", menuCommand);
                    eventSystem?.transform.SetParent(null);
                }
            }
        }
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        if (!rectTransform.IsNull())
        {
            rectTransform.anchoredPosition = Vector3.zero;
            rectTransform.localScale = Vector3.one;
        }
        else
        {
            gameObject.transform.position = Vector3.zero;
            gameObject.transform.localScale = Vector3.one;
        }

        Selection.activeGameObject = gameObject;

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }
}

public static class CreateObjectOnHierarchy
{
    [MenuItem("GameObject/🧊 YのL/2D/PointableUI", priority = 0)]
    public static void Create_PointableUI(MenuCommand menuCommand)
    {
        CreateObjectUtilities.CreatePrefab("Pointable UI", menuCommand);
    }

    [MenuItem("GameObject/🧊 YのL/2D/SwitchUI", priority = 0)]
    public static void Create_SwitchUI(MenuCommand menuCommand)
    {
        CreateObjectUtilities.CreatePrefab("Switch UI", menuCommand);
    }

    [MenuItem("GameObject/🧊 YのL/2D/FPS", priority = 0)]
    public static void Create_FPS(MenuCommand menuCommand)
    {
        CreateObjectUtilities.CreatePrefab("FPS", menuCommand);
    }
}