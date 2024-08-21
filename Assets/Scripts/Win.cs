using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
public class Win : MonoBehaviour
{    
    public bool gameOver;
    public static Win inst;
    public CanvasGroup cg;
    public TextMeshProUGUI ascendText,winText;
    public List<string> txt = new List<string>();
    Queue<string> qu = new Queue<string>();
    public AscendTrigger ascendTrigger;
    public AudioSource boom,music;
    public Color32 red,black;
    void Awake()
    {
      
        inst = this;
        cg.DOFade(0,0);
        winText.DOFade(0,0);
        music.DOFade(0,0);

        foreach (var item in txt)
        {
            qu.Enqueue(item);
        }
      
    }


    void Start()
    {
        Fade.inst.FadeIn(()=>
        {
            StartCoroutine(q());
            IEnumerator q(){
           // yield return new WaitForSeconds(1);
            winText.text = "The Ritual begins.";
               cg.DOFade(1,0);
            winText.DOFade(1,1);
          
            yield return new WaitForSeconds(2);
            
            cg.DOFade(0,2);
            winText.DOFade(0,2);
            yield return new WaitForSeconds(1.5f);
            music.DOFade(.4f,1);
            Fade.inst.FadeOut(()=>{
               
                Player.inst.canWalk = true;
            },1);
              //gameObject.SetActive(false);
            }
           

        },0);
    }

    public void Ascend()
    {
        ascendText.gameObject.SetActive(true);
        Countdown.inst.gameObject.SetActive(false);
        EntityCounter.inst.textMeshProUGUI.text = "The platform awaits you.";
        Shake();
        ascendTrigger.Activate();
    
    }

    public void Shake()
    {
        ascendText.rectTransform.DOShakePosition(60,10).OnComplete(()=>{
        Shake();
        });
    }

    public void End()
    {
        gameOver = true;
        Player.inst.canWalk = false;
        gameObject.SetActive(true);
       
        Fade.inst.FadeIn(()=>
        {
            cg.DOFade(1,1).OnComplete(()=>
            { StartCoroutine(ChangeText(qu.Dequeue())); });
        });

    }


    IEnumerator ChangeText(string text,bool q = false){
       
        winText.DOFade(0,1).OnComplete(()=>
        {
            winText.text = text;
            winText.DOFade(1,1).OnComplete(()=>
            {
                if(qu.Count > 0){
                    StartCoroutine(q());
                    IEnumerator q()
                    {
                        yield return new WaitForSeconds(2);
                        StartCoroutine(ChangeText(qu.Dequeue()));
                    }
                }
                else if(q == false)
                {
                  //  explode.Die(false);
                    boom.Play();
                    Fade.inst.ChangeColour(red);
                    StartCoroutine(q());
                    IEnumerator q(){
                        yield return new WaitForSeconds(1);
                        
                        Fade.inst.ChangeColour(black,2);
                        yield return new WaitForSeconds(2.5f);
                        SceneManager.LoadScene("Title");

                    }
                    Debug.Log("Main Menu");
                }
              
            });
            
        });

           yield return new WaitForSeconds(2);
    }

   
}
