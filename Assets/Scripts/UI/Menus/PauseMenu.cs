using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI title;

    bool end = false;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !end)
            TogglePanel();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void TogglePanel()
    {
        panel.SetActive(!panel.activeSelf);
        Time.timeScale = panel.activeSelf ? 0 : 1;
    }

    public void DeadPanel()
    {
        end = true;
        title.text = "Too Much Chilling";
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void WinPanel()
    {
        end = true;
        title.text = "You Survived!";
        panel.SetActive(true);
        Time.timeScale = 0;
    }
}
