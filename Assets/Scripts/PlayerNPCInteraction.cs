using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class PlayerNPCInteraction : MonoBehaviour
{
    public float radius = 0.3f;
    public GameObject theUiElement;
    // kinda vague, ill change it later
    public List<GameObject> objectsToToggle = new List<GameObject>();
    public GameObject player;
    [SerializeField]
    private float timeUntilEvent = 5f;

    // if key is set to null then it avoids the key press entirely
    [SerializeField]
    private string key = "f";
    private bool playerInArea = false;
    public DialogueManager dialogueManager;

    // Update is called once per frame
    void Update()
    {
        if (player != null){
            if (Vector2.Distance(transform.position, player.transform.position) <= radius){
                playerInArea = true;
                if(theUiElement != null){theUiElement.SetActive(true);}
                Debug.Log("1");
                if (key != "null"){
                    if (Input.GetKeyDown(key)){
                        // for now its just used to de-activate and re-active stuff
                        StartCoroutine(timer(timeUntilEvent));
                    }
                } else {
                    Debug.Log("2");
                    StartCoroutine(timer(timeUntilEvent));
                    
                }
            }
            if (Vector2.Distance(transform.position, player.transform.position) > radius && playerInArea == true)
            {
                playerInArea = false;
                if(theUiElement != null){theUiElement.SetActive(false);}
            }
        }

        // for interacting with the dialogueManager
        if (dialogueManager != null && dialogueManager.isDialogueEnded)
        {
            startTimer();
            dialogueManager.isDialogueEnded = false;
        }
    }

    public void startTimer(){
        StartCoroutine(timer(timeUntilEvent));
    }

    IEnumerator timer(float time){
        yield return new WaitForSeconds(time);
        foreach (GameObject theObject in objectsToToggle){
            theObject.SetActive(!theObject.activeSelf);
        }
        if (key == "null"){
            gameObject.SetActive(false);
        }

    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(this.transform.position, new Vector3(0, 0, 1), radius);
    }
}
