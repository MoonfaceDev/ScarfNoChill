using UnityEngine;
using UnityEngine.Animations;

public class Scarf : PlayableBehaviour<Scarf.Context>
{
    public class Context {}

    public int tier; 
    public RuntimeAnimatorController[] suits;


    public bool Active
    {
        get => active;
        private set
        {
            active = value;
            Animator.SetBool(ScarfHash, active);

            if (value)
            {
                warmth.damageRate *= heatReductionMultiplier;
            }
            else
            {
                warmth.damageRate /= heatReductionMultiplier;
            }
        }
    }

    private bool active;

    public override bool Playing => Active;

    public float maxStamina;
    public float staminaThreshold;
    public float staminaReductionRate;
    public float staminaIncrementRate;

    [Range(0, 1)]
    public float heatReductionMultiplier;

    public PauseMenu menu;

    [HideInInspector] public float stamina;
    private Walk walk;
    private Warmth warmth;

    private static readonly int ScarfHash = Animator.StringToHash("scarf");

    protected override void Awake()
    {
        base.Awake();
        tier = 0;
        walk = GetComponent<Walk>();
        warmth = GetComponent<Warmth>();

        stamina = maxStamina;
    }

    public override bool CanPlay(Context context)
    {
        return base.CanPlay(context) && Character.Grounded && stamina >= staminaThreshold; // TODO: !recovering from kb
    }

    protected override void Execute(Context context)
    {
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
        if (!Active) return;
        Active = false;
        walk.Enabled = true;
    }

    public void AdvanceTier()
    {
        if (tier == 2)
        {
            menu.WinPanel(GetComponent<Score>().score);
            return;
        }

        Character.Animator.runtimeAnimatorController = suits[++tier];
    }
}