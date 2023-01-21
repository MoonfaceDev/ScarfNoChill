using UnityEngine;
using UnityEngine.Events;

public class GroundTrigger : BaseComponent
{
    public Collider2D groundCollider;
    public UnityEvent onEnter;
    public UnityEvent onExit;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col == groundCollider)
        {
            onEnter.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col == groundCollider)
        {
            onExit.Invoke();
        }
    }
}