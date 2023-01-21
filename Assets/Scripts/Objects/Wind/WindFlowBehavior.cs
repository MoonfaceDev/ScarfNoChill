using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WindFlowBehavior : BaseComponent
{
    [HideInInspector] public WindBehavior wind;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject;

        if (!player.CompareTag("Player") || wind.alreadyHit) return;
        var playerWarmth = player.GetComponent<Warmth>();
        var playerScarf = player.GetComponent<Scarf>();

        playerWarmth.warmth -= (playerScarf.Active ? wind.scarfDamageMultiplier : 1) * wind.damage;

        Debug.Log("wind hit!");
        wind.alreadyHit = true;
    }
}