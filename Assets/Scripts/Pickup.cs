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

    public float triggerDistance = 4f;

    bool isNewsPaperActive;

    void Start()
    {
        text.SetActive(false);
    }

    void Update()
    {
        CalcDistance();
        if (Input.GetKeyDown(KeyCode.P))
        {
            text.SetActive(false);
            NewspaperUI.SetActive(true);
            isNewsPaperActive = true;

        }

        if(Input.GetKeyDown(KeyCode.Escape) && isNewsPaperActive)
        {
            isNewsPaperActive=false;
            NewspaperUI.SetActive(false);
        }
    }

    void CalcDistance()
    {
        float distance = Vector3.Distance(playerRef.transform.position, PickupRef.transform.position);

        if (distance <= triggerDistance)
        {
            if(!isNewsPaperActive) 
                ApplyGlowEffectAndText();
        }
        else
        {
            text.SetActive(false);
            glow.SetActive(false);
        }
    }

    void ApplyGlowEffectAndText()
    {
        text.SetActive(true);
        glow.SetActive(true);
        // TODO: Add glow material enable here
    }
}
