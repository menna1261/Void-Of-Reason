using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    //Get a reference of Radial Menu

    public RadialMenu menu;  // idk why tf would i need that

    public int WeaponDamage;
    public float BulletVelocity;
    public float BulletPreFabTime;
    public float SpreadIntensity;
    public float ShootingDelay;
    public Transform BulletSpawn;
    public GameObject BulletPreFab;
    public Camera PlayerCamera;
    public Camera WeoponCamera;
    public GameObject ScopingMode;

   public enum ShootingModes { Single, Auto, Burst }
    public ShootingModes currentShootingMode;
    public int BulletsPerBurst = 3;
    protected int BurstBulletsLeft;

    protected bool ReadyToShoot = true;
    protected bool isShooting = false;
    protected bool AllowReset = true;

    private float normalFov;
    private float ScopedFov = 15f;

    private bool isScoping = false;



    protected virtual void Start()
    {
        ReadyToShoot = true;
        BurstBulletsLeft = BulletsPerBurst;
    }

    protected virtual void Update()
    {
        HandleShootingInput();

        if (ReadyToShoot && isShooting)
        {
            BurstBulletsLeft = BulletsPerBurst;
            FireWeapon();
        }
        Aim();
    }

    void Aim()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            normalFov = PlayerCamera.fieldOfView;
            isScoping = !isScoping;
            if (isScoping)
            {
                ScopingMode.SetActive(isScoping);
                WeoponCamera.enabled = !isScoping;

                PlayerCamera.fieldOfView = ScopedFov;


            }
            else
            {
                ScopingMode.SetActive(false);
                PlayerCamera.fieldOfView = 60;
                WeoponCamera.enabled = true;

            }

            
        }
    }
    protected virtual void HandleShootingInput()
    {
        isShooting = Input.GetKeyDown(KeyCode.Mouse0);
    }

    protected virtual void FireWeapon()
    {
        GameObject bullet = Instantiate(BulletPreFab, BulletSpawn.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().BulletDamage = WeaponDamage;
        Vector3 direction = CalculateDirectionAndSpread().normalized;
        bullet.transform.forward = direction;
        bullet.GetComponent<Rigidbody>().AddForce(direction * BulletVelocity, ForceMode.Impulse);
        StartCoroutine(DestroyBulletAfterTime(bullet, BulletPreFabTime));

        PlayMuzzleEffect();
        PlayShootSound();
        PlayShootAnimation();

        if (AllowReset)
        {
            Invoke("ResetShooting", ShootingDelay);
            AllowReset = false;
        }

        if (currentShootingMode == ShootingModes.Burst && BurstBulletsLeft > 1)
        {
            BurstBulletsLeft--;
            Invoke(nameof(FireWeapon), ShootingDelay);
        }
    }

    protected virtual void ResetShooting()
    {
        ReadyToShoot = true;
        AllowReset = true;
    }

    protected virtual Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Vector3 targetPoint = Physics.Raycast(ray, out RaycastHit hit) ? hit.point : ray.GetPoint(100);
        Vector3 direction = targetPoint - BulletSpawn.position;
        direction += new Vector3(UnityEngine.Random.Range(-SpreadIntensity, SpreadIntensity),
                                 UnityEngine.Random.Range(-SpreadIntensity, SpreadIntensity),
                                 0f);
        return direction;
    }

    protected IEnumerator DestroyBulletAfterTime(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(bullet);
    }

    protected abstract void PlayShootSound();
    protected abstract void PlayMuzzleEffect();
    protected abstract void PlayShootAnimation();
}

