using System.Collections;
using UnityEngine;

public class CutScenes : MonoBehaviour
{

    [SerializeField] GameObject Inventory;
    [SerializeField] GameObject cutScenePanel;
    [SerializeField] GameObject arrowToNextScene;
    void Start()
    {
        // Freeze time
        Time.timeScale = 0;
        Inventory.SetActive(true);

    }

    public void unfreezeTime()
    {
        Time.timeScale = 1;
        if (Inventory != null)
        {
            Inventory.GetComponent<Canvas>().sortingOrder = 2;
            Inventory.SetActive(false);
        }
        else { Inventory = null; }

        cutScenePanel.SetActive(false);
        arrowToNextScene.SetActive(false);

    }
}
