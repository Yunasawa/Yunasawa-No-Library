using Sirenix.OdinInspector;
using UnityEngine;
using YNL.RPG;

public class Bullet : SkillObject
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.HP -= int.Parse(SkillData.Properties["Damage"]);
            enemy.Effect = SkillData.StatusEffect;
            enemy.StartEffectCountdown(2);
        }
    }
}