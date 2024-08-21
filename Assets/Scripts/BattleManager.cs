using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;







public class BattleManager : MonoBehaviour
{
    public static BattleManager inst;
    public bool playerInBattle;
    public Entity playerRival;
    public bool spectate;
    public AudioSource enter;
    void Awake()
    {
       if(Application.platform != RuntimePlatform.WebGLPlayer) {
            Screen.SetResolution(1200,800, true);
       }
          
        inst = this;
    }

    void Start(){
        var Q = FindObjectsOfType<Flesh>();
        foreach (var item in Q)
        {
            
            Destroy(item.gameObject);
        }
    }


    public void Enter(Entity a,Entity b)
    {
        bool playerInvolved = false;
        if(!spectate){
       
        }
        playerInvolved =  a == Player.inst || b == Player.inst;;

        
        a.canWalk = false;
        b.canWalk = false;
        a.animator.Play("PunchIdle");
        b.animator.Play("PunchIdle");
        if(playerInvolved){
              enter.pitch = Random.Range(.9f,1.1f);
                enter.Play();
            if(a == Player.inst){
                playerRival = b;
            }
            else{
                playerRival = a;
            }
            
        }   
        a.inBattle = true;
        b.inBattle = true;
        float lookAtSpeed = .5f;
        var aTargetRotation = Quaternion.LookRotation(b.transform.position - a.transform.position);
        var bTargetRotation = Quaternion.LookRotation(a.transform.position - b.transform.position);
        a.transform.DORotateQuaternion(aTargetRotation,lookAtSpeed).OnComplete(()=>{


            if(playerInvolved)
            {
              
                if(a == Player.inst){
                    b.pi.ChangeBlend2D();
                }
                else{
                    a.pi.ChangeBlend2D();
                }
                Fade.inst.FadeIn(()=>{
                AttackTicker.inst.travelTime = 1;
                playerInBattle = true;
                CursorManager.inst.Disable();
                CursorManager.inst.followMouse = false;
                Player.inst.firstPersonCamera.SetUp(playerRival.headBone);
                Fade.inst.FadeOut(()=>{},.1f);

                },.1f);
            }
          
           
        });
        b.transform.DORotateQuaternion(bTargetRotation,lookAtSpeed);

        if(playerInvolved){
            CursorManager.inst.Disable();
            CursorManager.inst.followMouse = false;
        }
        else{
            FightState fs = new FightState();
            fs.a = a;
            fs.b = b;   
            fs.attacksLeft = Random.Range(1,3);
            if(a.mass.level > b.mass.level)
            {
                fs.winner = a;
                fs.loser = b;
            }
            else if(a.mass.level < b.mass.level)
            {
                fs.winner = b;
                fs.loser = a;
            
            }
            else if( a.mass.level == b.mass.level){
                bool aWins = Random.Range(0,2) == 1;
                if(aWins)
                {
                    fs.winner = a;
                    fs.loser = b;
                }
                else
                {
                    fs.winner = b;
                    fs.loser = a;
                }

            }

            StartCoroutine(Fight(fs));
        }

      
    }

    public IEnumerator Fight(FightState fs){
        yield return new WaitForSeconds(Random.Range(1,3));
        fs.attacksLeft --;

        if(fs.attacksLeft == 0){
            
            fs.loser.health.dead = true;
            fs.winner.Punch();
            yield return new WaitForSeconds(.5f);
            fs.loser.Hit(1);
            yield return new WaitForSeconds(3.75f);
            if(fs.loser != Player.inst)
            {
                fs.winner.animator.Play("Walk");
                Enemy e = fs.winner as Enemy;
                fs.winner.inBattle = false;
                fs.winner.mass.Add((int) fs.loser.mass.totalMass);
                e.target = e.GetTarget();
                fs.winner.canWalk = true;
            }
           
            
        }
        else{
            if(Random.Range(0,2) == 1){
                fs.winner.Punch();
                yield return new WaitForSeconds(.5f);
                fs.loser.Hit(0);
                yield return new WaitForSeconds(3.75f);
                 StartCoroutine(Fight(fs));
            }
            else{
                fs.loser.Punch();
                yield return new WaitForSeconds(.5f);
                fs.winner.Hit(1);
                yield return new WaitForSeconds(3.75f);
                StartCoroutine(Fight(fs));
            }
        }
    
        yield return new WaitForSeconds(.5f);
     

        yield return null;
    }


    public void PlayerWin(){
        Fade.inst.FadeIn(()=>{
            AttackTicker.inst.travelTime = 1;
   Player.inst.firstPersonCamera.Exit();
        playerInBattle = false;
        Player.inst.animator.Play("Walk");
        Player.inst.inBattle = false;
        Player.inst.canWalk = true;
        CursorManager.inst.Enable();
        CursorManager.inst.followMouse = true;
        Player.inst.mass.Add( (int)playerRival.mass.totalMass);
        MassBar.inst.Refresh(   Player.inst.mass);
        AttackTicker.inst.Reset();
            Fade.inst.FadeOut(()=>{

            });
        });
     
    }

    
}

public struct FightState
{
    public int aAttacks,bAttacks,attacksLeft;
    public Entity a,b;
    public Entity winner,loser;
}