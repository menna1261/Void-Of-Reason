using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    public GameObject menu;
    public SoundManager soundManager;
    //public Animator animator;
    public Animator weaponAnimator;
    public Camera MainCamera;
    public GameObject ScopeMode;
    public GameObject[] Weapons;



    [System.Serializable]
    public class WeaponEntry
    {
        public string weaponName;
        public GameObject weaponPrefab;
    }

    public List<WeaponEntry> weaponList = new List<WeaponEntry>();
    public Transform weaponHolder; // Where weapons are attached 

    private GameObject currentWeapon;
    private Dictionary<string, GameObject> weaponPrefabs = new Dictionary<string, GameObject>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        foreach (var entry in weaponList)
        {
            if (!weaponPrefabs.ContainsKey(entry.weaponName))
                weaponPrefabs.Add(entry.weaponName, entry.weaponPrefab);
        }

        Debug.Log("Registered weapons:");
        foreach (var key in weaponPrefabs.Keys)
        {
            Debug.Log("- " + key);
        }

    }

    public void EquipWeapon(string weaponName)
    {
        Debug.Log("Trying to equip: " + weaponName);

        if (currentWeapon != null)
        {
            Debug.Log("Destroying current weapon: " + currentWeapon.name);
            Destroy(currentWeapon);
            currentWeapon = null;
        }

        if (weaponPrefabs.ContainsKey(weaponName))
        {
            Debug.Log("Found weapon in dict: " + weaponName);
            GameObject newWeapon = Instantiate(weaponPrefabs[weaponName], weaponHolder);
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.identity;

            currentWeapon = newWeapon;

            SoundManager soundManager = FindObjectOfType<SoundManager>();
            GlobalRefrences globalRefs = FindObjectOfType<GlobalRefrences>();
            //Camera PlayerCam = FindObjectOfType<MainCamera>();

            if (newWeapon.TryGetComponent<Saiga>(out var saiga))
            {
                saiga.globalRefrences = globalRefs;
                saiga.PlayerCamera = MainCamera;
               saiga.ScopingMode = ScopeMode;
                saiga.soundManager = soundManager;
            }

            weaponAnimator = currentWeapon.GetComponent<Animator>();
            /*            if (weaponAnimator != null)
                        {
                            int index = GetWeaponIndexByName(weaponName);
                            weaponAnimator.SetInteger("WeaponIndex", index);
                        }*/

            soundManager.PlayEquipSound();
            Debug.Log("Instantiated: " + newWeapon.name);


            //menu.AddWeapon(GetWeaponIndexByName(weaponName));
            //menu.GetComponent<RadialMenu>().AddWeapon(GetWeaponIndexByName(weaponName));
            Weapons[GetWeaponIndexByName(weaponName)].SetActive(true);
            Debug.Log("Weapon added www");

            var weaponScript = currentWeapon.GetComponent<Saiga>();
            if (weaponScript != null)
            {
                Debug.Log("Weapon script found on: " + currentWeapon.name);
            }
            else
            {
                Debug.LogWarning("Weapon script missing on: " + currentWeapon.name);
            }

        }
        else
        {
            Debug.LogWarning("Weapon not found in dict: " + weaponName);
        }
    }


    private int GetWeaponIndexByName(string name)
    {
        switch (name)
        {
            case "smg": return 0;
            case "Saiga": return 1;
            case "AK47": return 2;
            case "riffle": return 3;
            case "knife": return 4;
            default: return -1;
        }
    }
}
