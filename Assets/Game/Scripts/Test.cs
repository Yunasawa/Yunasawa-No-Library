using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using YNL.Attribute;
using YNL.Extension.Method;

public class Test : MonoBehaviour
{
    public float countdownDuration = 120f;
    public string CurrenTimeColon = "";
    public string CurrenTimeLetter = "";

    [Button]
    public void StartCount()
    {
        StartCoroutine(MTime.CountDown(countdownDuration, OnTick, OnCountdownEnd));
    }

    [Button]
    public void StopCount()
    {
        StopAllCoroutines();
        CurrenTimeColon = "";
        CurrenTimeLetter = "";
    }

    private void OnTick(int currentTime)
    {
        CurrenTimeColon = currentTime.TimeFormat("00:00");
        CurrenTimeLetter = currentTime.TimeFormat("hhmmss");
    }

    private void OnCountdownEnd()
    {
        Debug.Log("Countdown completed!");
    }

}