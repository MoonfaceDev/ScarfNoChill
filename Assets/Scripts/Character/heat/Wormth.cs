using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Wormth : CharacterBehaviour
{
    public float damageTakenDelaySeconds;
    public int damageAmount;

    [Range(0, 1)]
    public float slowDamageMultiplier;

    public UnityEvent deathEvent;

    [HideInInspector]
    public float heat;

    private static readonly int DyingHash = Animator.StringToHash("dying");

    private int maxDamageAmount;

    protected override void Awake()
    {
        base.Awake();

        maxDamageAmount = damageAmount;

        heat = 100;
        StartCoroutine("ChillDown");
    }

    private void TakeDamage()
    {
        heat -= damageAmount;
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (heat <= 0)
        {
            Animator.SetBool(DyingHash, true);
            deathEvent.Invoke();
            Debug.Log("no chill!!\n [character is dead]");
        }
    }

    public void SlowChill(bool startOrEnd)
    {
        damageAmount = startOrEnd ? (int) (damageAmount * slowDamageMultiplier) : maxDamageAmount;
    }

    private IEnumerator ChillDown()
    {
        while(Enabled)
        {
            yield return new WaitForSeconds(damageTakenDelaySeconds);
            TakeDamage();
        }
    }
}
