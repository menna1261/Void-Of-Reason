using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;

    public GameObject Saiga;
    public GameObject smg;

    private GameObject currentWeapon;

    private Dictionary<string, GameObject> weaponDict = new Dictionary<string, GameObject>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        weaponDict.Add("Saiga", Saiga);
        weaponDict.Add("Smg", smg);


        // Start with no weapon
        DeactivateAllWeapons();
    }

    public void EquipWeapon(string weaponName)
    {
        if (currentWeapon != null)
            currentWeapon.SetActive(false);

        if (weaponDict.ContainsKey(weaponName))
        {
            currentWeapon = weaponDict[weaponName];
            currentWeapon.SetActive(true);

            Animator animator = currentWeapon.GetComponent<Animator>();
            if (animator != null)
            {
                int index = GetWeaponIndexByName(weaponName);
                animator.SetInteger("WeaponIndex", index);
            }

            Debug.Log($"Equipped: {weaponName}");
        }
        else
        {
            Debug.LogWarning($"Weapon not found: {weaponName}");
        }
    }

    private int GetWeaponIndexByName(string name)
    {
        switch (name)
        {
            case "Knife": return 0;
            case "Saiga": return 1;
            case "Pistol": return 2;
            case "Riffle": return 3;
            case "smg": return 4;
            default: return -1;
        }
    }

    private void DeactivateAllWeapons()
    {
        foreach (var weapon in weaponDict.Values)
        {
            weapon.SetActive(false);
        }
    }

}
