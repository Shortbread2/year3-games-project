using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatmouse : MonoBehaviour
{
  
    private Camera cam;
    private Vector3 tF;
    

    Vector3 mouseposition;

    void Start(){
        cam = Camera.main;
        tF = this.transform.up; // facing up at Y axis
    }

    void Update()
    {
        mouseposition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookDir = mouseposition - transform.position;
        float angle = Vector2.Angle(tF, lookDir);//use Vector2 to avoid Z in the calculation
        //Debug.Log(angle);
        Debug.DrawRay(transform.position, tF * 2, Color.green);
        Debug.DrawRay(transform.position, lookDir, Color.red);

        Vector3 crossP = Vector3.Cross(tF, lookDir);
        Vector3 characterScale = transform.localScale;
        int clockwise = 1;
        if (crossP.z < 0){
            clockwise = -1;
            if (transform.localScale.x < 0){
                characterScale.x = 1;
            }
        } else {
            if (transform.localScale.x > 0){
                characterScale.x = -1;
            }
        }        
        transform.localScale = characterScale;
        transform.rotation = Quaternion.Euler(0, 0, (angle + -90f) * clockwise);
    }
}
