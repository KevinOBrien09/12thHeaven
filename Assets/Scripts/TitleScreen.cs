using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class TitleScreen : MonoBehaviour
{
    public Enemy enemy;
    public TextMeshProUGUI title;
    public Vector2 titleShown;
    public Vector3 enemyPos,enemyRot;
    public AudioSource music;
    public CanvasGroup options;
    public Tutorial tutorial;
    public bool lockOut;
    public AudioSource click;
   void Start(){
        Fade.inst.FadeIn((()=>
        {
            StartCoroutine(q());
            IEnumerator q()
            {
                yield return new WaitForSeconds(.1f);
                Fade.inst.FadeOut(()=>{
                enemy.transform.DOMove(enemyPos,.5f);
                enemy.transform.DORotate(new Vector3(0,215,0),1f);
                title.rectTransform.DOAnchorPos(titleShown,.5f).OnComplete(()=>{
                    music.DOFade(1,1);
                    options.DOFade(1,.2f);
                });
                },.2f);
               
                
            }


        }),0f);
        
    }
    public void Play()
    {
        if(lockOut){
            return;
        }
        lockOut = true;
          click.pitch = Random.Range(.9f,1.1f);
        click.Play();
        ConductOption(()=>
        {
            Fade.inst.FadeIn(()=>
            {
                StartCoroutine(q());
                IEnumerator q()
                {  
                    Fade.inst.FadeIn(()=>{ },1f);
                 
                    yield return new WaitForSeconds(1.5f);
                    SceneManager.LoadScene("Main");
                }
            });
        });
       
    }

    public void Tutorial(){
        tutorial.Open();
    }

    public void Quit()
    {
        if(lockOut){
            return;
        }
        lockOut = true;
          click.pitch = Random.Range(.9f,1.1f);
        click.Play();

        ConductOption(()=>
        {
            Fade.inst.FadeIn(()=>
            {
                Debug.Log("Leave Game");
                Application.Quit();

            });

        });
    }

    public void ConductOption(UnityAction a)
    {   
        music.DOFade(0,.5f);
        options.DOFade(0,.2f);
        enemy.Die(false);  
        StartCoroutine(q());
        IEnumerator q()
        {
            yield return new WaitForSeconds(2.3f);
            a.Invoke();
        }
    }
}
