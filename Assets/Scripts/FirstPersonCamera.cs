using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform target,posT;    
    public Camera camera_;
    public bool shit;
    void Start(){
        enabled = false;
    }

    public void SetUp(Transform _target){
        target = _target;
        camera_.depth = 99;
        enabled = true;
    }

    public void Exit(){
        target = null;
        camera_.depth = -5;
        enabled = false;
    }

    void LateUpdate(){
        transform.position = posT.position;
        if(!shit){
if(target != null){
        transform.LookAt(target);
       transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y,0);
        }
        else{
Debug.LogAssertion("TARGET IS NULL!!!");
        }
        }
        
     
    }
}