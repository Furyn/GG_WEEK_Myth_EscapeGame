using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public GameObject pauseScreen;
    public GameObject settingsScreen;

    public override void Init()
    {
        base.Init();
        ResumeScreens();
    }

    public void PauseScreen()
    {

        if(settingsScreen.activeSelf)
        {
            settingsScreen.SetActive(false);
        }

        pauseScreen.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SettingsScreen()
    {
        if (pauseScreen.activeSelf)
        {
            pauseScreen.SetActive(false);
        }

        settingsScreen.SetActive(true);
    }

    public void ResumeScreens()
    {
        pauseScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }
}
