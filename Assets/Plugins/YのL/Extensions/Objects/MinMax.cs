namespace YNL.Extension.Objects
{
    [System.Serializable]
    public class MinMax
    {
        public float Min;
        public float Max;

        public MinMax(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public bool InRange(float number) => number >= Min && number < Max;
    }
}