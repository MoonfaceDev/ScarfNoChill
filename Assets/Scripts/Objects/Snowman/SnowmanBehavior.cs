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

    public override bool Playing => Active;
    private bool active;

    public bool Active
    {
        get => active;
        set => active = value;
    }

    public Vector2 attackIntervalSecondsRange;
    public float attackRange;
    public float snowballSpeed;

    public GameObject spawnPoint;
    public GameObject aimObject;
    public GameObject snowball;

    public override bool CanPlay(Context context)
    {
        float distance = Vector2.Distance(gameObject.transform.position, 
            context.player.transform.position);
        
        return base.CanPlay(context) && distance < attackRange;
    }

    protected override void Execute(Context context)
    {
        Active = true;
        StartCoroutine(ThrowSnowballs(context.player.transform));
    }

    private IEnumerator ThrowSnowballs(Transform playerPos)
    { 
        while (Active)
        {
            float randomInterval = Random.Range(attackIntervalSecondsRange.x, attackIntervalSecondsRange.y);
            yield return new WaitForSeconds(randomInterval);

            //aim to target
            aimObject.transform.right = playerPos.position - transform.position;
            Instantiate(snowball, spawnPoint.transform.position, aimObject.transform.rotation);
        }
    }

    public override void Stop()
    {
        Active = false;
    }
}
