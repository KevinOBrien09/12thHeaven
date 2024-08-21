using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class Footsteps : MonoBehaviour
{
    public AudioSource sourceA,sourceB;
    public void Play(int i){
        if(i == 0){
            sourceA.PlayOneShot(sourceA.clip,.5f);
        }
        else{
            sourceB.PlayOneShot(sourceB.clip,.5f);
        }
    }
}