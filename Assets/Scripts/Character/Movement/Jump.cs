using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jump : PlayableBehaviour<Jump.Context>
{
    public class Context
    {
        public float jumpSpeed;

        public Context(float jumpSpeed)
        {
            this.jumpSpeed = jumpSpeed;
        }
    }

    private new Rigidbody2D rigidbody;

    public float maxJumpTimeSeconds;
    public override bool Playing => Jumping;

    private static readonly int JumpingHash = Animator.StringToHash("jumping");

    private float startTime;
    private bool jumping;
    private bool gainSpeed;
    private readonly float jumpSpeed;

    public bool Jumping
    {
        get => jumping;
        private set
        {
            jumping = value;
            Animator.SetBool(JumpingHash, value);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Character.Grounded && jumping)
            Stop();

        if (gainSpeed)
        {
            if (Time.time - startTime > maxJumpTimeSeconds)
                return;

            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
        }
    }


    public override bool CanPlay(Context context)
    {
        return Character.Grounded && Enabled;
    }

    public override void Stop()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
        jumping = false;
    }

    protected override void Execute(Context context)
    {
        startTime = Time.time;
        jumpSpeed = context.jumpSpeed;
        jumping = true;
        gainSpeed = true;
    }

    public void StopAccelerate()
    {
        gainSpeed = false;
    }
}
