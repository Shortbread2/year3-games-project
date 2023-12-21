using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFailed : MonoBehaviour
{
    public GameObject MiningTimer;
    private Dictionary<string, int> collectiblesDictionary = new Dictionary<string, int>();

    public GameObject Player;

    public GameObject GameOverPanel;

    void Update()
    {
        if (Player != null)
        {
            collectiblesDictionary = Player.GetComponent<PlayerCollection>().collectiblesDictionary;

            if (collectiblesDictionary["Gem"] >= 5)
            {
                // Stop the timer
                MiningTimer.GetComponent<MiningTimer>().StopTimer();
            }

            if (MiningTimer.GetComponent<MiningTimer>().getTimeIsZero())
            {
                GameOverPanel.SetActive(true);
            }
        }
    }
}
