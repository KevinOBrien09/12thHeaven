using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PunchImpact : MonoBehaviour
{
    public List<AudioSource> sources = new List<AudioSource>();
    public AudioSource explode;
    void Start(){
        Enemy e = null;
        if(transform.parent.TryGetComponent<Enemy>(out e)) {
            foreach (var item in sources)
            {
                item.spatialBlend = 1;
            }
        }
    }
    public void ChangeBlend2D(){
        foreach (var item in sources)
        {
            item.spatialBlend = 0;
        }
        explode.spatialBlend = 0;
    }
    public void PlayPI(){
        AudioSource src = sources[Random.Range(0,sources.Count)];
        src.pitch = Random.Range(.9f,1.1f);
        src.Play();
    }
}