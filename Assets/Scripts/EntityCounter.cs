using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.SceneManagement;
public class EntityCounter : MonoBehaviour
{
    public static EntityCounter inst;
    public Enemy enemyPrefab;
    public int howMany;
    public List<Entity> entities = new List<Entity>();
    public TextMeshProUGUI textMeshProUGUI;
    public GameObject gameOver;
    void Awake()
    {
        
        inst = this;
    }

    void Start(){
        for (int i = 0; i < howMany; i++)
        {
            entities.Add(SpawnEnemy());
        }
        if(!BattleManager.inst.spectate){
    entities.Add(Player.inst);
        }
    
        textMeshProUGUI.text = entities.Count +" Remain.";
    }

    public Enemy SpawnEnemy(){
        Enemy e = Instantiate(enemyPrefab,RandomNavmeshLocation(25),Quaternion.identity);
        e.target = e.RandomNavmeshLocation(5);
        return e;
    }


    public void RemoveEntity(Entity e){
        if(entities.Contains(e)){
            entities.Remove(e);
        }
        textMeshProUGUI.text = entities.Count +" Remain.";

        // if(e == Player.inst){
           
        //     Debug.Log("Player loss");
        // }

        if(entities.Count == 1){
            if(entities[0] == Player.inst){
                Win.inst.Ascend();
                //End();
            }
        }
        //     else{
        //         Debug.Log("Player Lose");
        //     }
        // }

    } 

    public void Redo(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit(){
SceneManager.LoadScene("Title");
    }
    
    public Vector3 RandomNavmeshLocation(float radius) {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += Vector3.zero;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
            finalPosition = hit.position;            
        }
        return finalPosition;
    }




}
