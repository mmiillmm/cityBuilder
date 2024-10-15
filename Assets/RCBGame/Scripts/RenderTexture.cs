using UnityEngine;
using UnityEngine.UI;

public class DynamicRenderTexture : MonoBehaviour
{
    public Camera renderCamera;        // The camera that renders to the Render Texture
    public RawImage rawImage;          // The UI Raw Image to display the Render Texture
    public bool useFixedResolution = false; // Toggle for using a fixed resolution (640x480)

    private RenderTexture renderTexture;

    void Start()
    {
        UpdateRenderTexture();
    }

    void Update()
    {
        // Check if the screen size or resolution has changed and update the render texture accordingly
        if (!useFixedResolution && (renderTexture.width != Screen.width || renderTexture.height != Screen.height))
        {
            UpdateRenderTexture();
        }
    }

    void UpdateRenderTexture()
    {
        // Release the existing RenderTexture to free memory
        if (renderTexture != null)
        {
            renderTexture.Release();
        }

        // Choose between a fixed resolution (640x480) or screen-based resolution
        if (useFixedResolution)
        {
            renderTexture = new RenderTexture(640, 480, 24); // Fixed resolution 640x480
        }
        else
        {
            renderTexture = new RenderTexture(Screen.width, Screen.height, 24); // Dynamic resolution based on screen size
        }

        // Assign the Render Texture to the camera
        renderCamera.targetTexture = renderTexture;

        // Assign the Render Texture to the Raw Image UI component
        rawImage.texture = renderTexture;
    }
}
