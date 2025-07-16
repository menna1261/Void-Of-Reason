using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; set; }
    public GlobalRefrences globalRefrences;

    public AudioSource Music;
    public AudioSource SFX;

    public AudioClip BGMusic;
    public AudioClip WalkingSound;
    public AudioClip RunningSound;
    public AudioClip GlassBreak;


    private void Update()
    {
        PlayRunningSound();
    }


    private void PlayRunningSound()
    {
        if (globalRefrences.isRunning)
        {
            Debug.Log("running ");
            SFX.clip = RunningSound; SFX.Play();
        }

        if(!globalRefrences.isWalking) {

            Debug.Log("Walking");
            SFX.clip = WalkingSound;
            SFX.Play();
        
        }

    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
