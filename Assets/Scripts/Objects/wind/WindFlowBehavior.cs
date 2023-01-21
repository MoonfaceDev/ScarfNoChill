using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WindFlowBehavior: BaseComponent
{
    public WindBehavior wind;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject player = collision.gameObject;

        if (player.tag == "Player" && !wind.alreayHit)
        {
            Warmth playerWarmth = player.GetComponent<Warmth>();
            Scarf playerScarf = player.GetComponent<Scarf>();

            if (playerScarf.Active)
                playerWarmth.warmth -= wind.damage * wind.scarfDamageMultiplier;
            else
                playerWarmth.warmth -= wind.damage;

            Debug.Log("wind hit!");
            wind.alreayHit = true;
        }
    }
}
