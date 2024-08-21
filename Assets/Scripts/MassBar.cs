using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class MassBar : MonoBehaviour
{
    public static MassBar inst;
    public Image fill;
    void Awake()
    {
        inst = this;
    }

   

    public void Refresh(Mass mass){
        fill.DOFillAmount((float)mass.current/(float) mass.target,.2f);
    }
    
}