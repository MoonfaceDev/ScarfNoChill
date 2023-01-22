using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Character : BaseComponent
{
    public GroundTrigger groundTrigger;

    public Animator Animator { get; private set; }

    public AudioManager audioManager;

    public float LookDirection
    {
        get => lookDirection;
        set
        {
            lookDirection = value;
            transform.rotation = GetRotation(lookDirection);
        }
    }

    private float lookDirection;

    public bool Grounded
    {
        get => grounded;
        private set
        {
            grounded = value;
            Animator.SetBool(GroundedHash, grounded);
        }
    }

    private bool grounded;
    private static readonly int GroundedHash = Animator.StringToHash("grounded");

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        groundTrigger.onEnter.AddListener(() => Grounded = true);
        groundTrigger.onExit.AddListener(() => Grounded = false);
    }

    private static Quaternion GetRotation(float direction)
    {
        return Quaternion.Euler(0, 90 * (1 - direction), 0);
    }
}