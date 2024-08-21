using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FleshRain : MonoBehaviour
{
    public Flesh fleshPrefab;
    public Transform[] spawnPoints;
    public Material white;
    public void Rain()
    {
        Transform t = spawnPoints[Random.Range(0,spawnPoints.Length)] ;
        for (int i = 0; i <  Random.Range(5,15); i++)
        {
            Flesh f = Instantiate(fleshPrefab,t.position,Quaternion.identity);
            f.Spawn(CharColours.RED,true);
            f.mesh.material = white;
        }

        
               

    }
}