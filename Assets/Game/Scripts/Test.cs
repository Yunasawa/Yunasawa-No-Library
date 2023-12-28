using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using YNL.Extension.Method;


public class Test : MonoBehaviour
{
    public float countdownDuration = 120f;
    public string CurrenTimeColon = "";
    public string CurrenTimeLetter = "";

    public Chat chat = new();

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

    private void Update()
    {
#if YUNASAWA_NO_LIBRARY
        Debug.Log("YUNASAWA");
#endif
    }
}

[System.Serializable]
public class Chat
{
    public string quest;
    public Chat chat1;
    public Chat chat2;
    public List<Chat> list = new();
}