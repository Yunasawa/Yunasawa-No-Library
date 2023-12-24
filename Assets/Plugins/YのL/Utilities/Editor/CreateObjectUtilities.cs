// Link: https://www.youtube.com/watch?v=1uqrSONpXkM&ab_channel=Andrew

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using YNL.Extension.Method;

public static class CreateObjectUtilities
{
    public static void CreatePrefab(string name)
    {
        HierarchyObjectContainerSO containerSO = Resources.Load<HierarchyObjectContainerSO>("Hierarchy Object Container");
        GameObject newObject = PrefabUtility.InstantiatePrefab(containerSO.ObjectList[name]) as GameObject;
        PlaceOnHierarchy(newObject);
        PrefabUtility.UnpackPrefabInstance(newObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
    }

    public static GameObject CreateConfig(string name)
    {
        HierarchyObjectContainerSO containerSO = Resources.Load<HierarchyObjectContainerSO>("Hierarchy Object Container");
        GameObject newObject = PrefabUtility.InstantiatePrefab(containerSO.ConfigObject[name]) as GameObject;
        PlaceOnHierarchy(newObject);
        PrefabUtility.UnpackPrefabInstance(newObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);

        return newObject;
    }

    public static void CreateObject(string name, params Type[] types)
    {
        GameObject newObject = ObjectFactory.CreateGameObject(name, types);
        PlaceOnHierarchy(newObject);
    }

    public static void PlaceOnHierarchy(GameObject gameObject)
    {
        StageUtility.PlaceGameObjectInCurrentStage(gameObject);
        GameObjectUtility.EnsureUniqueNameForSibling(gameObject);

        Undo.RegisterCreatedObjectUndo(gameObject, $"Create Object: {gameObject.name}");

        if (!Selection.activeGameObject.IsNull())
        {
            gameObject.transform.parent = Selection.activeGameObject.transform;
        }
        else
        {
            if (gameObject.HasComponent<CanvasRenderer>())
            {
                Canvas canvas;

                canvas = GameObject.FindObjectOfType<Canvas>();
                if (!canvas.IsNull())
                {
                    gameObject.transform.parent = canvas.transform;
                }
                else
                {
                    canvas = CreateObjectUtilities.CreateConfig("Canvas").GetComponent<Canvas>();
                    gameObject.transform.parent = canvas.transform;
                }
                if (GameObject.FindObjectOfType<EventSystem>().IsNull())
                {
                    GameObject eventSystem = CreateObjectUtilities.CreateConfig("EventSystem");
                    eventSystem.transform.parent = null;
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
    [MenuItem("GameObject/YのL/UI/PointableUI", priority = 1)]
    public static void Create_PointableUI(MenuCommand menuCommand)
    {
        CreateObjectUtilities.CreatePrefab("Pointable UI");
    }

    [MenuItem("GameObject/YのL/UI/SwitchUI", priority = 2)]
    public static void Create_SwitchUI(MenuCommand menuCommand)
    {
        CreateObjectUtilities.CreatePrefab("Switch UI");
    }
}