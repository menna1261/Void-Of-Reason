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

    public float triggerDistance = 1f;
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
            Debug.Log($"--{key}");
        }
    }

    void Update()
    {
       string CurrentPickup =  CalcDistance();
        if (Input.GetKeyDown(KeyCode.P))
        {
            text.SetActive(false);

            if(CurrentPickup == "NewsPaper")
            {
                NewspaperUI.SetActive(true);
                isNewsPaperActive = true;

            }
            else
            {
                PickDict[CurrentPickup].SetActive(false);
                weaponManager.EquipWeapon(CurrentPickup);


            }


        }

        if(Input.GetKeyDown(KeyCode.Escape) && isNewsPaperActive)
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
            if (Distance <= triggerDistance)
            {
                if (!isNewsPaperActive)
                    ApplyGlowEffectAndText();
                return entry.PickupName;
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
}
