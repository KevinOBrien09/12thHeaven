using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleshTrigger : MonoBehaviour
{
    public float delay;
    public bool canBeEaten;
    public AudioSource source;
    public GameObject model;
    public Flesh flesh;
    IEnumerator Start(){
        yield return new WaitForSeconds(delay);
        canBeEaten = true;
    }
    public void OnTriggerEnter(Collider other){
        if(canBeEaten){
            Entity e = null;
            bool p = other.TryGetComponent<Entity>(out e);
            if(p){
                if(!e.health.dead){
                    canBeEaten = false;
                    e.EatFlesh(flesh);
                    if(e == Player.inst){
                        source.spatialBlend = 0;
                        source.pitch = Random.Range(.9f,1.1f);
                        source.Play();
                    }
                    else{
 source.pitch = Random.Range(.9f,1.1f);
                    source.Play();
                    }
                   
                   gameObject.SetActive(false);
                   model.gameObject.SetActive(false);
                 BattleManager.inst.  StartCoroutine(q());
                   IEnumerator q(){
                    yield return new WaitForSeconds(2);
                    Destroy(gameObject);
                   }
                }
       
            }
        }
        
    }
}