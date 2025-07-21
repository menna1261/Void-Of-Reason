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
    public AudioSource equipment;
    public AudioSource ReloadingSmg;

    public AudioClip BGMusic;
    public AudioClip WalkingSound;
    public AudioClip RunningSound;
    public AudioClip GlassBreak;
    public AudioClip ChamberSound;
    public AudioClip reloadigSmg;
    public AudioClip ShootingSmg;
    public AudioClip ShootingSaiga;
    public AudioClip ShootingAK;




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
    public void PlayEquipSound()
    {
        Debug.Log("equip sound ");
        equipment.Play();
    }

    public IEnumerator ReloadSmg()
    {
        Debug.Log("Playing reload sound");
        ReloadingSmg.Play();
        yield return new WaitForSeconds(reloadigSmg.length);
        ReloadingSmg.clip = ChamberSound;
        Debug.Log("Playing Chamber sound");
        ReloadingSmg.Play();
        yield return new WaitForSeconds(ChamberSound.length);
        ResetClips();
    }

    public void ShootSmg()
    {
        SFX.clip = ShootingSmg;
        SFX.Play();
    }

    public void ResetClips()
    {
        ReloadingSmg.clip = reloadigSmg;
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
