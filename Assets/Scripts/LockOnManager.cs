 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class LockOnManager:MonoBehaviour
{
    public static  LockOnManager inst;
    public Aimer currentTarget;
    bool minTime;
    void Awake()
    {
        inst = this;
    }
    

    public void LockOn(Aimer aimer){
        if(Player.inst.inBattle){
            return;
        }
        if(!BattleManager.inst.spectate && !aimer.enemy.inBattle){
        minTime = true;
        StartCoroutine(q());
        IEnumerator q(){
            yield return new WaitForSeconds(.1f);
            minTime = false;
        }
        currentTarget = aimer;
        aimer.gameObject.SetActive(true);
        CursorManager.inst.Disable();
        }
    }
    public void RemoveLockOn(){

        Debug.Log("Remove Lock On");
        if(currentTarget != null)
        {
              
            currentTarget.gameObject.SetActive(false);
            currentTarget = null;
            if(!BattleManager.inst.playerInBattle){
                CursorManager.inst.Enable();
            }
           
        }
        
    }
    


    public void Update(){

        if(!BattleManager.inst.spectate){
            if(currentTarget != null && !minTime){
            ///mouse.sqrMagnitude > .15f ||
                Vector2 mouse = new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));
                if( !currentTarget.inRange() || BattleManager.inst.playerInBattle || currentTarget.enemy.inBattle){
                    RemoveLockOn();
                }
            }
        }
       
    }

}