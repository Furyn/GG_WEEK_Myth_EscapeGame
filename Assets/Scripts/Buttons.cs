﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void ChangeScene(string SceneToLoad)
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    public void OhNoHeBeQuittingTheGame()
    {
        Application.Quit();
    }
}
