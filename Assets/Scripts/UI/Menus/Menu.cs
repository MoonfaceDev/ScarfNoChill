using System.Linq;
using UnityEngine;

public class Menu : BaseComponent
{
    public string[] buttonsToOpen;
    public string[] buttonsToClose;

    public GameObject menuObject;

    private void Update()
    {
        if (buttonsToOpen.Any(Input.GetButtonDown) && !menuObject.activeSelf)
        {
            menuObject.SetActive(true);
            Time.timeScale = 0;
            return;
        }

        if (buttonsToClose.Any(Input.GetButtonDown) && menuObject.activeSelf)
        {
            menuObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}