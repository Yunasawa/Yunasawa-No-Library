using UnityEngine;
using YNL.Utilities;

namespace YNL.RPG
{
    [CreateAssetMenu(menuName = "YNL/Skill Database", fileName = "Skill Database")]
    public class SkillDatabase : ScriptableObject
    {
        public SerializableDictionary<string, Skill> Skills = new();
    }
}