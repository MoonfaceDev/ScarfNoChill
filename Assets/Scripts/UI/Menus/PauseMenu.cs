using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject panel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            panel.SetActive(!panel.activeSelf);

        if (panel.activeSelf)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
