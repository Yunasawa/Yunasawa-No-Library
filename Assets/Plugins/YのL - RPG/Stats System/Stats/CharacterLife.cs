using Sirenix.OdinInspector;

namespace YNL.RPG
{
    [System.Serializable]
    public class CharacterLife
    {
        public LifePoint HP;
        public LifePoint MP;
        public LifePoint SP;
    }

    [System.Serializable]
    public class LifePoint
    {
        public int CurrentPoint;
        public int MaxPoint;
        public float Ratio => (float)CurrentPoint / MaxPoint;

        public LifePoint(int fullPoint)
        {
            this.MaxPoint = fullPoint;
        }

        public void UpdateMax(int point) => MaxPoint = point;
        public void AddPoint(int point)
        {
            CurrentPoint += point;
            if (CurrentPoint > MaxPoint) CurrentPoint = MaxPoint;
            else if (CurrentPoint < 0) CurrentPoint = 0;
        }
    }
}