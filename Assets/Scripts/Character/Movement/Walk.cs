using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Walk : PlayableBehaviour<Walk.Context>
{
    public class Context
    {
        public readonly float direction;

        public Context(float direction)
        {
            this.direction = direction == 0 ? 0 : Mathf.Sign(direction);
        }
    }

    public float speed;

    public override bool Playing => Walking;

    public bool Walking
    {
        get => walking;
        private set
        {
            walking = value;
            Animator.SetBool(WalkingHash, walking);
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

    public override bool CanPlay(Context context)
    {
        return context.direction != 0;
    }

    protected override void Execute(Context context)
    {
        Walking = true;
        rigidbody.velocity = new Vector2(context.direction * speed, rigidbody.velocity.y);
        transform.localRotation = GetRotation(context.direction);
    }

    private static Quaternion GetRotation(float direction)
    {
        return Quaternion.Euler(0, 90 * (1 - direction), 0);
    }

    public override void Stop()
    {
        Walking = false;
        rigidbody.velocity = Vector2.zero;
    }
}