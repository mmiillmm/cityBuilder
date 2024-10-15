using UnityEngine;

public class TriggerZoneText : MonoBehaviour
{
    public GameObject textObject; // Szöveg objektum, amit aktiválunk vagy deaktiválunk

    private void Start()
    {
        // Az indításkor a szöveg objektumot inaktívvá tesszük
        textObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ha a trigger zónába belépő objektum a "Player" címkével rendelkezik
        if (other.CompareTag("Player"))
        {
            // Aktiváljuk a szöveg objektumot
            textObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Ha a trigger zónából kilépő objektum a "Player" címkével rendelkezik
        if (other.CompareTag("Player"))
        {
            // Deaktiváljuk a szöveg objektumot
            textObject.SetActive(false);
        }
    }
}
