using System;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class BootsRecipe : Recipe
{
    public float multiplier;

    public override void Craft(GameObject player)
    {
        player.GetComponent<Walk>().speed *= multiplier;
    }
}