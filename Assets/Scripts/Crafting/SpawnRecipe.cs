using System;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class SpawnRecipe : Recipe
{
    public GameObject resultPrefab;

    public override void Craft(GameObject player)
    {
        Object.Instantiate(resultPrefab, player.transform.position, Quaternion.identity);
    }
}