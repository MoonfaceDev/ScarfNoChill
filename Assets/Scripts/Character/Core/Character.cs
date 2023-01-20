using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Character : BaseComponent
{
    public bool Grounded { get; private set; }

    public Collider2D[] groundColliders;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsGroundCollider(collision.collider))
        {
            Grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (IsGroundCollider(collision.collider))
        {
            Grounded = false;
        }
    }

    private bool IsGroundCollider(Object col)
    {
        return groundColliders.Any(groundCollider => groundCollider == col);
    }
}