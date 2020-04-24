using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField]
    private int Timer;

    private void Update()
    {
        if (Input.anyKeyDown || Timer <= 0)
            SceneManager.LoadScene(0);
    }
}
