using System;

namespace YNL.Extension.Method
{
    public static class MSystem
    {
        /// <summary>
        /// Make a for loop in a more convenience way.
        /// <br> Use: <b> time.ForLoop(() => { code }); </b></br>
        /// </summary>
        public static void ForLoop(this int time, Action loopCode)
        {
            for (int i = 0; i < time; i++) loopCode?.Invoke();
        }

        /// <summary>
        /// Make a if else condition in a more convenience way.
        /// <br> Use: <b> condition.IfElse(() => { isTrue }, () => { isFalse }); </b></br>
        /// </summary>
        public static void IfElse(this bool condition, Action isTrue, Action isFalse)
        {
            if (condition) isTrue?.Invoke();
            else isFalse?.Invoke();
        }
    }
}