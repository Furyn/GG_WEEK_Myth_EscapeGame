using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ChangeSceneEndVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nameOhTheSceneToLoad = "TRUC";
    private bool isStarting = false;
    private AsyncOperation op;

    private void Start()
    {
        op = SceneManager.LoadSceneAsync(nameOhTheSceneToLoad);
        op.allowSceneActivation = false;
    }

    private void Update()
    {

        if (videoPlayer.isPlaying)
        {
            isStarting = true;
        }

        if ( (!videoPlayer.isPlaying && isStarting) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            op.allowSceneActivation = true;
            SingeltonMusique.instance.PlayMusique();
        }
    }
}