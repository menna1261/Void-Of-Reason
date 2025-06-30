using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47 : BaseWeapon
{
    public Animator animator;
    public GameObject MuzzleEffect;

    protected override void PlayShootSound()
    {
        SoundManager.instance.AK_sound.Play();
    }

    protected override void PlayMuzzleEffect()
    {
        MuzzleEffect.GetComponent<ParticleSystem>().Play();
    }

    protected override void PlayShootAnimation()
    {
        animator.SetTrigger("shooting");
    }
}

