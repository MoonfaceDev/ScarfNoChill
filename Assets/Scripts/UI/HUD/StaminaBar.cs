using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : BaseComponent
{
    public Scarf scarf;
    public Vector3 positionOffset;
    public Scrollbar scrollbar;

    private void Update()
    {
        scrollbar.gameObject.SetActive(ShouldDisplay());
        transform.position = scarf.transform.position + positionOffset;
        scrollbar.size = scarf.stamina / scarf.maxStamina;
    }

    private bool ShouldDisplay()
    {
        return scarf.stamina < scarf.maxStamina;
    }
}