using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScene : PlayableBehaviour<DeathScene.Context>
{
    public class Context { };

    public float cutsceneTime;

    public Walk walk;
    public Jump jump;
    public Scarf scarf;
    public Craft craft;

    public PauseMenu panel;

    private static readonly int DyingHash = Animator.StringToHash("dying");

    public override bool Playing => Active;
    private bool active;
    public bool Active
    {
        get => active;
        set => active = true;
    }

    protected override void Awake()
    {
        base.Awake();
        ToggleAllAbilities(true);
    }

    public override void Stop()
    {
        throw new System.NotImplementedException();
    }

    protected override void Execute(Context context)
    {
        active = true;
        ToggleAllAbilities(false);
        Animator.SetBool(DyingHash, true);


        StartCoroutine(StartScene());
    }

    public void ToggleAllAbilities(bool toggle)
    {
        walk.Enabled = toggle;
        jump.Enabled = toggle;
        scarf.Enabled = toggle;
        craft.Enabled = toggle;
    }

    private IEnumerator StartScene()
    {
        yield return new WaitForSeconds(cutsceneTime);
        panel.DeadPanel();
    }
    
}
