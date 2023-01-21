using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanBehavior : PlayableBehaviour<SnowmanBehavior.Context>
{
    public class Context 
    {
        public GameObject player;

        public Context(GameObject player)
        {
            this.player = player;
        }
    }

    public GameObject snowball;

    public override bool Playing => Active;
    private bool active;

    public bool Active
    {
        get => active;
        set => active = value;
    }

    public float attackIntervalSeconds;

    private void Awake()
    {
        
    }

    public override bool CanPlay(Context context)
    {
        return base.CanPlay(context) && context.player.;
    }

    protected override void Execute(Context context)
    {
        throw new System.NotImplementedException();
    }

    public override void Stop()
    {
        ;
    }
}
