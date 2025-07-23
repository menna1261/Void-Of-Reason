using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public GameObject playerRef;
    public GameObject PickupRef;
    public GameObject NewspaperUI;
    public GameObject text;
    public GameObject glow;
    public WeaponManager weaponManager;
    public GameObject GlowMaterialSaiga;
    public GameObject GlowMaterialSmg;
    public GameObject GlowMaterialAK;


    public float triggerDistance = 6f;
    bool isNewsPaperActive;

    [System.Serializable]
    public class PickupEntry
    {
        public string PickupName;
        public GameObject PickupPrefab;
    }

    public List<PickupEntry> Pickups = new List<PickupEntry>();
    private Dictionary<string, GameObject> PickDict = new Dictionary<string, GameObject>();

    void Start()
    {
        text.SetActive(false);
        glow.SetActive(false);
        GlowMaterialSaiga.SetActive(false);
        GlowMaterialSmg.SetActive(false);
        GlowMaterialAK.SetActive(false);

        foreach (PickupEntry entry in Pickups)
        {
            if (!PickDict.ContainsKey(entry.PickupName))
            {
                PickDict.Add(entry.PickupName, entry.PickupPrefab);
            }
        }
    }

    void Update()
    {
        string CurrentPickup = isNewsPaperActive ? "" : CalcDistance();

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!string.IsNullOrEmpty(CurrentPickup))
            {
                text.SetActive(false);

                if (CurrentPickup == "NewsPaper")
                {
                    text.SetActive(false);
                    NewspaperUI.SetActive(true);
                    isNewsPaperActive = true;
                    Time.timeScale = 0f;
                }
                else
                {
                    if (PickDict.ContainsKey(CurrentPickup))
                    {
                        // Hide glow
                        if (CurrentPickup == "Saiga")
                            GlowMaterialSaiga.SetActive(false);
                        else if (CurrentPickup == "smg")
                            GlowMaterialSmg.SetActive(false);
                        else if(CurrentPickup == "AK47")
                            GlowMaterialAK.SetActive(false);
                        // Disable the actual weapon pickup after pressing P
                        PickDict[CurrentPickup].SetActive(false);

                        // Equip the weapon
                        weaponManager.EquipWeapon(CurrentPickup);
                        text.SetActive(false);

                        Destroy(PickDict[CurrentPickup]);
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isNewsPaperActive)
        {
            isNewsPaperActive = false;
            NewspaperUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    // Returns the name of the closest valid pickup
    string CalcDistance()
    {
        foreach (PickupEntry entry in Pickups)
        {
            GameObject pickup = entry.PickupPrefab;

            if (!pickup.activeSelf)
                continue; // skip if already picked up

            float Distance = Vector3.Distance(playerRef.transform.position, pickup.transform.position);
            Debug.Log($"Distance : {Distance}");
            if (CalcDirection(pickup) > 0.4f && Distance <= triggerDistance)
            {
                text.SetActive(true);

                if (entry.PickupName == "NewsPaper")
                {
                    if (!isNewsPaperActive)
                        ApplyGlowEffectAndText();
                    return entry.PickupName;
                }
                else
                {
                    if (entry.PickupName == "Saiga")
                        GlowMaterialSaiga.SetActive(true);
                    else if (entry.PickupName == "smg")
                        GlowMaterialSmg.SetActive(true);
                    else if (entry.PickupName == "AK47") 
                        GlowMaterialAK.SetActive(true);
                    text.SetActive(true);
                    return entry.PickupName;
                }
            }
            else
            {
                // Out of range or not facing
                text.SetActive(false);
                glow.SetActive(false);

                if (entry.PickupName == "Saiga")
                    GlowMaterialSaiga.SetActive(false);
                else if (entry.PickupName == "smg")
                    GlowMaterialSmg.SetActive(false);
                else if(entry.PickupName == "AK47")
                    GlowMaterialAK.SetActive(false);
            }
        }

        return "";
    }

    void ApplyGlowEffectAndText()
    {
        text.SetActive(true);
        glow.SetActive(true);
    }

    float CalcDirection(GameObject pickup)
    {
        Vector3 toPickup = pickup.transform.position - playerRef.transform.position;
        toPickup.Normalize();

        return Vector3.Dot(playerRef.transform.forward, toPickup);
    }
}
