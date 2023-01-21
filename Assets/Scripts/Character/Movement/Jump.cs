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

    private new Rigidbody2D rigidbody2D;

    public float maxJumpTimeSeconds;
    public override bool Playing => Jumping;

    private static readonly int JumpingHash = Animator.StringToHash("jumping");

    private float startTime;
    private bool jumping;
    private bool gainSpeed;
    private float jumpSpeed;

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
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Character.Grounded && jumping)
            Stop();

        if (gainSpeed)
        {
            if (Time.time - startTime > maxJumpTimeSeconds)
                return;

            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpSpeed);
        }
    }


    public override bool CanPlay(Context context)
    {
        return Character.Grounded && Enabled;
    }

    public override void Stop()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
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
