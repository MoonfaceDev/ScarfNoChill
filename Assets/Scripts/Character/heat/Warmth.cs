using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Warmth : CharacterBehaviour
{
    public const float MaxWarmth = 100;

    public float damageRate;
    public int damagePercentage;

    public UnityEvent deathEvent;

    [HideInInspector]
    public float heat;

    private static readonly int DyingHash = Animator.StringToHash("dying");

    private float heatReductionMultiplier;

    protected override void Awake()
    {
        base.Awake();
        heatReductionMultiplier = 1;
        heat = MaxWarmth;
    }

    private void Update()
    {
        heat -= damagePercentage * heatReductionMultiplier * damageRate * Time.time;
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
        heatReductionMultiplier = startOrEnd ? heatReductionMultiplier * slowDamageMultiplier_ : 
            heatReductionMultiplier / slowDamageMultiplier_;
    }

}