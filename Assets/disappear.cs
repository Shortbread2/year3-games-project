using UnityEngine;

public class dissappear : MonoBehaviour
{
    private float startTime;
    private Renderer renderer;

    public float duration = 15f; // Time in seconds for the fade

    void Start()
    {
        startTime = Time.time;
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Calculate the time elapsed since the script started
        float elapsedTime = Time.time - startTime;

        // Calculate the transparency based on the elapsed time
        float transparency = 1.0f - Mathf.Clamp01(elapsedTime / duration);

        // Apply the transparency to the material of the game object
        SetMaterialTransparency(transparency);

        // If the duration has passed, you can perform additional actions or disable the script
        if (elapsedTime >= duration)
        {
            // Additional actions (if needed)
            // For example, destroy the game object or disable the script
            // gameObject.SetActive(false);
            // Destroy(gameObject);
            enabled = false; // Disable the script
        }
    }

    void SetMaterialTransparency(float alpha)
    {
        // Assuming the game object has a renderer component with a material supporting transparency
        if (renderer != null)
        {
            foreach (Material material in renderer.materials)
            {
                Color color = material.color;
                color.a = alpha;
                material.color = color;
            }
        }
    }
}
