using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Linq;
public class Tutorial : MonoBehaviour
{
    public int index;
    public List<GameObject> tabs = new List<GameObject>();
    public AudioSource click;
    public GameObject guy;
    public GameObject attackHowTo;
    public void Open(){
        guy.SetActive(false);
        gameObject.SetActive(true);
        index = 0;
        ChangeTab(index);
       
    }   
    public void Close(){

      
    
        guy.SetActive(true);
        gameObject.SetActive(false);
    
    }
    public void Right(){
        index++;
        index = Mathf.Clamp(index, -1,tabs.Count);
        ChangeTab(index);
    }

    public void Left(){
        index--;
        index = Mathf.Clamp(index, -1,tabs.Count);
        ChangeTab(index);
    }

    public void ChangeTab(int i){
        click.pitch = Random.Range(.9f,1.1f);
        if(i == 3 || i == 4){
            attackHowTo.SetActive(true);
        }
        else{
            attackHowTo.SetActive(false);
        }
        click.Play();
        if(tabs.ElementAtOrDefault(i)){
            foreach (var item in tabs)
            {
                item.gameObject.SetActive(false);
            }
            tabs[i].SetActive(true);
        }
        else{
            Close();
        }
    }
}
