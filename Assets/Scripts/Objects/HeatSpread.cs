using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HeatSpread : BaseComponent
{
    private Collider2D playerCollider;
    private Warmth warmth;

    public string playerTag;
    public float healRate;

    private void Awake()
    {
        var player = GameObject.FindWithTag(playerTag);
        playerCollider = player.GetComponent<Collider2D>();
        warmth = player.GetComponent<Warmth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            warmth.healRate += healRate;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            warmth.healRate -= healRate;
        }
    }
}