using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int BulletDamage;

    private void OnCollisionEnter(Collision ObjectWeHit)
    {

        Debug.Log("bullet hit");
        if (ObjectWeHit.gameObject.CompareTag("Wall"))
        {
            CreateBulletImpactEffect(ObjectWeHit);
        }


        if (ObjectWeHit.gameObject.CompareTag("bottle"))
        {

            SoundManager.instance.playGlassSound();
            ObjectWeHit.gameObject.GetComponent<bottle>().shatter();
            
        }


        if (ObjectWeHit.gameObject.CompareTag("zombie"))
        {
            
                ObjectWeHit.gameObject.GetComponent<zombie>().TakeDamage(BulletDamage);
            
            Destroy(gameObject);
        }
    }

    private void CreateBulletImpactEffect(Collision ObjectWeHit)
    {
        ContactPoint contact = ObjectWeHit.contacts[0];

        GameObject hole = Instantiate(

            GlobalRefrences.instance.bulletImpactEffectPreFab,
            contact.point,
            Quaternion.LookRotation(contact.normal)

            );

        hole.transform.SetParent(ObjectWeHit.gameObject.transform);
  
    }


}


