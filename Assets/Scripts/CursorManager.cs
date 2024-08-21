using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public static CursorManager inst;
    
    public Canvas myCanvas;
    public Image img;
    public float speed;
    public bool followMouse;
    void Awake()
    {
        inst = this;
    }


    void Start()
    {
        followMouse = true;
       Cursor.visible = false;
       Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    { 
        Cursor.visible = false;
        if(followMouse){
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
            transform.position = myCanvas.transform.TransformPoint(pos);
        }

        
        transform.Rotate(new Vector3(0,0,speed * Time.deltaTime));
    }




    public Vector3 worldToUISpace(Canvas parentCanvas, Vector3 worldPos)
    {
        //Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Vector2 movePos;

        //Convert the screenpoint to ui rectangle local point
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
        //Convert the local point to world point
        return parentCanvas.transform.TransformPoint(movePos);
    }

    public void Disable(){
        //followMouse = false;
        img.enabled = false;
    }

    public void Enable(){
        img.enabled = true;
    }
}
