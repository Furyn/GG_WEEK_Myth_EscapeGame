using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    private AsyncOperation opVideo;
    [SerializeField]
    private string nameVideoScene = "VideoScene";

    [SerializeField]
    private string nameCredits = "Credits";

    private void Start()
    {
        opVideo = SceneManager.LoadSceneAsync(nameVideoScene);
        opVideo.allowSceneActivation = false;
    }

    public void PlayButton()
    {
        opVideo.allowSceneActivation = true;

        SingeltonMusique.instance.StopMusique();
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene(nameCredits);
    }

    public void OhNoHeBeQuittingTheGame()
    {
        Application.Quit();
    }
}
