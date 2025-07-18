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
    public Material GlowMaterial;

    public float triggerDistance = 4f;
    bool isNewsPaperActive;


    [System.Serializable]
    public class PickupEntry
    {
        public string PickupName;
        public GameObject PickupPrefab;
    }
 
    public List<PickupEntry> Pickups = new List<PickupEntry>();
    private Dictionary<string , GameObject> PickDict = new Dictionary<string, GameObject>();



    void Start()
    {
        text.SetActive(false);
        
        foreach (PickupEntry entry in Pickups)
        {
            if (!PickDict.ContainsKey(entry.PickupName))
            {
                PickDict.Add(entry.PickupName, entry.PickupPrefab);
            }
        }

        foreach (var key in PickDict.Keys)
        {
            Debug.Log("pick dict");
            Debug.Log($"--{key}");
        }
    }

    void Update()
    {
        string CurrentPickup = CalcDistance();

        Debug.Log($"current pickup :  {CurrentPickup}");

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!string.IsNullOrEmpty(CurrentPickup))
            {
                text.SetActive(false);

                if (CurrentPickup == "NewsPaper")
                {
                    NewspaperUI.SetActive(true);
                    isNewsPaperActive = true;
                }
                else
                {
                    if (PickDict.ContainsKey(CurrentPickup))
                        PickDict[CurrentPickup].SetActive(false);

                    weaponManager.EquipWeapon(CurrentPickup);
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.Escape) && isNewsPaperActive)
        {
            isNewsPaperActive=false;
            NewspaperUI.SetActive(false);
        }
    }


    //Returns the name of the closest pickup
    string CalcDistance()
    {
        foreach (PickupEntry entry in Pickups)
        {
            float Distance = Vector3.Distance(playerRef.transform.position, entry.PickupPrefab.transform.position);
            if (CalcDirection(entry.PickupPrefab) > 0.7 && Distance <= triggerDistance)
            {
                if (entry.PickupName == "NewsPaper")
                {
                    if (!isNewsPaperActive)
                        ApplyGlowEffectAndText();
                    return entry.PickupName;
                }

                else
                {
                    entry.PickupPrefab.GetComponent<Renderer>().material = GlowMaterial;
                }
            }
            else
            {
                text.SetActive(false);
                glow.SetActive(false);
            }
        }

        return "";
    }

    void ApplyGlowEffectAndText()
    {
        text.SetActive(true);
        glow.SetActive(true);
        // TODO: Add glow material enable here
    }

    float CalcDirection(GameObject pickup)
    {
        Vector3 toPickup = pickup.transform.position - playerRef.transform.position;
        toPickup.Normalize(); // optional, as Vector3.Dot handles normalization internally

        float dot = Vector3.Dot(playerRef.transform.forward, toPickup);
        return dot;
    }

}
