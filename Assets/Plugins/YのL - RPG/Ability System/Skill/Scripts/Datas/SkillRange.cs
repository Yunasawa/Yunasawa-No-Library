using UnityEngine;

namespace YNL.RPG
{
    [System.Serializable]
    public class SkillRange
    {
        public SkillRangeType Range;
        public int Amount; // Amount of 'Skill Range', for 'Single' is 1, for 'Area' is the radius.
    }
}