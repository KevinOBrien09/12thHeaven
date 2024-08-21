using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    public int max;
    public int current;
    public bool dead;

    public void TakeDMG(int i){
        current -=i;
        if(current <= 0){
            Die();
        }
    }

    public void Regen(float regenAmount)
    {
        if(current < max)
        {
            int heal = (int) Mathf.Min(max -  current, regenAmount);
            current = current + heal;
            
        }
    }

    public void Die(){
        dead = true;
    }   
}