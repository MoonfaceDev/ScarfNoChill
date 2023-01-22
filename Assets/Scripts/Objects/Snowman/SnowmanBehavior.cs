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
    public GameObject snowball;

    private Context context;

    protected override void Awake()
    {
        context = new Context(GameObject.FindGameObjectWithTag("Player"));
    }

    private void Update()
    {
        if (!Active)
            Play(context);
    }

    public override bool CanPlay(Context context)
    {
        float distance = Vector2.Distance(gameObject.transform.position, 
            context.player.transform.position);
        
        return base.CanPlay(context) && distance < attackRange;
    }

    protected override void Execute(Context context)
    {
        Active = true;
        StartCoroutine(ThrowSnowballs(context));
    }

    private IEnumerator ThrowSnowballs(Context context)
    { 
        while (Active)
        {
            float randomInterval = Random.Range(attackIntervalSecondsRange.x, attackIntervalSecondsRange.y);
            yield return new WaitForSeconds(randomInterval);

            GameObject instance = Instantiate(snowball, spawnPoint.transform.position, spawnPoint.transform.rotation);
            

            Vector3 playerMiddle = new Vector3(context.player.transform.position.x, context.player.transform.position.y + 1, context.player.transform.position.z);
            Vector3 ballPos = instance.transform.position;

            instance.GetComponent<Snowball>().speed = playerMiddle.x > ballPos.x ? snowballSpeed : -snowballSpeed;

            instance.transform.right = playerMiddle - ballPos;
        }
    }

    public override void Stop()
    {
        Active = false;
    }
}
