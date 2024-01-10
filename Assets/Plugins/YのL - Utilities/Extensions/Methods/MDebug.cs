using UnityEngine;

namespace YNL.Extension.Method
{
    public static class MDebug
    {
        public static void Warning(string message) => Debug.Log($"<color=#FFE045><b>⚠ Warning: </b></color> {message}");

        public static void Caution(string message) => Debug.Log($"<color=#FF983D><b>⚠ Caution: </b></color> {message}");

        public static void Action(string message) => Debug.Log($"<color=#EC82FF><b>▶ Action: </b></color><i>{message}</i>");

        public static void Notification(string message) => Debug.Log($"<color=#FFCD45><b>▶ Notification: </b></color><i>{message}</i>");
    }
}