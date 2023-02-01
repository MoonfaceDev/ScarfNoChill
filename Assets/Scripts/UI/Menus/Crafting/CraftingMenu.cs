using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class CraftingMenu : BaseComponent
{
    [Serializable]
    public class CollectableEntry
    {
        public string objectType;
        public Sprite icon;
    }

    [Serializable]
    public class RecipeEntry<T> where T : Recipe
    {
        public T recipe;
        public string resultName;
        public Sprite resultIcon;
    }

    public CraftPanel craftingPanel;
    public Inventory inventory;
    public Craft craft;
    public Scarf scarf;

    public CollectableEntry[] collectables;
    public Transform collectablesLayout;
    public GameObject collectablePrefab;

    [FormerlySerializedAs("recipes")] public RecipeEntry<SpawnRecipe>[] spawnRecipes;
    public RecipeEntry<SpawnerRecipe>[] spawnerRecipes;
    public RecipeEntry<ScarfRecipe>[] scarfRecipes;
    public RecipeEntry<BootsRecipe> bootsRecipe;
    public Transform recipesLayout;
    public GameObject recipePrefab;

    private Dictionary<string, CollectableView> collectableViews;
    private List<Tuple<Recipe, CraftingRecipeView>> recipeViews;
    private SpawnersManager collectableSpawnersManager;

    private void Awake()
    {
        recipeViews = new List<Tuple<Recipe, CraftingRecipeView>>();
        collectableSpawnersManager = FindObjectOfType<SpawnersManager>();
        InitializeCollectables();
        InitializeRecipes();
    }

    public void InitializeCollectables()
    {
        collectableViews = new Dictionary<string, CollectableView>();

        foreach (var collectable in collectables)
        {
            var collectableInstance = Instantiate(collectablePrefab, collectablesLayout);
            var collectableView = collectableInstance.GetComponent<CollectableView>();
            collectableView.Initialize(collectable.icon);
            collectableView.UpdateAmount(0);
            collectableViews[collectable.objectType] = collectableView;
        }
    }

    private void InitializeRecipes()
    {
        foreach (var (_, recipeView) in recipeViews)
        {
            Destroy(recipeView.gameObject);
        }
        recipeViews.Clear();

        foreach (var recipe in spawnRecipes)
        {
            recipeViews.Add(new Tuple<Recipe, CraftingRecipeView>(recipe.recipe, InitializeRecipe(recipe)));
        }

        collectableSpawnersManager.Initialize();

        foreach (var recipe in spawnerRecipes)
        {
            if (recipe.recipe.index == SpawnersManager.tiers[recipe.recipe.objectType])
            {
                recipeViews.Add(
                    new Tuple<Recipe, CraftingRecipeView>(recipe.recipe, InitializeRecipe(recipe)));
            }
        }

        foreach (var recipe in scarfRecipes)
        {
            if (recipe.recipe.index == scarf.tier)
            {
                recipeViews.Add(new Tuple<Recipe, CraftingRecipeView>(recipe.recipe, InitializeRecipe(recipe)));
            }
        }
        
        recipeViews.Add(new Tuple<Recipe, CraftingRecipeView>(bootsRecipe.recipe, InitializeRecipe(bootsRecipe)));
    }

    private CraftingRecipeView InitializeRecipe<T>(RecipeEntry<T> recipe) where T : Recipe
    {
        var recipeInstance = Instantiate(recipePrefab, recipesLayout);
        var recipeView = recipeInstance.GetComponent<CraftingRecipeView>();
        recipeView.Initialize(GetIngredientsData(recipe.recipe.ingredients), recipe.resultName, recipe.resultIcon,
            () =>
            {
                craft.CraftRecipe(recipe.recipe);
                InitializeRecipes();
                craftingPanel.Disable();
            });
        return recipeView;
    }

    private IEnumerable<CraftingIngredientView.Data> GetIngredientsData(
        IEnumerable<Recipe.Ingredient> ingredients)
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
        foreach (var entry in collectableViews)
        {
            entry.Value.UpdateAmount(inventory.storage[entry.Key]);
        }
        
        foreach (var (recipe, view) in recipeViews)
        {
            UpdateRecipeView(recipe, view);
        }
    }

    private void UpdateRecipeView(Recipe recipe, CraftingRecipeView view)
    {
        if (!craft.HasRequiredScore(recipe))
        {
            view.UpdateData(false, $"Required score is {recipe.scoreRequirement}");
            return;
        }

        if (!craft.HasIngredients(recipe))
        {
            view.UpdateData(false, "Missing ingredients");
            return;
        }

        view.UpdateData(true, "");
    }
}