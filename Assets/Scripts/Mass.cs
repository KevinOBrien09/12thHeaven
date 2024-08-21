using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mass
{
    public float current,target,totalMass;
    public int level;
    public Entity owner;
    public void Add(int newExp)
    {
        current += newExp;
        totalMass += newExp;
        while(current >= target)
        {
            current = current - target;
          
            level++;
            target = target + 10;
            owner.NewMassLevel();
         
        }
    }
}