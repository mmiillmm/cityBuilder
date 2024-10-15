using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private bool playerInTrigger = false; // Játékos a trigger zónában
    private string triggerTag = ""; // Trigger zóna címke

    void Update()
    {
        // Ha a játékos a trigger zónában van és az 'E' billentyűt lenyomtuk
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            // Ha a trigger címke 'enterHome', töltsük be a 1-es számú jelenetet
            if (triggerTag == "enterHome")
            {
                SceneManager.LoadScene(1);
            }
            // Ha a trigger címke 'exitHome', töltsük be a 0-ás számú jelenetet
            else if (triggerTag == "exitHome")
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ha a trigger címke 'enterHome' vagy 'exitHome', állítsuk be a játékos a trigger zónában állapotot
        if (other.CompareTag("enterHome") || other.CompareTag("exitHome"))
        {
            playerInTrigger = true;
            triggerTag = other.tag;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Ha a játékos elhagyja a trigger zónát, állítsuk vissza az állapotokat
        if (other.CompareTag("enterHome") || other.CompareTag("exitHome"))
        {
            playerInTrigger = false;
            triggerTag = "";
        }
    }
}
