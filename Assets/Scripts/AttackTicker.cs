using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class AttackTicker : MonoBehaviour
{
    public static AttackTicker inst;

    public Vector2 ranges;
    public RectTransform ticker;
    public Image tickIMG;
    public float travelTime;
    public AudioSource hitsfx,failSfx;
    int bounceCount;
    bool doingSomething;
    public List<AttackCollider> colliders = new List<AttackCollider>(); 
    public Color32 defaultColour;
     public GenericDictionary<CharColours,Color32> dict = new GenericDictionary<CharColours, Color32>();
    public bool enemyAttacks;
    public float speed;
    public AudioSource src;
    void Awake()
    {
        
        inst = this;
    }



    public void Start(){
        defaultColour = tickIMG.color;
        bounceCount = 1;
        foreach (var item in colliders)
        {
            item.gameObject.SetActive(false);
        }
        Bounce(false);
         SetNewCollider();
    }

    public void Update()
    {
        if(BattleManager.inst.playerInBattle)
        {
            if(AttackCollider.inside && !doingSomething && enemyAttacks)
            {
                if(Random.Range(0,100) <= 2)
                {
                    
                }
            }

            if(Input.GetMouseButtonDown(0)&& !doingSomething|| Input.GetKeyDown(KeyCode.Space)&& !doingSomething )
            {   
                if(AttackCollider.inside ){
                    TriggerHit(Player.inst,BattleManager.inst.playerRival);
                     hitsfx.pitch = Random.Range(.9f,1.1f);
        hitsfx.Play();
                    travelTime -= .1f;
                    travelTime = Mathf.Clamp(travelTime,.2f,1f);
                }
                else
                {
                   TriggerHit(BattleManager.inst.playerRival,Player.inst);
                   failSfx.pitch = Random.Range(.9f,1.1f);
      failSfx.Play();
                   travelTime = 1;
                }
            }
        }
    }

    public void TriggerHit(Entity attacker,Entity target){
        tickIMG.color = dict[ attacker.charColour];
        doingSomething = true;
        attacker.Punch();
        Pause();
       
        foreach (var item in colliders)
        {
            item.gameObject.SetActive(false);
        }
        StartCoroutine(q());
        IEnumerator q(){
            yield return new WaitForSeconds(.5f);
            target.Hit(attacker.mass.level+1);
            yield return new WaitForSeconds(.5f);
            if(!target.health.dead){
            Reset();
            }
            else{
                target.Die();
            }
          
          
            
        }
    }

    public void Reset(){
        Pause();
    
        doingSomething = false;
        
        AttackCollider.inside = false;
        tickIMG.color = defaultColour;
    }


    public void Pause()
    {
        ticker.DOTogglePause();
        //DOKill();
    }

    public void Bounce(bool atEnd){
        float x = 0;
        if(atEnd){
            x = ranges.y;
        }
        else{
            x = ranges.x;
        }
        ticker.DOAnchorPosX(x,travelTime).OnComplete(()=>{
            // src.pitch = Random.Range(.7f,8f);
            // src.Play();
            bounceCount++;
            if(bounceCount %2 ==0){
                SetNewCollider();
            }
           
            Bounce(!atEnd);
        });
    }

    void SetNewCollider(){
        if(BattleManager.inst.playerInBattle){
foreach (var item in colliders)
        {
            item.gameObject.SetActive(false);
        }

        colliders[Random.Range(0,colliders.Count)].Reset();
        }
        
      
    }
}
