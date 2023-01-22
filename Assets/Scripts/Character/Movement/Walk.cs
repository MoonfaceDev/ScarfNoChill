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
        return base.CanPlay(context) && context.direction != 0;
    }

    protected override void Execute(Context context)
    {
        Walking = true;
        Character.LookDirection = context.direction;
    }

    private void FixedUpdate()
    {
        if (Walking)
        {
            rigidbody.velocity = new Vector2(Character.LookDirection * speed, rigidbody.velocity.y);
        }
    }

    public override void Stop()
    {
        Walking = false;
        rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
    }
}