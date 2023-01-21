using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingRecipeView : BaseComponent
{
    public Transform ingredientsLayout;
    public GameObject ingredientPrefab;
    public TMP_Text resultLabel;
    public TMP_Text errorLabel;
    public Button craftButton;
    public Image resultImage;

    public void Initialize(IEnumerable<CraftingIngredientView.Data> ingredientsData, string resultName,
        Sprite resultIcon, Action onCraft)
    {
        foreach (var ingredient in ingredientsData)
        {
            var ingredientInstance = Instantiate(ingredientPrefab, ingredientsLayout);
            ingredientInstance.GetComponent<CraftingIngredientView>().UpdateData(ingredient);
        }

        resultLabel.text = resultName;
        resultImage.sprite = resultIcon;
        craftButton.onClick.AddListener(delegate { onCraft(); });
    }

    public void UpdateData(bool canCraft, string error)
    {
        craftButton.interactable = canCraft;
        errorLabel.text = error;
    }
}