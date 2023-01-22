using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WindFlowBehavior : BaseComponent
{
    public float lifeSpanSeconds;
    [HideInInspector] public WindBehavior wind;

    private void OnEnable()
    {
        Destroy(gameObject, lifeSpanSeconds);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject;

        if (!player.CompareTag("Player") || wind.alreadyHit) return;
        var playerWarmth = player.GetComponent<Warmth>();
        var playerScarf = player.GetComponent<Scarf>();

        playerWarmth.TakeDamage((playerScarf.Active ? wind.scarfDamageMultiplier : 1) * wind.damage);

        Debug.Log("wind hit!");
        wind.alreadyHit = true;
    }
}