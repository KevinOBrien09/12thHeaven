using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Countdown : MonoBehaviour
{
    public static Countdown inst;
    public float countDownRange = 30;
    public TextMeshProUGUI textMeshProUGUI;
    public string fluff;
    float timeStamp;
    float totalTime;
    public FleshRain fleshRain;
    void Awake()
    {
        inst = this;
    }

    void Start(){
        fluff = textMeshProUGUI.text;
        totalTime = 30;
    }

    // Update is called once per frame
    void Update()
    {
        
           
      
        if(Time.time >= timeStamp){

            totalTime = countDownRange;
            timeStamp += countDownRange;
            fleshRain.Rain();
            Debug.Log("Trigger");
        }
        else{
            totalTime -= Time.deltaTime;

            // Divide the time by 60
            float minutes = Mathf.FloorToInt(totalTime / 60); 
            
            // Returns the remainder
            float seconds = Mathf.FloorToInt(totalTime % 60);
            
            float miliseconds = Mathf.FloorToInt(totalTime *1000);
            //string mili = miliseconds.ToString().Substring(2);
           // mili = mili.Substring(0, mili.Length-2);
            // Set the text string
            textMeshProUGUI.text = fluff + string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
