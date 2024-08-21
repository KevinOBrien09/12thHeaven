using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscendTrigger : MonoBehaviour
{
    public Transform endPoint;
    public MeshRenderer sr;
    public Material red;
    public float spin;
    void Start(){
gameObject.SetActive(false);
    }
   public void Activate(){
    gameObject.SetActive(true);
   }

   void Update(){
    sr.transform.Rotate(new Vector3(0,spin * Time.deltaTime,0));
    sr.material = red;
   }
   public void OnTriggerEnter(Collider other)
   {
    Debug.Log(other.gameObject.name);
        if(other.gameObject.name == "Player"){
            Player.inst.EndGame(endPoint);
            //Win.inst.End();
        }
   }
}
