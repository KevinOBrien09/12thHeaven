using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
public class Fade : MonoBehaviour
{
    public static Fade inst;
    public Image image;


    void Awake()
    {
         
        inst = this;
    }


    public void FadeIn(UnityAction action,float time = .5f){
         image.raycastTarget = true;
        image.DOFade(1,time).OnComplete(()=>{
            action.Invoke();
        });
       
    }

    public void FadeOut(UnityAction action,float time = .5f){
        image.DOFade(0,time).OnComplete(()=>{
            action.Invoke();
            image.raycastTarget = false;
        });
       
    }

    public void ChangeColour(Color c,float t = .2f){
        image.DOColor(c,t);
        //color = c;
    }
}
