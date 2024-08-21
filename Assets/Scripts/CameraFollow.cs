 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow inst;

    public Transform target;
    public Transform big;
    public float smoothspeed = 0.125f;
    public Vector3 offset;
    public int spectateIndex;
     void Awake()
    {
        inst = this;
    }
    
    void Start(){
        offset = transform.position;
        if(BattleManager.inst.spectate){
            spectateIndex = 0;
            NewSpectateTarget();
        }
    }

    public void NewSpectateTarget(){
            target = EntityCounter.inst.entities[spectateIndex].transform;
    }
    void Update(){
      
        if(target != null)
        {
            Vector3 desiredposition = target.position + offset;
            Vector3 smoothedposition = Vector3.Lerp(transform.position, desiredposition, smoothspeed*Time.deltaTime);
            transform.position = smoothedposition;
        }
        if(BattleManager.inst.spectate){

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (spectateIndex >= EntityCounter.inst.entities.Count - 1)
                    spectateIndex = 0;
                else
                    spectateIndex++;

                 NewSpectateTarget();
                    
            }
            if(Input.GetKeyDown(KeyCode.A))
            {
                if (spectateIndex <= 0)
                    spectateIndex = EntityCounter.inst.entities.Count - 1;
                else
                    spectateIndex--;

                NewSpectateTarget();
            }

        }
        else{
            if(Player.inst != null){
 if(Player.inst.transform.localScale.x > 3){
            target = big;
        }
        else{
            target = Player.inst.transform;
        }
            }
 
        }
        


     
    }
 

}