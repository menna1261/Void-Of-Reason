using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeFollow : MonoBehaviour
{
    public Transform weaponAttachmentPoint; // assign the weapon's scope mount point here

    void LateUpdate()
    {
        if (weaponAttachmentPoint != null)
        {
            transform.position = weaponAttachmentPoint.position;
            transform.rotation = weaponAttachmentPoint.rotation;
        }
    }
}

