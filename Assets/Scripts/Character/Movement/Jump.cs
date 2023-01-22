using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jump : PlayableBehaviour<Jump.Context>
{
    public class Context { }

    private new Rigidbody2D rigidbody;

    public Scarf scarf;

    public float thrust;
    public float maxAccelerateSeconds;
    public override bool Playing => Jumping;

    private static readonly int JumpingHash = Animator.StringToHash("jumping");

    private float startTime;
    private bool jumping;
    private bool gainSpeed;

    public bool Jumping
    {
        get => jumping;
        private set
        {
            jumping = value;
            Animator.SetBool(JumpingHash, value);

            if (value)
                Character.audioManager.PlaySFX("jump");
        }
    }

    protected override void Awake()
    {
        base.Awake();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    public override bool CanPlay(Context context)
    {
        return base.CanPlay(context) && Character.Grounded && !scarf.Active;
    }

    protected override void Execute(Context context)
    {
        startTime = Time.time;
        Jumping = true;
        gainSpeed = true;
    }

    private void Update()
    {
        if (Character.Grounded && Jumping && !gainSpeed)
            Stop();
        
        if (Time.time - startTime > maxAccelerateSeconds)
            StopAccelerate();
    }

    private void FixedUpdate()
    {
        if (gainSpeed)
        {
            rigidbody.AddForce(transform.up * thrust);
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
