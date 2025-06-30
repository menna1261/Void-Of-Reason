using UnityEngine;

public class Pistol : BaseWeapon
{
    public Animator animator;

    protected override void Start()
    {
        base.Start();
        //animator = GetComponentInChildren<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("we are trying to reload");
            Reload();
        }
    }

    protected override void PlayShootSound()
    {
        // Play audio here
    }

    protected override void PlayMuzzleEffect()
    {
        // Play VFX here
    }

    protected override void PlayShootAnimation()
    {
        Debug.Log("Trying to shoot");
        if (animator != null)
        {
            animator.ResetTrigger("Shoot");
            animator.SetTrigger("Shoot");
            Debug.Log("Shoot trigger set");
        }
        else
        {
            Debug.LogWarning("Animator is NULL");
        }
    }


    protected void Reload()
    {
        if (animator != null)
        {
            Debug.Log("we are fucking reloading ");
            animator.SetTrigger("Reload");
        }
    }
}
