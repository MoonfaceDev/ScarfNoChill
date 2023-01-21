using System;
using UnityEngine;

[Serializable]
public abstract class Recipe
{
    [Serializable]
    public class Ingredient
    {
        public string objectType;
        public int amount;
    }

    public int scoreRequirement;
    public Ingredient[] ingredients;

    public abstract void Craft(GameObject player);
}