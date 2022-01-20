using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBehavior : MonoBehaviour
{
    [SerializeField] private CameraShake screenShake;
    [SerializeField] private float explosivePower=5.0f;
    [SerializeField] private float explosionRadius = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        //screenShake = GetComponent<CameraShake>();
        //**screenShake.shake = true;
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(explosivePower, explosionPos, explosionRadius, 3.0F);

            if(hit.CompareTag("Enemy"))
            {
                hit.GetComponent<EnemyBehavior>().KillEnemy();
            }
        }
    }

    IEnumerator Timer(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
    }

    private void OnDisable()
    {
        //screenShake.shake = false;
        //screenShake.StopShake();
    }
}
