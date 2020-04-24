using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class ChangeSceneEndVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nameOhTheSceneToLoad = "TRUC";
    private bool isStarting = false;

    private void Update()
    {

        if (videoPlayer.isPlaying)
        {
            isStarting = true;
        }

        if ( (!videoPlayer.isPlaying && isStarting) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(nameOhTheSceneToLoad));
        }
    }
}