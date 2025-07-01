using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalRefrences : MonoBehaviour
{
    public static GlobalRefrences instance {  get;  set; }
    public GameObject bulletImpactEffectPreFab;

    public bool isWalking;
    public bool isRunning;
    public bool isMoving;

   
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
