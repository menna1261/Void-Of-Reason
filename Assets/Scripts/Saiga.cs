using UnityEngine;

public class Saiga : BaseWeapon
{
    public Animator animator;
    public GlobalRefrences globalRefrences;
    //public WeaponManager weaponManager;


    protected override void Start()
    {
        base.Start();

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>(); // or GetComponent<Animator>()
        }

        if (animator == null)
        {
            Debug.LogWarning("Animator is NULL");
        }
        else
        {
            Debug.Log("Animator assigned: " + animator.runtimeAnimatorController.name);
        }

    }

    protected override void Update()
    {
        base.Update();

        foreach (var param in animator.parameters)
        {
            Debug.Log($"Param: {param.name} = {animator.GetBool(param.name)}");
        }


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

        Debug.Log("Idk why tf we are hwere");

        if (animator== null)
        {
            Debug.LogWarning("Animator is NULL in walking/running check");
            return;
        }

       /* animator.SetBool("Walking", globalRefrences.isWalking);
        animator.SetBool("Running", globalRefrences.isRunning);*/

        if (globalRefrences.isWalking)
        {
            animator.SetTrigger("Walking");
        }
        if (globalRefrences.isRunning)
        {
            animator.SetTrigger("Running");
        }
        if (!globalRefrences.isMoving)
        {
            animator.SetTrigger("Stop");
        }
      

        Debug.Log($"isMoving = {globalRefrences.isMoving}, isWalking={globalRefrences.isWalking}, isRunning={globalRefrences.isRunning}");

    }

}
 


