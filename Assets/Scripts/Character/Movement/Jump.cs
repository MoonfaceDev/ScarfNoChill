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

    private Rigidbody2D rb;

    public float maxJumpTimeSeconds;
    public override bool Playing => Jumping;

    private static readonly int JumpingHash = Animator.StringToHash("jumping");

    private float startTime;
    private bool jumping;

    //helps determine when the first jump call was made
    private bool startedJump;

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
        rb = gameObject.GetComponent<Rigidbody2D>();
        startedJump = false;
    }

    private void Update()
    {
        if (Character.Grounded && jumping)
            Stop();
    }


    public override bool CanPlay(Context context)
    {
        return Character.Grounded && Enabled;
    }

    public override void Stop()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        jumping = false;
        startedJump = false;
    }

    protected override void Execute(Context context)
    {
        if (startedJump)
        {
            startTime = Time.time;
            ContinueJump(context.jumpSpeed);
        }
        else
        {
            startedJump = true;
            jumping = true;
        }
    }

    //called each frame while holding button
    public void ContinueJump(float jumpSpeed)
    {
        //check for max jump height
        if (Time.time - startTime > maxJumpTimeSeconds)
            return;

        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    }
}
