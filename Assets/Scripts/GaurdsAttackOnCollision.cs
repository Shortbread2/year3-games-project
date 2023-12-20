using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaurdsAttackOnCollision : MonoBehaviour
{
    public GameObject guardAttack1;
    public GameObject guardAttack2;
    public GameObject Player;
    public GameObject activateTimerView;
    public GameObject activateTimer;
    public GameObject countDownTimer;
    private Dictionary<string, int> collectiblesDictionary = new Dictionary<string, int>();

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject != null && other.gameObject == Player)
        {

            collectiblesDictionary = Player.GetComponent<PlayerCollection>().collectiblesDictionary;

            if (collectiblesDictionary["IDCard"] < 1)
            {
                guardAttack1.GetComponent<NPC>().TakeDamage(0, Player);
                guardAttack1.GetComponent<AIBehaviour>().aiPathfinder.canMove = true;
                guardAttack1.GetComponent<AIBehaviour>().SeenTarget = true;

                guardAttack2.GetComponent<NPC>().TakeDamage(0, Player);
                guardAttack2.GetComponent<AIBehaviour>().aiPathfinder.canMove = true;
                guardAttack2.GetComponent<AIBehaviour>().SeenTarget = true;
                activateTimer.SetActive(true);
                countDownTimer.SetActive(true);
                activateTimerView.SetActive(true);
            }


        }



    }
}
