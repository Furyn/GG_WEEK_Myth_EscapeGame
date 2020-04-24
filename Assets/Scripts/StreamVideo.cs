using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;

    public string nameOhTheSceneToLoad = "TRUC";

    private bool isStarting = false;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(PlayVideo());
    }

    private void Update()
    {
        if (!videoPlayer.isPlaying && isStarting )
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(nameOhTheSceneToLoad));
        }

    }


    IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        audioSource.Play();
        isStarting = true;
    }
}