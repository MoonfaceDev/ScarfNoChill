using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingMenu : BaseComponent
{
    [Serializable]
    public class CollectableEntry
    {
        public string objectType;
        public Sprite icon;
    }

    [Serializable]
    public class RecipeEntry
    {
        public Craft.Recipe recipe;
        public string resultName;
        public Sprite resultIcon;
    }

    public Inventory inventory;
    public Craft craft;

    public CollectableEntry[] collectables;
    public Transform collectablesLayout;
    public GameObject collectablePrefab;

    public RecipeEntry[] recipes;
    public Transform recipesLayout;
    public GameObject recipePrefab;

    private Dictionary<string, CollectableView> collectableViews;
    private Dictionary<Craft.Recipe, CraftingRecipeView> recipeViews;

    private void Awake()
    {
        InitializeCollectables();
        InitializeRecipes();
    }

    private void InitializeCollectables()
    {
        collectableViews = new Dictionary<string, CollectableView>();

        foreach (var collectable in collectables)
        {
            var collectableInstance = Instantiate(collectablePrefab, collectablesLayout);
            var collectableView = collectableInstance.GetComponent<CollectableView>();
            collectableView.Initialize(collectable.icon);
            collectableViews[collectable.objectType] = collectableView;
        }
    }

    private void InitializeRecipes()
    {
        recipeViews = new Dictionary<Craft.Recipe, CraftingRecipeView>();

        foreach (var recipe in recipes)
        {
            var recipeInstance = Instantiate(recipePrefab, recipesLayout);
            var recipeView = recipeInstance.GetComponent<CraftingRecipeView>();
            recipeView.Initialize(GetIngredientsData(recipe.recipe.ingredients), recipe.resultName, recipe.resultIcon,
                () =>
                {
                    craft.CraftRecipe(recipe.recipe);
                    gameObject.SetActive(false);
                });
            recipeViews[recipe.recipe] = recipeView;
        }
    }

    private IEnumerable<CraftingIngredientView.Data> GetIngredientsData(
        IEnumerable<Craft.Recipe.Ingredient> ingredients)
    {
        var ingredientsData = new List<CraftingIngredientView.Data>();
        foreach (var ingredient in ingredients)
        {
            var collectable = collectables.Single(collectable => collectable.objectType == ingredient.objectType);
            ingredientsData.Add(new CraftingIngredientView.Data(ingredient, collectable.icon));
        }

        return ingredientsData;
    }

    private void Update()
    {
        UpdateCollectables();
        UpdateRecipes();
    }

    private void UpdateCollectables()
    {
        foreach (var entry in collectableViews)
        {
            entry.Value.UpdateAmount(inventory.storage[entry.Key]);
        }
    }

    private void UpdateRecipes()
    {
        foreach (var entry in recipeViews)
        {
            entry.Value.UpdateData(craft.CanCraft(entry.Key));
        }
    }
}