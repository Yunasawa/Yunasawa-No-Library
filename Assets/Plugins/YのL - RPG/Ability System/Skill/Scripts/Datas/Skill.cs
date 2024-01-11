using Sirenix.OdinInspector;
using UnityEngine;
using YNL.Utilities;

namespace YNL.RPG
{
    [CreateAssetMenu(fileName = "Skill", menuName = "YのL/Skill")]
    public class Skill : ScriptableObject
    {
        [BoxGroup("A", showLabel: false)]
        [BoxGroup("A")] public string Code = "";
        [BoxGroup("A")] public string Name = "";
        [BoxGroup("A")] public SkillType Type = SkillType.None;
        [BoxGroup("A")] public string Description = "";
        [BoxGroup("A")] public Sprite Icon;

        [BoxGroup("B", showLabel: false)]
        [BoxGroup("B")] public float ManaCost = 0;
        [BoxGroup("B")] public float CastTime = 0;
        [BoxGroup("B")] public float SkillDelay = 0;
        [BoxGroup("B")] public float Cooldown = 0;

        public SerializableDictionary<string, string> Properties = new();

        [BoxGroup("C", showLabel: false)]
        [BoxGroup("C")] public SkillObject CastEffect;
        [BoxGroup("C")] public AudioClip SoundEffect;

        [BoxGroup("D", showLabel: false)]
        [BoxGroup("D")] public StatusEffect StatusEffect;
    }
}