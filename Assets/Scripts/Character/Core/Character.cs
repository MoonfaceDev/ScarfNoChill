using UnityEngine;

public class Character : BaseComponent
{
    public LayerMask groundLayers;
    public bool Grounded { get; private set; }

    private void Update()
    {
        Grounded = Physics2D.OverlapPoint(transform.position, groundLayers);
    }
}