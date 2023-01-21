using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectableView : BaseComponent
{
    public Image iconImage;
    public TMP_Text amountLabel;

    public void Initialize(Sprite icon)
    {
        iconImage.sprite = icon;
    }

    public void UpdateAmount(int amount)
    {
        amountLabel.text = amount.ToString();
    }
}