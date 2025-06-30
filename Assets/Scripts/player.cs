using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{

    public int HP = 100;

    public GameObject bloodyScreen;
    public GameObject GameOverScreen;

    public void TakeDamage(int damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            print("player Dead");
            playerDead();
        }

        else
        {
            print("player hit");
            StartCoroutine(bloodyScreenEffect());
        }

    }

    private void playerDead()
    {
        GetComponent<MouseMovement>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        if (GameOverScreen.activeInHierarchy == false)
        {
            GameOverScreen.SetActive(true);
        }

    }

    private IEnumerator bloodyScreenEffect()
    {
        if(bloodyScreen.activeInHierarchy == false)
        {
            bloodyScreen.SetActive(true);
        }

        var image = bloodyScreen.GetComponentInChildren<Image>();

        // Set the initial alpha value to 1 (fully visible).
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the new alpha value using Lerp.
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            // Update the color with the new alpha value.
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            // Increment the elapsed time.
            elapsedTime += Time.deltaTime;

            yield return null; ; // Wait for the next frame.
        }

        if (bloodyScreen.activeInHierarchy)
        {
            bloodyScreen.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("zombieHand"))
        {
            Debug.Log("Player hit");
            TakeDamage(25);
        }
    }



}
