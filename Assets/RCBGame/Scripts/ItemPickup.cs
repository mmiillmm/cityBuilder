using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectCounter : MonoBehaviour
{
    public string objectTag = "Wood"; // Set the tag of the objects to count
    public TMP_Text countText; // Reference to the UI Text element
    private int objectCount = 0;

    void Start()
    {
        UpdateCountText();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the specified tag
        if (gameObject.CompareTag(objectTag))
        {
            objectCount++;
            UpdateCountText();

            // Optionally, destroy the object after counting
            Destroy(gameObject);
        }
    }

    private void UpdateCountText()
    {
        countText.text = "Count: " + objectCount.ToString();
    }
}
