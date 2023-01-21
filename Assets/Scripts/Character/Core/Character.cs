using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Character : BaseComponent
{
    public LayerMask groundLayer;
    public Animator Animator { get; private set; }

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
        Grounded = Physics2D.Raycast(transform.position, Vector3.down, 0.01f, groundLayer);
    }
}