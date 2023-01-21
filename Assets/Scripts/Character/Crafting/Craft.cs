using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CannotCraftException : Exception
{
}

[RequireComponent(typeof(Inventory))]
public class Craft : CharacterBehaviour
{
    private Inventory inventory;
    private Score score;

    protected override void Awake()
    {
        base.Awake();
        inventory = GetComponent<Inventory>();
        score = GetComponent<Score>();
    }

    public bool HasIngredients(Recipe recipe)
    {
        return recipe.ingredients.All(ingredient =>
            inventory.storage[ingredient.objectType] >= ingredient.amount
        );
    }

    public bool HasRequiredScore(Recipe recipe)
    {
        return score.score >= recipe.scoreRequirement;
    }

    public void CraftRecipe(Recipe recipe)
    {
        if (!HasIngredients(recipe))
        {
            throw new CannotCraftException();
        }
        ConsumeIngredients(recipe.ingredients);
        recipe.Craft(gameObject);
    }

    private void ConsumeIngredients(IEnumerable<Recipe.Ingredient> ingredients)
    {
        foreach (var ingredient in ingredients)
        {
            inventory.storage[ingredient.objectType] -= ingredient.amount;
        }
    }
}