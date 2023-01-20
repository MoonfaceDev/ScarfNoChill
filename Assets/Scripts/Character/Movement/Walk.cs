using UnityEngine;

public class WalkContext
{
    public float direction;

    public WalkContext(float direction)
    {
        this.direction = direction == 0 ? 0 : Mathf.Sign(direction);
    }
}

[RequireComponent(typeof(Rigidbody2D))]
public class Walk : PlayableBehaviour<WalkContext>
{
    public float speed;

    public override bool Playing => Walking;

    public bool Walking
    {
        get => walking;
        set
        {
            walking = value;
            animator.SetBool(WalkingHash, walking);
        }
    }

    private bool walking;
    private new Rigidbody2D rigidbody;

    private static readonly int WalkingHash = Animator.StringToHash("walking");

    protected override void Awake()
    {
        base.Awake();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public override bool CanPlay(WalkContext context)
    {
        return context.direction != 0;
    }

    protected override void Execute(WalkContext context)
    {
        Walking = true;
        rigidbody.velocity = new Vector2(context.direction * speed, rigidbody.velocity.y);
    }

    public override void Stop()
    {
        Walking = false;
        rigidbody.velocity = Vector2.zero;
    }
}