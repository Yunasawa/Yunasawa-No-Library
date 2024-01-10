using YNL.Utilities;

namespace YNL.RPG
{
    [System.Serializable]
    public class SkillDatabase
    {
        public SerializableDictionary<string, Skill> Skills;

        public Skill GetSkill(string code)
        {
            return Skills[code];
        }
    }
}