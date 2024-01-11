using System.Collections;
using UnityEngine;
using YNL.Extension.Method;
using YNL.RPG;

public class Enemy : MonoBehaviour
{
    public int HP = 1000;
    public StatusEffect Effect = StatusEffect.None;
    private Coroutine currentEffect;

    public void StartEffectCountdown(int second)
    {
        if (!currentEffect.IsNull()) StopCoroutine(currentEffect);
        currentEffect = StartCoroutine(EffectCountdown(second));
    }

    public IEnumerator EffectCountdown(int countdown)
    {
        yield return new WaitForSeconds(countdown);
        Effect = StatusEffect.None;
    }
}