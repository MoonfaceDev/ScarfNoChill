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

    public float maxAccelerateSeconds;
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
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    public override bool CanPlay(Context context)
    {
        return Character.Grounded && base.CanPlay(context);
    }

    protected override void Execute(Context context)
    {
        startTime = Time.time;
        jumpSpeed = context.jumpSpeed;
        Jumping = true;
        gainSpeed = true;
    }

    private void Update()
    {
        if (Character.Grounded && Jumping)
            Stop();

        if (gainSpeed)
        {
            if (Time.time - startTime > maxAccelerateSeconds)
                return;

            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
        }
    }

    public override void Stop()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
        Jumping = false;
    }

    public void StopAccelerate()
    {
        gainSpeed = false;
    }
}
