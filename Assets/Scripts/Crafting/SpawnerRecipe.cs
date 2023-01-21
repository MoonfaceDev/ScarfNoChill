using System;
using UnityEngine;

[Serializable]
public class SpawnerRecipe : Recipe
{
    public string objectType;
    public int index;
    
    public override void Craft(GameObject player)
    {
        SpawnersManager.Upgrade(objectType);
    }
}