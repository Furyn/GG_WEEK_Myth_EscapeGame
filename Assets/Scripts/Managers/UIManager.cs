using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    GameObject pauseScreen;
    GameObject settingsScreen;

    public override void Init()
    {
        base.Init();

    }

    void PauseScreen()
    {

        if(settingsScreen.activeSelf)
        {
            settingsScreen.SetActive(false);
        }

        pauseScreen.SetActive(true);
    }

    void SettingsScreen()
    {
        if (pauseScreen.activeSelf)
        {
            pauseScreen.SetActive(false);
        }

        settingsScreen.SetActive(true);
    }
}
