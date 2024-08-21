 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class Aimer:MonoBehaviour
{
    public Enemy enemy;
    public SpriteRenderer aimSprite;
    public float range,attackRange;
    public Color32 grey;
    float dist;
    public void Update(){
        if(Player.inst != null){
  dist = Vector3.Distance(Player.inst.transform.position,transform.position);
      
     
            transform.LookAt(Camera.main.transform);
            aimSprite.transform.Rotate(new Vector3(0,0,100 * Time.deltaTime));
        
            if(dist < attackRange+Player.inst.mass.level/2){
                aimSprite.color = Color.red;
            }
            else{
                aimSprite.color = grey;
            }
        }
      
       
        // var distance = (Camera.main. transform.position - transform.position).magnitude;
        // var size = distance * FixedSize * Camera.main.fieldOfView;
        // aimOffset.transform.localScale = Vector3.one * size;
        // aimOffset.transform.forward = transform.position - Camera.main.transform.position;
    }

    public bool inRange(){
        if(Player.inst != null){
        
        dist = Vector3.Distance(Player.inst.transform.position,transform.position);
        return dist < range || dist <1;
        }
        return  false;
    }

     public bool inBattleRange(){
        dist = Vector3.Distance(Player.inst.transform.position,transform.position);
        return dist < attackRange+Player.inst.mass.level/2 || dist <1;
    }
}