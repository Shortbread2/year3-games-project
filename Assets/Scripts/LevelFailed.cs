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
        collectiblesDictionary = Player.GetComponent<PlayerCollection>().collectiblesDictionary;

        if (collectiblesDictionary["Gem"] >= 5)
        {

            if (MiningTimer.GetComponent<MiningTimer>().getTimeIsZero() == true)
            {
                // Timer For level 1 Ends and you dont have 10 crystals
                //TODO: go to pause scene
                Debug.Log("TODO: go to pause scene");
                //GameOverPanel.SetActive(true);
            }
        }
        else
        { //Debug.Log("TODO: not working");
        }

    }

}
