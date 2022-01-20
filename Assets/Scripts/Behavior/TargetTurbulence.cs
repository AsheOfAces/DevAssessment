using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTurbulence : MonoBehaviour
{
    //an interesting way to add a little chaos to the missle volley shots.
    //This acts as a continuously moving target for the missles, giving them the illusion
    //of air resistance and some chaotic movement.
    [SerializeField] private float turbulenceRadius = 5f;
    [SerializeField] private float turbulenceSpeed = 20f;
    private Vector3 targetPosition;
    private Vector3 spawnPosition;

    private void Start()
    {
        //init
        spawnPosition = transform.position;
        ComputeNewTurbulentPosition();
    }

    void Update()
    {
        //lerp only if we haven't reached close enough to the target.
        if(Vector3.Distance(transform.position, targetPosition) > 1)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime*turbulenceSpeed);
        }
        else ComputeNewTurbulentPosition();
    }

    void ComputeNewTurbulentPosition()
    {
        //get a random point within a circle
        targetPosition = new Vector3(Random.insideUnitCircle.x * turbulenceRadius + spawnPosition.x, transform.position.y, Random.insideUnitCircle.y * turbulenceRadius + spawnPosition.z);
    }
}
