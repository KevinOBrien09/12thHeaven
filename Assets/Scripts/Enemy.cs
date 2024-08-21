using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using System.Linq;

public enum CharColours{RED,BLUE,GREEN}
public class Enemy : Entity,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public NavMeshAgent agent;
    public Aimer aimer;
  public  Vector3 target;
    public GenericDictionary<CharColours,Material> dict = new GenericDictionary<CharColours, Material>();
    public EnemyFleshTrigger enemyFleshTrigger;
    public bool hasTarget;
    Flesh fleshTarget;
    public bool runningToFlesh;
    public float checkRadius,fightRadius;
   public bool notRandColour;
   
   public override void Start(){
    base.Start();
        aimer.gameObject.SetActive(false);
        if(!notRandColour){
  System.Array values =   System.Enum.GetValues(typeof(CharColours));
        System.  Random random = new   System.Random();
        charColour = (CharColours)values.GetValue(random.Next(values.Length));
        model.material = dict[charColour];
        }

      
        agent.speed = moveSpeed;
        canWalk = true;
        target = RandomNavmeshLocation(15);
        if(agent.enabled){
            agent.SetDestination(target);
        }
        StartCoroutine(CheckForFight());
    }

    public void Update()
    {
        if(canWalk)
        {
            bool inRange = Vector3.Distance(transform.position,target) < .1f;
            if(inRange || agent.velocity.magnitude <= .1f || runningToFlesh && fleshTarget == null)
            {
                int i = Random.Range(0,5);
                Vector3 v = GetTarget();
                if(v == Vector3.zero){
                    runningToFlesh = false;
                    fleshTarget = null;
                    target = RandomNavmeshLocation(15);
                }
                else{
                    target = v;
                }
              
                if(agent.enabled){
                      agent.speed = moveSpeed;
 agent.SetDestination(target);
                }
               
                Debug.Log("New pos");
            }
        
        }
        else{
            agent.speed = 0;
        }
        Anim(agent.velocity.magnitude);
      
       
    }

    IEnumerator CheckForFight(){
        yield return new WaitForSeconds(Random.Range(2,5));
        Collider[] fightColliders = Physics.OverlapSphere(transform.position, fightRadius);
        Entity e = null;
        
        foreach (var item in fightColliders)
        {
            if(item.gameObject.name == "ENEMYHERE"){
                bool nearbyEnemy = item.transform.parent.TryGetComponent<Entity>(out e) && e != this;
               
                if(nearbyEnemy && !e.inBattle && !inBattle && e.canWalk)
                {
                    if(Random.Range(0,25) == 1){
                    BattleManager.inst.Enter(this,e);
                    }
                
                }
            }
           
        }

        StartCoroutine(CheckForFight());


    }

      


    public Vector3 GetTarget(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);
      List<Collider> c = hitColliders.OrderBy((d) => (d.transform.position - transform.position).sqrMagnitude).ToList();
        Flesh f = null;
      

   
        foreach (var item in c)
        {
            bool nearbyFlesh = item.TryGetComponent<Flesh>(out f) && f.ft.canBeEaten;
            
            if(nearbyFlesh )
            {
                NavMeshHit hit;
                if (NavMesh.FindClosestEdge(item.transform.position, out hit, NavMesh.AllAreas))
                {
                    runningToFlesh = true;
                    fleshTarget = f;
                    return item.transform.position;
                }
               
            }
          
        }

       
        

        return Vector3.zero;
    }




    public Vector3 RandomNavmeshLocation(float radius) {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
            finalPosition = hit.position;            
        }
        return finalPosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Player.inst != null){
   if(!BattleManager.inst.spectate){
if(aimer.inRange() && LockOnManager.inst.currentTarget == null){
            LockOnManager.inst.LockOn(aimer);
            // aimer.gameObject.SetActive(true);
            // CursorManager.inst.Disable();
        }
        }
     
        }
        

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LockOnManager.inst.RemoveLockOn();
        //  CursorManager.inst.followMouse = true;
        // aimer.gameObject.SetActive(false);
        // CursorManager.inst.Enable();
    }
}