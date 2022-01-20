using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXKiller : MonoBehaviour
{
    [HideInInspector] public VolleySpawner spawner;
    private ParticleSystem effect;
    

    private void Awake()
    {
        effect = GetComponent<ParticleSystem>();        
    }

    private void LateUpdate()
    {
        if(effect.isStopped)
        {
            spawner.RepoolDeadVFX(gameObject);
            //Debug.Log("Here");
        }
    }
}
