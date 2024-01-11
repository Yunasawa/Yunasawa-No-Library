namespace YNL.RPG
{
    public enum SkillType
    {
        None, Attack, Defence, Buff, Debuff
    }

    public enum SkillRangeType
    {
        Single, Multiple, Area
    }

    public enum SkillSpawnType
    {
        FromCharacter, FromMouse
    }

    public enum SkillState
    {
        Ready, Active, Cooldown
    }
}