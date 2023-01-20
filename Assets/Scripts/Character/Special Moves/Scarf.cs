using System;
using UnityEngine;

public class Scarf : PlayableBehaviour<Scarf.Context>
{
    public class Context
    {
    }

    public bool Active
    {
        get => active;
        private set
        {
            active = value;
            Animator.SetBool(ScarfHash, active);
        }
    }

    private bool active;

    public override bool Playing => active;

    public float maxStamina;
    public float staminaThreshold;
    public float staminaReductionRate;
    public float staminaIncrementRate;

    private float stamina;
    private Walk walk;
    private static readonly int ScarfHash = Animator.StringToHash("scarf");

    protected override void Awake()
    {
        base.Awake();
        walk = GetComponent<Walk>();
    }

    public override bool CanPlay(Context context)
    {
        return base.CanPlay(context) && Character.Grounded && stamina >= staminaThreshold ; // TODO: !recovering from kb
    }

    protected override void Execute(Context context)
    {
        // TODO: reduce damage to the bar
        Active = true;
        walk.Enabled = false;
    }

    private void Update()
    {
        if (Active)
        {
            stamina -= staminaReductionRate * Time.time;
        }
        else
        {
            stamina += staminaIncrementRate * Time.time;
        }
    }

    public override void Stop()
    {
        Active = false;
        walk.Enabled = true;
    }
}