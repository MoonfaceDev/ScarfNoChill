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
    private static readonly int throwingHash = Animator.StringToHash("throw");

    public override bool Playing => Active;
    private bool active;

    public bool Active
    {
        get => active;
        set => active = value;
    }

    public float attackIntervalSeconds;
    public float attackCooldown;
    public float attackRange;
    public float snowballSpeed;
    public float damage;
    public float rotationModifier;

    public GameObject spawnPoint;
    public GameObject snowball;

    private Context context;

    protected override void Awake()
    {
        base.Awake();
        context = new Context(GameObject.FindGameObjectWithTag("Player"));
    }

    private void Update()
    {
        //check if player is close
        if (!Active)
            Play(context);

        //player is too far away end coroutine
        else if (!CanPlay(context))
            Stop();

    }

    public override bool CanPlay(Context context)
    {
        float distance = Vector2.Distance(gameObject.transform.position, 
            context.player.transform.position);
        
        return base.CanPlay(context) && distance < attackRange;
    }

    protected override void Execute(Context context)
    {
        StopAllCoroutines();
        Active = true;
        StartCoroutine(ThrowSnowballs(context));
    }

    private IEnumerator ThrowSnowballs(Context context)
    {
        print("coroutine check " + transform.position);
        while (Active)
        {
            Character.Animator.SetBool(throwingHash, true);

            Vector3 playerMiddle = new Vector3(context.player.transform.position.x, context.player.transform.position.y + 1, context.player.transform.position.z);
            int dir = 1; 

            if (playerMiddle.x > transform.position.x)
            {
                dir = 1;
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                dir = -1;
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }

            yield return new WaitForSeconds(attackIntervalSeconds);

            Character.Animator.SetBool(throwingHash, false);

            GameObject instance = Instantiate(snowball, spawnPoint.transform.position, spawnPoint.transform.rotation);

            //aim to target
            Vector3 ballPos = instance.transform.position;

            instance.GetComponent<Snowball>().speed = snowballSpeed * dir;
            instance.GetComponent<Snowball>().player = context.player.GetComponent<Warmth>();
            instance.GetComponent<Snowball>().damage = damage;

            yield return new WaitForSeconds(attackCooldown);
        }
    }

    public override void Stop()
    {
        Active = false;
    }
}
