using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Scrollbar))]
public class StaminaBar : BaseComponent
{
    public Scarf scarf;
    public Vector3 positionOffset;
    public Image[] images;

    private Scrollbar scrollbar;

    private void Awake()
    {
        scrollbar = GetComponent<Scrollbar>();
    }

    private void Update()
    {
        foreach (var image in images)
        {
            image.enabled = ShouldDisplay();
        }

        transform.position = scarf.transform.position + positionOffset;
        scrollbar.size = scarf.stamina / scarf.maxStamina;
    }

    private bool ShouldDisplay()
    {
        return scarf.stamina < scarf.maxStamina;
    }
}