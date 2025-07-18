using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; set; }
    public GlobalRefrences globalRefrences;

    public AudioSource Music;
    public AudioSource SFX;
    public AudioSource GlasSBreak;

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
            if (SFX.clip != RunningSound || !SFX.isPlaying)
            {
                SFX.clip = RunningSound;
                SFX.Play();
            }
        }
        else if (globalRefrences.isWalking)
        {
            if (SFX.clip != WalkingSound || !SFX.isPlaying)
            {
                SFX.clip = WalkingSound;
                SFX.Play();
            }
        }
        else
        {
            // Stop SFX if player is neither running nor walking
            if (SFX.isPlaying)
            {
                SFX.Stop();
            }
        }
    }

    public void playGlassSound()
    {
        GlasSBreak.Play();



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
