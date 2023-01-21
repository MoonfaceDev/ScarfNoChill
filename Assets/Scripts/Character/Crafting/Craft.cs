using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CannotCraftException : Exception {
}

[RequireComponent(typeof(Inventory))]
public class Craft : CharacterBehaviour
{
    [Serializable]
    public class Recipe
    {
        [Serializable]
        public class Ingredient
        {
            public string objectType;
            public int amount;
        }

        public Ingredient[] ingredients;
        public GameObject resultPrefab;
    }

    private Inventory inventory;

    protected override void Awake()
    {
        base.Awake();
        inventory = GetComponent<Inventory>();
    }

    public bool CanCraft(Recipe recipe)
    {
        return recipe.ingredients.Any(ingredient =>
            inventory.storage[ingredient.objectType] >= ingredient.amount
        );
    }

    public void CraftRecipe(Recipe recipe)
    {
        if (!CanCraft(recipe))
        {
            throw new CannotCraftException();
        }
        ConsumeIngredients(recipe.ingredients);
        Instantiate(recipe.resultPrefab, transform.position, Quaternion.identity);
    }

    private void ConsumeIngredients(IEnumerable<Recipe.Ingredient> ingredients)
    {
        foreach (var ingredient in ingredients)
        {
            inventory.storage[ingredient.objectType] -= ingredient.amount;
        }
    }
}