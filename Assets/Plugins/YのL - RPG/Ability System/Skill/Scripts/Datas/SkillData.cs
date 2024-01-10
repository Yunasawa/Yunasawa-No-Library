using Sirenix.OdinInspector;
using UnityEngine;
using YNL.Utilities;

namespace YNL.RPG
{
    [System.Serializable]
    public class SkillData
    {
        #region ▶ Properties
        [FoldoutGroup("Basic Properties", expanded: false)]
        [FoldoutGroup("Basic Properties")] public string Code = "";
        [FoldoutGroup("Basic Properties")] public string Name = "";
        [FoldoutGroup("Basic Properties")] public SkillType Type = SkillType.None;
        [FoldoutGroup("Basic Properties")] public string Description = "";
        [FoldoutGroup("Basic Properties")] public Sprite Icon;

        [FoldoutGroup("Basic Properties")] public float ManaCost = 0;
        [FoldoutGroup("Basic Properties")] public float CastTime = 0;
        [FoldoutGroup("Basic Properties")] public float SkillDelay = 0;
        [FoldoutGroup("Basic Properties")] public float Cooldown = 0;

        [BoxGroup("Specific Properties", showLabel: false)]
        [BoxGroup("Specific Properties")] public SerializableDictionary<string, string> Properties = new();
        #endregion
    }
}