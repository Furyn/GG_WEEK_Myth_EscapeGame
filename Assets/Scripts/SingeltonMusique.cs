using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SingeltonMusique : MonoBehaviour
{
    public static SingeltonMusique instance;

    public static AudioSource adS;

    private void Awake()
    {
        if (instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            adS = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayMusique()
    {
        adS.Play();
    }

    public void StopMusique()
    {
        adS.Stop();
    }

}
