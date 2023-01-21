using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingIngredientView : BaseComponent
{
    public class Data
    {
        public readonly Craft.Recipe.Ingredient ingredient;
        public readonly Sprite icon;

        public Data(Craft.Recipe.Ingredient ingredient, Sprite icon)
        {
            this.ingredient = ingredient;
            this.icon = icon;
        }
    }

    public TMP_Text amountLabel;
    public Image image;
    
    public void UpdateData(Data data)
    {
        amountLabel.text = data.ingredient.amount.ToString();
        image.sprite = data.icon;
    }
}