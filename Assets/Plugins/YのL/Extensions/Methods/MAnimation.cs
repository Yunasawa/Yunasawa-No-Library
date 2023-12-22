using UnityEngine;

namespace YNL.Extension.Method
{
    public static class MAnimation
    {
        /// <summary> 
        /// Changes current animation with lerping 
        /// </summary>
        public static void SmoothPlay(this Animator animator, string animation, float speed)
        {
            if (!animator.IsInTransition(0)) animator.CrossFade(animation, speed);
        }

        /// <summary> 
        /// Changes current animation without exit time.
        /// </summary>
        public static void ImmediatePlay(this Animator animator, string animation, float speed)
        {
            animator.CrossFade(animation, speed);
        }
    }
}