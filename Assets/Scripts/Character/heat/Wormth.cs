using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Wormth : CharacterBehaviour
{
    public static int MaxWormth = 100;

    public float damageTakenDelaySeconds;
    public int damagePrecentage;

    
    public UnityEvent deathEvent;

    [HideInInspector]
    public float heat;

    private static readonly int DyingHash = Animator.StringToHash("dying");

    private int maxDamageAmount;

    protected override void Awake()
    {
        base.Awake();

        heat = MaxWormth;
        StartCoroutine("ChillDown");
    }

    private void TakeDamage()
    {
        heat -= damagePrecentage;
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

    public void SlowChill(bool startOrEnd, float slowDamageMultiplier)
    {
        damagePrecentage = startOrEnd ? (int) (damagePrecentage * slowDamageMultiplier) : maxDamageAmount;
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
