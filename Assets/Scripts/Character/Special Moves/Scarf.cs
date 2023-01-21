﻿using UnityEngine;

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

    public override bool Playing => Active;

    public float maxStamina;
    public float staminaThreshold;
    public float staminaReductionRate;
    public float staminaIncrementRate;

    [HideInInspector] public float stamina;
    private Walk walk;
    private static readonly int ScarfHash = Animator.StringToHash("scarf");

    protected override void Awake()
    {
        base.Awake();
        walk = GetComponent<Walk>();
        stamina = maxStamina;
    }

    public override bool CanPlay(Context context)
    {
        return base.CanPlay(context) && Character.Grounded && stamina >= staminaThreshold; // TODO: !recovering from kb
    }

    protected override void Execute(Context context)
    {
        // TODO: reduce damage to the bar
        walk.Stop();
        Active = true;
        walk.Enabled = false;
    }

    private void Update()
    {
        if (Active)
        {
            stamina -= staminaReductionRate * Time.deltaTime;
            if (stamina < 0)
            {
                stamina = 0;
                Stop();
            }
        }
        else
        {
            stamina = Mathf.Min(stamina + staminaIncrementRate * Time.deltaTime, maxStamina);
        }
    }

    public override void Stop()
    {
        Active = false;
        walk.Enabled = true;
    }
}