using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; set; }

    public AudioSource AK_sound;
    public AudioSource Walkingsound;
    public AudioClip walking;

    public AudioClip zombieWalking;
    public AudioSource zombieChannel;

    public AudioSource glassChannel;
    public AudioClip glassBreaking;

    public AudioClip bgSound;
    public AudioSource bgsoundChannel;

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
