using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class HPBar : MonoBehaviour
{
    public static HPBar inst;
    public Image fill;
    void Awake()
    {
        inst = this;
    }

   

    public void Refresh(Health hp){
        fill.DOFillAmount((float)hp.current/(float) hp.max,.2f);
    }
    
}