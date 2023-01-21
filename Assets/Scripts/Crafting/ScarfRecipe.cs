using System;
using UnityEngine;

[Serializable]
public class ScarfRecipe : Recipe
{
    public int index;

    public override void Craft(GameObject player)
    {
        player.GetComponent<Scarf>().AdvanceTier();
    }
}