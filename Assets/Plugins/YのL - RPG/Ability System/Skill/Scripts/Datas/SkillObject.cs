using UnityEngine;

namespace YNL.RPG
{
    public class SkillObject : MonoBehaviour
    {
        public Skill SkillData;

        protected virtual void Awake()
        {
            OnCasted();
        }

        public virtual void OnCasted()
        {
            Destroy(gameObject, SkillData.CastTime);
        }

        public virtual void OnAffected() { }
    }
}