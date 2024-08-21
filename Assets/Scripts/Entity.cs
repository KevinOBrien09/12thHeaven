using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public CharColours charColour;
    public  bool canWalk;
    public Transform headBone;
    public float moveSpeed;
    public Animator animator;
    public PunchImpact pi;
    public SkinnedMeshRenderer model;
    public Health health;
    public Flesh felshPrefab;
    bool left;
    public AudioSource explode;
    public Mass mass;
    public bool inBattle;
    public virtual  void Start()
    {
        InitHealth();
        left = Random.Range(0,1) == 1;
        mass = new Mass();
        mass.owner = this;
        mass.target = 30;
    }

    public virtual void InitHealth(){
 health = new Health();
        health.max = 3;
        health.current = health.max;
    }

    public void Anim(float magnitude)
    {
        animator.SetFloat("move", magnitude,.1f, Time.deltaTime);
        
    }

    public virtual void Hit(int dmg){
       
        if(!health.dead){
            animator.Play("Hit");
        }
        else{
            Die();
        }

         if(BattleManager.inst.playerInBattle)
        {
            if(BattleManager.inst.playerRival == this){
                health.TakeDMG(dmg);
            }
        }
       
       
    }

    public virtual void Die(bool hit = true)
    {
        Debug.Log("DEAD!!!");
        StartCoroutine(q());
        IEnumerator q()
        {
            if(!hit){
                goto xd;
            }
             animator.Play("Hit");
            yield return new WaitForSeconds(1);
            xd:
            animator.SetTrigger("kneel");
            yield return new WaitForSeconds(2.3f);
            model.gameObject.SetActive(false);
         
            int r = Random.Range(5,10);
            for (int i = 0; i < r; i++)
            {
                Flesh f = Instantiate(felshPrefab,transform.position,Quaternion.identity);
                f.Spawn(charColour,owner:this);
            }
            explode.Play();

            if(this == Player.inst){
                 Fade.inst.FadeIn(()=>{
             EntityCounter.inst.   gameOver.gameObject.SetActive(true);
             CursorManager.inst.Enable();
             CursorManager.inst.followMouse = true;
                },0);
            }
            yield return new WaitForSeconds(1);
            if(EntityCounter.inst != null){
   EntityCounter.inst.RemoveEntity(this);
            }
             if(BattleManager.inst != null && Win.inst != null){
            if(BattleManager.inst.playerRival == this && !Win.inst.gameOver)
            {
                BattleManager.inst.PlayerWin();
            }
             }
            
            Destroy(gameObject);

        }
        
    }   

    public virtual void EatFlesh(Flesh flesh){
       mass.Add(5);
    }

    public virtual void NewMassLevel(){
        
        health.max+=3;
        if(this != Player.inst){
            health.current = health.max;
        }
        else{
            health.Regen(health.max/3);
            HPBar.inst.Refresh(Player.inst.health);
        }
        moveSpeed += .5f;
        float q = transform.localScale.x;
        transform.localScale = new Vector3(q+.25f,q+.25f,q+.25f);
    }

    public void Punch()
    {
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Punch") )
        {
            animator.SetBool("mirror",left);
            left = !left;
            
            animator.SetTrigger("punch");
        }
    }

}