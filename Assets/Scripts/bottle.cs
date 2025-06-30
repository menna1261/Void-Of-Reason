using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottle : MonoBehaviour
{
    public List<Rigidbody> allParts = new List<Rigidbody>();
   

    public void shatter()
    {
        foreach (var part in allParts)
        {
            part.isKinematic = false;
        }
    }


}


