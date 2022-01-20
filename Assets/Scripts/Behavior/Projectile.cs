using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //we use this list to pick a target position, and the position is moved to give a little turbulence(See TargetTurbulence.cs).
    //if we're using any of the two other shots, we directly run some code.
    //For a shooter, we need accuracy, sure, but here, a little chaos adds a lot to the experience.
    [SerializeField]private List<Transform> targetSpots;
    [SerializeField]private Transform target;
    [SerializeField]private float speed;
    [SerializeField]private float angularSpeed;
    [SerializeField]private GameObject nukeExplosion;
    [SerializeField]private GameObject shooterExplosion;
    
    private Rigidbody rb;
    private GameObject targetParent;
    
    [HideInInspector]public VolleySpawner spawner;

    private void Awake()
    {
        //cache to prevent needing multiple calls in Update() etc.
        rb = GetComponent<Rigidbody>();
        //spawner = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<VolleySpawner>(); //todo: set this directly from the spawner
        targetParent = GameObject.FindGameObjectWithTag("TargetParent");
        //populate the list of potential targets.
        foreach (Transform child in targetParent.transform)
        {
           targetSpots.Add(child);
        }
    }
    private void OnEnable()
    {
        //pick one of n number of potential targets
        int i = Random.Range(0, targetSpots.Count - 1);
        target = targetSpots[i];
    }

    // Update is called once per frame
    void Update()
    {
        //always rotate towards the picked target.
        Vector3 targetDirection = target.position - transform.position;
        //normalise steps in frame time
        float singleStep = angularSpeed * Time.deltaTime;
        // rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        //draw a ray pointing at our target only if we're in editor
        #if UNITY_EDITOR
        Debug.DrawRay(transform.position, newDirection, Color.red);
        #endif
        //apply rotation
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void FixedUpdate()
    {
        //we set the velocity in FixedUpdate, and smoothly interp in Update().
        rb.velocity = transform.forward * speed;
    }
    private void OnCollisionEnter(Collision coll)
    {
        if(coll.transform.CompareTag("Finish"))
        {
            spawner.RepoolDeadObject(this);
        }
    }
}
