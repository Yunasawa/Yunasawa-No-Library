using UnityEngine;

public static class MDebug
{
    public static void Warning(string message)
    {
        Debug.Log($"<color=#FFE045><b>⚠ Warning: </b></color> {message}");
    }
}