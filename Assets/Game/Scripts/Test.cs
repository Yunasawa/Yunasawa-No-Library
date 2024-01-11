using UnityEngine;
using YNL.RPG;

public class Test : MonoBehaviour
{
    [SerializeField] private CharacterLife Life;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SkillObject skillObject = Instantiate(SkillManager.Instance.Database.Skills["Bullet"].CastEffect, Vector3.zero, new Quaternion(0, 0, 0, 0));
        }
    }
}