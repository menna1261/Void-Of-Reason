using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    //Damage

    public int WeoponDamage;

    public Camera WeoponCamera;
    public GameObject BulletPreFab;
   // public GameObject MuzzleEffect;
    public Transform BulletSpawn;
    public float BulletVelocity;
    public float BulletPreFabTime;
    private Animator animator;

    bool isScoping = false;
    public GameObject ScopingMode;
    public GameObject fpsArm;


    //Reference to player camera
    public Camera playerCamera;

    //shootings
    public bool ReadyToShoot, isShooting;
    private bool AllowReset = false;
    public float ShootingDelay = 3f;
    private float normalFov;
    private float ScopedFov = 15f;

    //Burst
    public int BulletsPerBurst = 3;
    public int BurstBulletsLeft;

    //Spread
    public float SpreadIntensity;

    //Shooting Modes
    public enum ShootingModes
    {
       Single,
       Auto,
       Burst
    }

    private ShootingModes currentShootingMode;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentShootingMode == ShootingModes.Auto)
        {
            // Holding down the key
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        
        else if (currentShootingMode == ShootingModes .Burst ||
            currentShootingMode == ShootingModes.Single) {

            //Pressing the key only once
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            normalFov = playerCamera.fieldOfView;
            isScoping = !isScoping;
            if (isScoping)
            {
                ScopingMode.SetActive(isScoping);
                WeoponCamera.enabled = !isScoping;
                
                playerCamera.fieldOfView = ScopedFov;


            }
            else
            {
                ScopingMode.SetActive(false);
                playerCamera.fieldOfView = 60;
                WeoponCamera.enabled = true;

            }


        }

        //The actual shooting 
        if (ReadyToShoot && isShooting)
        {
          
            BurstBulletsLeft = BulletsPerBurst;
            fireWeapon();
        }
    

    }

    private void Awake()
    {
        //We're ready to shoot, initially
        ReadyToShoot = true;
        //Assign the current Burst to the BurstCount
        BurstBulletsLeft = BulletsPerBurst;
        animator = GetComponent<Animator>();
    }

    private void fireWeapon()
    {
        Debug.Log("fire weapon is being called");
        //MuzzleEffect.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("shooting");
        Debug.Log("shooting animation is playing");
       // SoundManager.instance.AK_sound.Play();

        //Get the shooting direction 
        Vector3 ShootingDirection = CalculateDirectionAndSpread().normalized;
        
        //Instantiate the bullet scene
        GameObject Bullet = Instantiate(BulletPreFab, BulletSpawn.position, Quaternion.identity);
        
        //Assigning the bullet damage

        Bullet bul = Bullet.GetComponent<Bullet>();
        bul.BulletDamage = WeoponDamage;

        //Give the bullet its direction
        Bullet.transform.forward = ShootingDirection;

        //Shoot the bullet
        Bullet.GetComponent<Rigidbody>().AddForce(ShootingDirection * BulletVelocity, ForceMode.Impulse);

        //Destroy the bullet after a specified time interval
        StartCoroutine(DestroyBulletAferTime(Bullet, BulletPreFabTime));

        //Check if we're done shooting
        if (AllowReset)
        {
            Invoke("ResetShooting", ShootingDelay);
            AllowReset = false;
        }

        //If we're still in bursting mode 
        if (currentShootingMode == ShootingModes.Burst && BurstBulletsLeft > 1)
        {
            BurstBulletsLeft--;
            //Recurse over the function till there are no bullets left
            Invoke("fireWeapon", ShootingDelay);
            
        }

    }

    private void ResetShooting()
    {
        AllowReset = true;
        ReadyToShoot = true;
    }

    private Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit hit;
        Vector3 targetPoint;

        if(Physics.Raycast(ray,out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }
        Vector3 direction = targetPoint - BulletSpawn.position;
        float x = UnityEngine.Random.Range(-SpreadIntensity, SpreadIntensity);
        float y = UnityEngine.Random.Range(-SpreadIntensity, SpreadIntensity);


        return direction + new Vector3(x, y, 0f);

    }

    private IEnumerator DestroyBulletAferTime(GameObject bullet, float bulletPreFabTime)
    {
        yield return new WaitForSeconds(bulletPreFabTime);
        Destroy(bullet);
    }
}
