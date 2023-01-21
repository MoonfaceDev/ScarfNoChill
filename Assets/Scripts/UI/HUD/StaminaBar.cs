using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Scrollbar))]
public class StaminaBar : BaseComponent
{
    public Scarf scarf;
    public Vector3 positionOffset;

    private Scrollbar scrollbar;
    private Image scrollbarImage;

    private void Awake()
    {
        scrollbar = GetComponent<Scrollbar>();
        scrollbarImage = GetComponent<Image>();
    }

    private void Update()
    {
        scrollbarImage.enabled = ShouldDisplay();
        scrollbar.targetGraphic.enabled = ShouldDisplay();
        
        transform.position = scarf.transform.position + positionOffset;
        scrollbar.size = scarf.stamina / scarf.maxStamina;
    }

    private bool ShouldDisplay()
    {
        return scarf.stamina < scarf.maxStamina;
    }
}