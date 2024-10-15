using UnityEngine;
using UnityEngine.UI;  // Import the UnityEngine.UI namespace

public class TouchCounter : MonoBehaviour
{
    private int counter = 0;
    public Text counterText;  // Reference to your UI.Text element

    private void Start()
    {
        UpdateCounterText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GroundElement"))
        {
            counter++;
            UpdateCounterText();
            Debug.Log("Touched an element! Counter: " + counter);

            // Optionally destroy the object
            // Destroy(other.gameObject);
        }
    }

    // Updates the UI Text element with the current counter value
    private void UpdateCounterText()
    {
        counterText.text = "Counter: " + counter.ToString();
    }
}
