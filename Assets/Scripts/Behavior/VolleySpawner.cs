using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class VolleySpawner : MonoBehaviour
{
    //this one spawns projectiles in succession and handles pooling. The actual movement driver code is a separate monobehavior on the projectiles.
    private bool isSpawningVolley;
    [SerializeField] private Button volleyButton;
    [SerializeField] private Transform[] volleyTransform;
    [SerializeField] private Projectile volleyProjectile;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float volleyDelay;
    [SerializeField] private CameraShake screenShake;
    
    private Animator anim;
    private int volleyIndex = 0;
    private int turretIndex = 0;
    private ObjectPool<Projectile> projectilePool;
    private ObjectPool<GameObject> explosionPool;

    public int volleyCount;

    private void Start()
    {
        anim = GetComponent<Animator>();
        screenShake = GetComponent<CameraShake>(); //there's probably a better way for implementing screenshake

        //boilerplate for creating the pool
        projectilePool = new ObjectPool<Projectile>
            (
                () => { return Instantiate(volleyProjectile); },
                volleyProjectile => { volleyProjectile.gameObject.SetActive(true); },
                volleyProjectile => { volleyProjectile.gameObject.SetActive(false); },
                volleyProjectile => { Destroy(volleyProjectile); },
                true, 16, 1500
            );
        explosionPool = new ObjectPool<GameObject>
            (
                () => { return Instantiate(explosionEffect); },
                explosionEffect => { explosionEffect.SetActive(true); },
                explosionEffect => { explosionEffect.SetActive(false); },
                explosionEffect => { Destroy(explosionEffect); },
                true, 16, 1500
            );
    }

    public void SpawnProj()
    {
        //start the spawn cycle if and only if we're not already spawning.
        if (!isSpawningVolley)
        {
            //disable inputs till we are done with the volley.
            volleyButton.interactable = false;
            isSpawningVolley = true;
            screenShake.shake = true;
            if(anim != null)
            {
                anim.SetBool("Shoot", true);
            }
            SpawnProj();
        }
        else
        {
            //there's probably a better way to have this repeater logic, but for the intended effect, this is what works for now
            //how many times do we want to shoot a volley of missles?
            if (volleyIndex < volleyCount)
            {
                //loop through the whole list of transforms till volley is complete
                if (turretIndex < volleyTransform.Length)
                {
                    //spawn from pool and instantiate position, then pass a delay.
                    var projectile = projectilePool.Get();
                    projectile.spawner = this;
                    projectile.transform.position = volleyTransform[turretIndex].position;
                    StartCoroutine(VolleyDelayTimer());
                    turretIndex++;
                }
                else
                {
                    //increment and clear this loop, then recurse.
                    volleyIndex++;
                    turretIndex = 0;
                    StartCoroutine(VolleyDelayTimer());
                }
                    
            }
            //reset everything if we're done
            else
            {
                volleyButton.interactable = true;
                turretIndex = 0;
                volleyIndex = 0;
                isSpawningVolley=false;
                screenShake.shake = false;
                screenShake.StopShake();
                if (anim != null)
                {
                    anim.SetBool("Shoot", false);
                }
            }
        }
    }

    private IEnumerator VolleyDelayTimer()
    {
        yield return new WaitForSeconds(volleyDelay + Random.Range(0.0f, 0.15f));
        SpawnProj();
    }

    public void RepoolDeadObject(Projectile instance)
    {
        //pool-spawn an explosion where we return the missle to the pool.
        var explosion = explosionPool.Get();
        explosion.GetComponent<VFXKiller>().spawner = this;
        explosion.transform.position = instance.transform.position;
        //reinit the particle system
        explosion.GetComponent<ParticleSystem>().Play();
        
        //return projectile to the pool.
        projectilePool.Release(instance);
        
    }

    public void RepoolDeadVFX(GameObject vfx)
    {
        //return the explosion to the pool when requested
        explosionPool.Release(vfx);
    }
}
