using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    [HideInInspector]
    public bool isPaused;

    public override void Init()
    {
        base.Init();
        //ResumeGame();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        isPaused = true;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player.GetComponent<PlayerInventory>().inventory.Count > 1)
        {
            player.GetComponent<PlayerInventory>().playerInventory.GetComponent<Animator>().speed = 0.0f;
        }

        player.GetComponent<Rigidbody>().isKinematic = true;
        UIManager.Instance.PauseScreen();
    }

    public void ResumeGame()
    {
        isPaused = false;
        UIManager.Instance.ResumeScreens();

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player.GetComponent<PlayerInventory>().inventory.Count > 1)
        {
            player.GetComponent<PlayerInventory>().playerInventory.GetComponent<Animator>().speed = 1.0f;
            player.GetComponent<PlayerInventory>().Animate();
        }

        player.GetComponent<Rigidbody>().isKinematic = false;

    }
}
