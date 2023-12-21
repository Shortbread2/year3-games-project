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
    public GameObject theObject;
    public GameObject player;
    [SerializeField]
    private float timeUntilEvent = 5f;
    [SerializeField]
    private string key = "f";
    private bool playerInArea = false;

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= radius){
            playerInArea = true;
            if(theUiElement != null){theUiElement.SetActive(true);}
            if (Input.GetKeyDown(key)){
                // for now its just used to de-activate and re-active stuff
                StartCoroutine(timer(timeUntilEvent));
            }
        }
        if (Vector2.Distance(transform.position, player.transform.position) > radius && playerInArea == true)
        {
            playerInArea = false;
            if(theUiElement != null){theUiElement.SetActive(false);}
        }
    }

    IEnumerator timer(float time){
        yield return new WaitForSeconds(time);
        theObject.SetActive(!theObject.activeSelf);
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(this.transform.position, new Vector3(0, 0, 1), radius);
    }
}
