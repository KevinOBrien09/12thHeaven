using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;
using System.Linq;
public class FluffMessage : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
   public List<string> messages = new List<string>();
    public Vector2 messageRange;
    float timeStamp;
    bool countDown;
    Queue<string> qu = new Queue<string>();
    void Start(){
        textMeshProUGUI.DOFade(0,0);
        timeStamp = Random.Range(messageRange.x,messageRange.y) + 5;
        countDown = true;
    }

    public void Reset(){
       System.Random rng = new  System.Random();

        var shuffled = messages.OrderBy(_ => rng.Next()).ToList();
        foreach (var item in shuffled)
        {
            qu.Enqueue(item);
        }
    }

   public void Update(){
        if(Time.time >= timeStamp && countDown){
            
            countDown = false;
            Show();
        }
   }

    public void Show(){
        if(qu.Count <= 0){
            Reset();
        }
        
        textMeshProUGUI.text = qu.Dequeue();
        textMeshProUGUI.DOFade(1,.5f).OnComplete(()=>{

        StartCoroutine(q());
        });
       
        IEnumerator q(){
          
            yield return new WaitForSeconds(3.5f);
            textMeshProUGUI.DOFade(0,.5f).OnComplete(()=>
            {              timeStamp = Time.time + Random.Range(messageRange.x,messageRange.y);
                countDown = true;
            });
        }
   }
}
