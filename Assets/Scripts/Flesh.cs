using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flesh : MonoBehaviour
{
    public Rigidbody rb;
    public float explosionForce;
    public FleshTrigger ft;
    public GenericDictionary<CharColours,Material> colourDict = new GenericDictionary<CharColours, Material>();
    public MeshRenderer mesh;
    public bool isWhite;
    public void Spawn(CharColours colours,bool _isWhite = false,Entity owner = null)
    {
        isWhite = _isWhite;
        mesh.material = colourDict[colours];
        float min = .05f;
        float max = .2f;
        if(owner != null){
            min = owner.transform.localScale.x/20;
            max = owner.transform.localScale.x/5;

        }
        transform.localScale = new Vector3(Random.Range(min,max),Random.Range(min,max),Random.Range(min,max));
        var rng = Random.insideUnitSphere + transform.position;
        rb.AddExplosionForce(explosionForce,rng,10);
    }
}