using UnityEngine;

public class Saiga : BaseWeapon
{
    public Animator animator;
    public GlobalRefrences globalRefrences;


    protected override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("we are trying to reload");
            Reload();
        }

        PlayWalkingOrRunningAnimation();
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
            animator.ResetTrigger("Shooting");
            animator.SetTrigger("Shooting");
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
            animator.SetTrigger("Reloading");
        }
    }

     protected void PlayWalkingOrRunningAnimation()
     {
         if (globalRefrences.isWalking == true )
         {
            animator.SetBool("Walking", true);
         }

         else if (globalRefrences.isRunning == true )
         {
             animator.SetBool("Running", true);
         }

         else if (globalRefrences.isMoving == false)
        {
            animator.SetTrigger("Stop");
        }
     }
 }


