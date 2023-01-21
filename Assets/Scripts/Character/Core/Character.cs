using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Character : BaseComponent
{
    public LayerMask groundLayer;
    public Animator Animator { get; private set; }

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
    }

    private void Update()
    {
        Grounded = Physics2D.Raycast(transform.position, Vector3.down, 0.05f, groundLayer);
    }
    
    private static Quaternion GetRotation(float direction)
    {
        return Quaternion.Euler(0, 90 * (1 - direction), 0);
    }
}