using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Player : Entity
{
    public static Player inst;
    [SerializeField] CharacterController controller;
    public AudioSource music,rise;
    public Camera camera_;
    public FirstPersonCamera firstPersonCamera;
    float smoothBlend = .2f;
    float tref;
    public AudioSource levelUp;
    
    void Awake()
    {
        inst = this;
    }

    public override void Start(){
    base.Start();
      //  canWalk = true;
        MassBar.inst.Refresh(mass);
        HPBar.inst.Refresh(health);
       
    }

 
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)&& !animator.GetCurrentAnimatorStateInfo(0).IsName("Punch") && !BattleManager.inst.playerInBattle)
        {
            if(LockOnManager.inst.currentTarget != null)
            {
                if(LockOnManager.inst.currentTarget.enemy != null && !BattleManager.inst.playerInBattle && LockOnManager.inst.currentTarget.inBattleRange())
                {
                    BattleManager.inst.Enter(this,LockOnManager.inst.currentTarget.enemy );
                }
            }
        }
        Vector2 input = new Vector2();
        if(canWalk && !animator.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y =  Input.GetAxisRaw("Vertical");
            Walk(input);
          
        }
        
        Anim(input.magnitude);
    }

    public override void Hit(int dmg)
    {
        if(!health.dead)
        {
            animator.Play("Hit");
        }
        else
        {
            Die();
        }
        health.TakeDMG(dmg);
        HPBar.inst.Refresh(health);
    }

    public void Walk(Vector2 input)
    {  
        Vector3 dir = new Vector3(input.x,0,input.y).normalized;
        if(dir.magnitude >= .1f){
            float target = Mathf.Atan2(dir.x,dir.z) * Mathf.Rad2Deg + camera_.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,target,ref tref,.1f);
            transform.rotation = Quaternion.Euler(0,angle,0);
            Vector3 moveDir = Quaternion.Euler(0,target,0) * Vector3.forward;
            controller.Move(moveDir.normalized*moveSpeed*Time.deltaTime);
            controller.Move(new Vector3(0,-14,0));
            transform.position = new Vector3(transform.position.x,0,transform.position.z);
        }
    }

    public void EndGame(Transform endPoint)
    {
        canWalk = false;
        animator.Play("Rise");
        music.DOFade(0,1).OnComplete(()=>
        {
            rise.Play();
            CameraFollow.inst.enabled = false;
            transform.DOMove(endPoint.position,8);
            StartCoroutine(q());
            IEnumerator q()
            {
                yield return new WaitForSeconds(3);
                Win.inst.End();
            }
        });
    }

    public override void EatFlesh(Flesh flesh)
    {
        base.EatFlesh(flesh);
        if(flesh.isWhite){
            health.Regen(1);
            HPBar.inst.Refresh(health);
        }
        MassBar.inst.Refresh(mass);
    }

    public override void InitHealth()
    {
        health = new Health();
        health.max = 5;
        health.current = health.max;
    }

    public override void NewMassLevel(){
      base.NewMassLevel();
       MassBar.inst.Refresh(mass); 
       HPBar.inst.Refresh(health);
       levelUp.Play();
    }


   
}
