using System;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : BaseComponent
{
    public Scarf scarf;
    public Vector3 positionOffset;
    public Scrollbar scrollbar;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        scrollbar.gameObject.SetActive(ShouldDisplay());
        rectTransform.anchoredPosition = scarf.transform.position + positionOffset;
        scrollbar.size = scarf.stamina / scarf.maxStamina;
    }

    private bool ShouldDisplay()
    {
        return scarf.stamina < scarf.maxStamina;
    }
}