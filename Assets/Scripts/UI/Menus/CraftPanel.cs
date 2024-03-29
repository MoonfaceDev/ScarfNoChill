﻿using System.Linq;
using UnityEngine;

public class CraftPanel : CharacterBehaviour
{
    public string[] buttonsToOpen;
    public string[] buttonsToClose;

    public GameObject menuObject;

    private void Update()
    {
        if (!Character.Grounded)
            return;

        if (buttonsToOpen.Any(Input.GetButtonDown) && !menuObject.activeSelf)
        {
            Enable();
            return;
        }

        if (buttonsToClose.Any(Input.GetButtonDown) && menuObject.activeSelf)
        {
            Disable();
        }
    }

    public void Enable()
    {
        menuObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Disable()
    {
        menuObject.SetActive(false);
        Time.timeScale = 1;
    }
}