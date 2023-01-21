using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Wormth : CharacterBehaviour
{
    public float damageTakenDelaySeconds;
    public float damageAmount;
    public UnityEvent deathEvent;

    [HideInInspector]
    public float heat;

    private static readonly int DyingHash = Animator.StringToHash("dying");

    protected override void Awake()
    {
        base.Awake();

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

    private IEnumerator ChillDown()
    {
        while(Enabled)
        {
            yield return new WaitForSeconds(damageTakenDelaySeconds);
            TakeDamage();
        }
    }
}
