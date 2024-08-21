using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackCollider : MonoBehaviour
{
    public static bool inside;
    public BoxCollider collider_;
    public Image image;
    public List<float> widths = new List<float>();
    public Color32 defaultState,missed,canHit;
    void Start(){
        inside = false;
    }
    public void Reset(){
        gameObject.SetActive(true);
        float x = widths[Random.Range(0,widths.Count)];
        collider_.size = new Vector3(x,collider_.size.y,collider_.size.z);
        image.rectTransform.sizeDelta = new Vector2(x+5,image.rectTransform.sizeDelta.y);
        image.color = defaultState;
    }

    public void OnTriggerEnter(Collider other){
        image.color = canHit;
        inside = true;
        Debug.Log("Enter");
    }
     public void OnTriggerExit(Collider other){
        image.color = defaultState;
        inside = false;
        Debug.Log("Enter");
    }
}