using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YNL.Extension.Method
{
    public static class MMath
    {
        /// <summary>
        /// Convert a numeric string into decimal number.
        /// </summary>
        public static int ToInt(this string input)
            => input.IsNull() ? 0 : int.Parse(input);

        /// <summary> 
        /// Returns the value with FPS's change with default scale of 120 
        /// </summary>
        public static float Oscillate(this float value)
            => value * (float)(60 * Time.deltaTime);

        /// <summary> 
        /// Returns the value with FPS's change 
        /// </summary>
        public static float Oscillate(this float value, float scale)
            => value * (float)(scale * Time.deltaTime);

        /// <summary>
        /// Normalize a <b><i>Value</i></b> to scale of <b><i>Range</i></b>.
        /// </summary>
        public static float Normalize(this float value, float range)
            => value / range;

        /// <summary>
        /// Percentization a <b><i>Value</i></b> to scale of <b><i>Range</i></b>. 
        /// </summary>
        public static float Percent(this float value, float range)
            => value / range;
        public static float Percent(this float value)
            => value / 100;

        /// <summary> 
        /// Round float into custom amount of digit 
        /// </summary>
        public static float RoundToDigit(this float input, int digit)
            => Mathf.Round(input * Mathf.Pow(10, digit)) / Mathf.Pow(10, digit);

        /// <summary> 
        /// Check if input float is between (compare - gap) and (compare + gap). 
        /// </summary>
        public static bool CompareGap(this float input, float compare, float gap)
            => input >= compare - gap && input <= compare + gap ? true : false;

        /// <summary> 
        /// Check if input value if between 2 comparing value. 
        /// </summary>
        public static bool IsBetween(this float input, float lower, float higher)
            => input >= lower && input <= higher ? true : false;

        /// <summary>
        /// Convert Hex into Integer.
        /// </summary>
        public static int HexToInt(this string hex)
        {
            int output = 0;
            try
            {
                output = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Format Exception: Invalid HEX Format.");
            }
            return output;
        }
    }

    public static class MTime
    {
        /// <summary>
        /// An IEnumerator used for countdown time. <br></br> <br></br>
        /// <i>duration:</i> amount of time by second. <br></br>
        /// <i>remainTick (Action):</i> return remaining amount of time by second (use with TimeFormat()). <br></br>
        /// <i>onEnd (Action):</i> invoked at the end of counting time.
        /// </summary>
        public static IEnumerator CountDown(float duration, Action<int> remainTick, Action onEnd)
        {
            int remainingTime = (int)duration;

            while (remainingTime > 0)
            {
                remainTick?.Invoke(remainingTime);
                yield return new WaitForSeconds(1.0f);
                remainingTime--;

                if (remainingTime == 0) remainTick?.Invoke(remainingTime);
            }

            onEnd?.Invoke();
        }
    }

    public static class MVector
    {
        #region 🔁 Converting Vector
        /// <summary> 
        /// Convert from Vector3 into Vector2 
        /// </summary>
        public static Vector2 ToVector2(this Vector3 input)
            => new Vector2(input.x, input.y);

        /// <summary> 
        /// Convert from Vector2 float into Vector2 integer. 
        /// </summary>
        public static Vector2Int ToVector2Int(this Vector2 input)
            => new Vector2Int((int)input.x, (int)input.y);

        /// <summary> 
        /// Convert from Vector3 float into Vector3 integer. 
        /// </summary>
        public static Vector3Int ToVector3Int(this Vector3 input)
            => new Vector3Int((int)input.x, (int)input.y, (int)input.z);
        #endregion

        #region 📠 Calculating Vector
        /// <summary> 
        /// Make a vector3 with random value betwwen <b><i> x </i></b> and <b><i> y </i></b>. 
        /// </summary>
        public static Vector3 Vector3Random(this Vector3 point, float x, float y)
            => new Vector3(point.x + UnityEngine.Random.Range(x, y), point.y + UnityEngine.Random.Range(x, y), point.z + UnityEngine.Random.Range(x, y));
        #endregion

        #region 📐 Set / Add Vector
        /// <summary>
        /// Set X axis of Vector2.
        /// </summary>
        public static Vector2 SetX(this Vector2 vector, float x)
            => new Vector2(x, vector.y);

        /// <summary>
        /// Set Y axis of Vector2.
        /// </summary>
        public static Vector2 SetY(this Vector2 vector, float y)
            => new Vector2(vector.x, y);

        /// <summary>
        /// Add into X axis of Vector2.
        /// </summary>
        public static Vector2 AddX(this Vector2 vector, float x)
            => new Vector2(vector.x + x, vector.y);

        /// <summary>
        /// Add into Y axis of Vector2.
        /// </summary>
        public static Vector2 AddY(this Vector2 vector, float y)
            => new Vector2(vector.x, vector.y + y);

        /// <summary>
        /// Set X axis of Vector3.
        /// </summary>
        public static Vector3 SetX(this Vector3 vector, float x)
            => new Vector3(x, vector.y, vector.z);

        /// <summary>
        /// Set Y axis of Vector3.
        /// </summary>
        public static Vector3 SetY(this Vector3 vector, float y)
            => new Vector3(vector.x, y, vector.z);

        /// <summary>
        /// Set Z axis of Vector3.
        /// </summary>
        public static Vector3 SetZ(this Vector3 vector, float z)
            => new Vector3(vector.x, vector.y, z);

        /// <summary>
        /// Add into X axis of Vector3.
        /// </summary>
        public static Vector3 AddX(this Vector3 vector, float x)
            => new Vector3(vector.x + x, vector.y, vector.z);

        /// <summary>
        /// Add into Y axis of Vector3.
        /// </summary>
        public static Vector3 AddY(this Vector3 vector, float y)
            => new Vector3(vector.x, vector.y + y, vector.z);

        /// <summary>
        /// Add into Z axis of Vector3.
        /// </summary>
        public static Vector3 AddZ(this Vector3 vector, float z)
            => new Vector3(vector.x, vector.y, vector.z + z);
        #endregion

        #region 📝 Get Closest / Farthest Vector
        /// <summary>
        /// Get the closest Vector2 from an array to the original Vector2.
        /// </summary>
        public static Vector2 GetClosestVector2From(this Vector2 vector, Vector2[] otherVectors)
        {
            if (otherVectors.Length == 0) throw new Exception("The array of other vectors is empty");
            var minDistance = Vector2.Distance(vector, otherVectors[0]);
            var minVector = otherVectors[0];
            for (var i = otherVectors.Length - 1; i > 0; i--)
            {
                var newDistance = Vector2.Distance(vector, otherVectors[i]);
                if (newDistance < minDistance)
                {
                    minDistance = newDistance;
                    minVector = otherVectors[i];
                }
            }
            return minVector;
        }

        /// <summary>
        /// Get the closest Vector2 from a list to the original Vector2.
        /// </summary>
        public static Vector2 GetClosestVector2From(this Vector2 vector, List<Vector2> otherVectors)
        {
            if (otherVectors.Count == 0) throw new Exception("The list of other vectors is empty");
            var minDistance = Vector2.Distance(vector, otherVectors[0]);
            var minVector = otherVectors[0];
            for (var i = otherVectors.Count - 1; i > 0; i--)
            {
                var newDistance = Vector2.Distance(vector, otherVectors[i]);
                if (newDistance < minDistance)
                {
                    minDistance = newDistance;
                    minVector = otherVectors[i];
                }
            }
            return minVector;
        }

        /// <summary>
        /// Get the farthest Vector2 from an array to the original Vector2.
        /// </summary>
        public static Vector2 GetFarthestVector2From(this Vector2 vector, Vector2[] otherVectors)
        {
            if (otherVectors.Length == 0) throw new Exception("The array of other vectors is empty");
            var maxDistance = Vector2.Distance(vector, otherVectors[0]);
            var maxVector = otherVectors[0];
            for (var i = otherVectors.Length - 1; i > 0; i--)
            {
                var newDistance = Vector2.Distance(vector, otherVectors[i]);
                if (newDistance > maxDistance)
                {
                    maxDistance = newDistance;
                    maxVector = otherVectors[i];
                }
            }
            return maxVector;
        }

        /// <summary>
        /// Get the farthest Vector2 from a list to the original Vector2.
        /// </summary>
        public static Vector2 GetFarthestVector2From(this Vector2 vector, List<Vector2> otherVectors)
        {
            if (otherVectors.Count == 0) throw new Exception("The list of other vectors is empty");
            var maxDistance = Vector2.Distance(vector, otherVectors[0]);
            var maxVector = otherVectors[0];
            for (var i = otherVectors.Count - 1; i > 0; i--)
            {
                var newDistance = Vector2.Distance(vector, otherVectors[i]);
                if (newDistance > maxDistance)
                {
                    maxDistance = newDistance;
                    maxVector = otherVectors[i];
                }
            }
            return maxVector;
        }

        /// <summary>
        /// Get the closest Vector3 from an array to the original Vector3.
        /// </summary>
        public static Vector3 GetClosestVector3From(this Vector3 vector, Vector3[] otherVectors)
        {
            if (otherVectors.Length == 0) throw new Exception("The list of other vectors is empty");
            var minDistance = Vector3.Distance(vector, otherVectors[0]);
            var minVector = otherVectors[0];
            for (var i = otherVectors.Length - 1; i > 0; i--)
            {
                var newDistance = Vector3.Distance(vector, otherVectors[i]);
                if (newDistance < minDistance)
                {
                    minDistance = newDistance;
                    minVector = otherVectors[i];
                }
            }
            return minVector;
        }

        /// <summary>
        /// Get the closest Vector3 from a list to the original Vector3.
        /// </summary>
        public static Vector3 GetClosestVector3From(this Vector3 vector, List<Vector3> otherVectors)
        {
            if (otherVectors.Count == 0) throw new Exception("The list of other vectors is empty");
            var minDistance = Vector3.Distance(vector, otherVectors[0]);
            var minVector = otherVectors[0];
            for (var i = otherVectors.Count - 1; i > 0; i--)
            {
                var newDistance = Vector3.Distance(vector, otherVectors[i]);
                if (newDistance < minDistance)
                {
                    minDistance = newDistance;
                    minVector = otherVectors[i];
                }
            }
            return minVector;
        }

        /// <summary>
        /// Get the farthest Vector3 from an array to the original Vector3.
        /// </summary>
        public static Vector3 GetFarthestVector3From(this Vector3 vector, Vector3[] otherVectors)
        {
            if (otherVectors.Length == 0) throw new Exception("The array of other vectors is empty");
            var maxDistance = Vector3.Distance(vector, otherVectors[0]);
            var maxVector = otherVectors[0];
            for (var i = otherVectors.Length - 1; i > 0; i--)
            {
                var newDistance = Vector3.Distance(vector, otherVectors[i]);
                if (newDistance > maxDistance)
                {
                    maxDistance = newDistance;
                    maxVector = otherVectors[i];
                }
            }
            return maxVector;
        }

        /// <summary>
        /// Get the farthest Vector3 from an list to the original Vector3.
        /// </summary>
        public static Vector3 GetFarthestVector3From(this Vector3 vector, List<Vector3> otherVectors)
        {
            if (otherVectors.Count == 0) throw new Exception("The array of other vectors is empty");
            var maxDistance = Vector3.Distance(vector, otherVectors[0]);
            var maxVector = otherVectors[0];
            for (var i = otherVectors.Count - 1; i > 0; i--)
            {
                var newDistance = Vector3.Distance(vector, otherVectors[i]);
                if (newDistance > maxDistance)
                {
                    maxDistance = newDistance;
                    maxVector = otherVectors[i];
                }
            }
            return maxVector;
        }
        #endregion
    }
}