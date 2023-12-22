using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aggravateEntitiesOnTrigger : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> entities = new List<GameObject>();
    private GameObject Player;
    public bool agroWithNoCondition = false;
    public GameObject activateTimerView;
    public GameObject activateTimer;
    public GameObject countDownTimer;
    private Dictionary<string, int> collectiblesDictionary = new Dictionary<string, int>();

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        agroEntities(other);
    }
    // sometimes OnTriggerEnter2D doesnt work so yea i put in OnTriggerStay2D
    void OnTriggerStay2D(Collider2D other)
    {
        agroEntities(other);
    }
    public void agroEntities(Collider2D other){
        if (other.gameObject != null && other.gameObject.tag == "Player")
        {
            Player = other.gameObject;

            collectiblesDictionary = Player.GetComponent<PlayerCollection>().collectiblesDictionary;

            if (collectiblesDictionary["IDCard"] < 1 || agroWithNoCondition)
            {
                foreach (GameObject entity in entities){
                    if (entity.tag == "NPC"){
                        entity.GetComponent<NPC>().TakeDamage(0,Player); // agros the guards to the player
                        entity.GetComponent<AIBehaviour>().SeenTarget = true;
                    }
                    if (entity.tag == "Enemy"){
                        // TODO - agro enemy
                    }
                }
            }
            if (activateTimer != null){
                    activateTimer.SetActive(true);
                    countDownTimer.SetActive(true);
                    activateTimerView.SetActive(true);
                }
        }
    }
}
