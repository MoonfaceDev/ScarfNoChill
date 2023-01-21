using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Warmth : CharacterBehaviour
{
    public const int MaxWarmth = 100;

    public float damageTakenDelaySeconds;
    public int damagePercentage;


    public UnityEvent deathEvent;

    [HideInInspector] public float heat;

    private static readonly int DyingHash = Animator.StringToHash("dying");

    private float slowDamageMultiplier;

    protected override void Awake()
    {
        base.Awake();
        slowDamageMultiplier = 1;
        heat = MaxWarmth;
        StartCoroutine(ChillDown());
    }

    private void TakeDamage()
    {
        heat -= damagePercentage * slowDamageMultiplier;
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

    public void SlowChill(bool startOrEnd, float slowDamageMultiplier_)
    {
        slowDamageMultiplier = startOrEnd ? slowDamageMultiplier_ : 1;
    }

    private IEnumerator ChillDown()
    {
        while (Enabled)
        {
            yield return new WaitForSeconds(damageTakenDelaySeconds);
            TakeDamage();
        }
    }
}