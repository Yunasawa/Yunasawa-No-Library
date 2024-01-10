using UnityEngine;

namespace YNL.Extension.Method
{
    public static class MRigidbody
    {
        /// <summary>
        /// Change direction of a rigidbody while keeping its velocity.
        /// </summary>
        public static void ChangeDirection(this Rigidbody rigidbody, Vector3 direction)
        {
            rigidbody.velocity = direction.normalized * rigidbody.velocity.magnitude;
        }

        /// <summary>
        /// Change velocity to target speed while keeping its direction.
        /// </summary>
        public static void NormalizeVelocity(this Rigidbody rb, float magnitude = 1)
        {
            rb.velocity = rb.velocity.normalized * magnitude;
        }
    }
}