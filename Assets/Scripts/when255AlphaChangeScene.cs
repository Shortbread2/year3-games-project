using UnityEngine;
using UnityEngine.UI; // Add this for Image
using UnityEngine.SceneManagement; // Add this for scene management

public class when255AlphaChangeScene : MonoBehaviour
{
    public GameObject changeScene;

    void Update()
    {
        Image imageComponent = gameObject.GetComponent<Image>();

        // Check if the alpha value is approximately 1 (255 when normalized)
        if (Mathf.Approximately(imageComponent.color.a, 1f))
        {
            changeScene.SetActive(true);

            // Optionally, you can add a line to load a new scene
            // SceneManager.LoadScene("YourSceneName");
        }
    }
}
